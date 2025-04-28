#pragma warning disable SYSLIB0011

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using model;
using services;

namespace networking
{
    public sealed class BasketballServerProxy : IBasketballServices
    {
        private readonly string host;
        private readonly int port;

        private IBasketballObserver client;

        private NetworkStream stream;
        private IFormatter formatter;
        private TcpClient connection;

        private readonly Queue<IResponse> responses;
        private volatile bool finished;
        private EventWaitHandle waitHandle;

        public BasketballServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<IResponse>();
        }

        public User Login(string username, string password, IBasketballObserver clientObserver)
        {
            InitializeConnection();
            client = clientObserver;
            var credentials = new string[] { username, password };
            SendRequest(new LoginRequest(credentials));
            var response = ReadResponse();

            switch (response)
            {
                case OkResponse _:
                    return new User(0, username, password); 
                case ErrorResponse err:
                    CloseConnection();
                    throw new BasketballException(err.Message);
                default:
                    throw new BasketballException("Unknown response during login");
            }
        }

        public void Logout(User user)
        {
            var userDto = DTOUtils.GetDTO(user);
            SendRequest(new LogoutRequest(userDto));
            var response = ReadResponse();
            if (response is ErrorResponse err)
            {
                throw new BasketballException(err.Message);
            }
            CloseConnection();
        }

        public IEnumerable<Match> GetAvailableMatches()
        {
            SendRequest(new GetAvailableMatchesRequest());
            var response = ReadResponse();

            if (response is ErrorResponse err)
            {
                throw new BasketballException(err.Message);
            }

            var matchesResponse = (GetAvailableMatchesResponse)response;
            var matchesDto = matchesResponse.Matches;
            var matches = DTOUtils.GetFromDTO(matchesDto);

            return matches;
        }

        public void SellTicket(int matchId, int userId, string customerName, int seatsToBuy)
        {
            var ticketDto = new TicketDTO(0, matchId, userId, customerName, seatsToBuy);
            SendRequest(new SellTicketRequest(ticketDto));
            var response = ReadResponse();

            if (response is ErrorResponse err)
            {
                throw new BasketballException(err.Message);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new BasketballException("Error initializing connection: " + e.Message);
            }
        }

        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                connection.Close();
                waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SendRequest(IRequest request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new BasketballException("Error sending request: " + e);
            }
        }

        private IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                waitHandle.WaitOne();
                lock (responses)
                {
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }

        private void StartReader()
        {
            var t = new Thread(Run);
            t.Start();
        }

        private void Run()
        {
            while (!finished)
            {
                try
                {
                    var response = (IResponse)formatter.Deserialize(stream);
                    Console.WriteLine("Received response: " + response);

                    if (response is IUpdateResponse update)
                    {
                        HandleUpdate(update);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue(response);
                        }

                        waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    if (!finished)
                        Console.WriteLine("Reading error: " + e.Message);
                    else
                        Console.WriteLine("Connection closed normally.");
                }
            }
        }

        private void HandleUpdate(IUpdateResponse update)
        {
            switch (update)
            {
                case TicketSoldResponse _:
                    Console.WriteLine("Ticket sold update received!");
                    try
                    {
                        client?.TicketSoldUpdate();
                    }
                    catch (BasketballException e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }

                    break;
            }
        }
    }
}