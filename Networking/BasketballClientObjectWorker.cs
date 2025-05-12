#pragma warning disable SYSLIB0011

using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using model;
using services;

namespace networking
{
    public sealed class BasketballClientWorker : IBasketballObserver
    {
        private readonly IBasketballServices server;
        private readonly TcpClient connection;

        private readonly NetworkStream stream;
        private readonly IFormatter formatter;
        private volatile bool connected;

        public BasketballClientWorker(IBasketballServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Run()
        {
            while (connected)
            {
                try
                {
                    var request = formatter.Deserialize(stream);
                    var response = HandleRequest((IRequest)request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error closing connection: " + e);
            }
        }

        public void TicketSoldUpdate()
        {
            Console.WriteLine("Ticket sold update - notifying client...");
            try
            {
                SendResponse(new TicketSoldResponse());
            }
            catch (Exception e)
            {
                throw new BasketballException("Sending error: " + e);
            }
        }

        private IResponse HandleRequest(IRequest request)
        {
            IResponse response = null;
            switch (request)
            {
                case LoginRequest req:
                {
                    Console.WriteLine("Login request received...");
                    var creds = req.Credentials; // string[] {username, password}
                    try
                    {
                        lock (server)
                        {
                            server.Login(creds[0], creds[1], this);
                        }

                        return new OkResponse();
                    }
                    catch (BasketballException e)
                    {
                        connected = false;
                        return new ErrorResponse(e.Message);
                    }
                }
                case LogoutRequest logReq:
                {
                    Console.WriteLine("Logout request received...");
                    var userDto = logReq.User;
                    try
                    {
                        lock (server)
                        {
                            server.Logout(DTOUtils.GetFromDTO(userDto));
                        }

                        connected = false;
                        return new OkResponse();
                    }
                    catch (BasketballException e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                }
                case GetAvailableMatchesRequest getMatchesReq:
                {
                    Console.WriteLine("GetAvailableMatches request received...");
                    try
                    {
                        lock (server)
                        {
                            var matches = server.GetAvailableMatches();
                            var matchesDto = DTOUtils.GetDTO(matches);
                            return new GetAvailableMatchesResponse(matchesDto);
                        }
                    }
                    catch (BasketballException e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                }
                case SellTicketRequest sellReq:
                {
                    Console.WriteLine("SellTicket request received...");
                    var ticketDto = sellReq.Ticket;
                    try
                    {
                        lock (server)
                        {
                            server.SellTicket(ticketDto.MatchId, ticketDto.UserId, ticketDto.CustomerName,
                                ticketDto.SeatsSold);
                        }

                        return new TicketConfirmedResponse();
                    }
                    catch (BasketballException e)
                    {
                        return new ErrorResponse(e.Message);
                    }
                }
                default:
                    return null;
            }
        }

        private void SendResponse(IResponse response)
        {
            Console.WriteLine($"Sending response {response.GetType().Name}...");
            lock (stream)
            {
                formatter.Serialize(stream, response);
                stream.Flush();
            }
        }
    }
}