using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using model;
using networking;
using persistence;
using Ticketing;
using SellTicketRequest = Ticketing.SellTicketRequest;

namespace server.grpc
{
    public class GrpcBasketballServiceImpl : TicketService.TicketServiceBase
    {
        private readonly IUserRepository userRepo;
        private readonly IMatchRepository matchRepo;
        private readonly ITicketRepository ticketRepo;
        
        private static readonly object watchersLock = new object();
        private static readonly List<IServerStreamWriter<MatchList>> watchers = new List<IServerStreamWriter<MatchList>>();

        public GrpcBasketballServiceImpl(IUserRepository userRepo, IMatchRepository matchRepo, ITicketRepository ticketRepo)
        {
            this.userRepo = userRepo;
            this.matchRepo = matchRepo;
            this.ticketRepo = ticketRepo;
        }

        public override Task<LoginResponse> Login(Ticketing.LoginRequest request, ServerCallContext context)
        {
            var user = userRepo.FindByUsernameAndPassword(request.Username, request.Password);
            if (user == null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password",
                    UserId = 0
                });
            }

            return Task.FromResult(new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                UserId = user.Id
            });
        }

        public override Task<MatchList> GetAllMatches(Empty request, ServerCallContext context)
        {
            var matches = matchRepo.FindAll();
            var response = new MatchList();

            foreach (var m in matches)
            {
                response.Matches.Add(new Ticketing.Match
                {
                    Id = m.Id,
                    TeamA = m.TeamA,
                    TeamB = m.TeamB,
                    TicketPrice = m.TicketPrice,
                    AvailableSeats = m.AvailableSeats
                });
            }

            return Task.FromResult(response);
        }

        public override Task<SellTicketResponse> SellTicket(SellTicketRequest request, ServerCallContext context)
        {
            var match = matchRepo.FindMatchById(request.MatchId);
            if (match == null)
            {
                return Task.FromResult(new SellTicketResponse
                {
                    Success = false,
                    Message = "Match not found"
                });
            }

            if (match.AvailableSeats < request.Seats)
            {
                return Task.FromResult(new SellTicketResponse
                {
                    Success = false,
                    Message = "Not enough seats available"
                });
            }

            var ticket = new Ticket(0, request.MatchId, request.UserId, request.CustomerName, request.Seats);
            ticketRepo.Save(ticket);
            matchRepo.UpdateSeats(match.Id, request.Seats);
            
            NotifyWatchers();

            return Task.FromResult(new SellTicketResponse
            {
                Success = true,
                Message = "Ticket sold successfully"
            });
        }

        public override Task WatchMatches(Empty request, IServerStreamWriter<MatchList> responseStream, ServerCallContext context)
        {
            Console.WriteLine("[WatchMatches] New watcher connected");

            lock (watchersLock)
            {
                watchers.Add(responseStream);
            }
            
            return Task.Run(() =>
            {
                try
                {
                    while (!context.CancellationToken.IsCancellationRequested)
                    {
                        Thread.Sleep(1000);
                    }
                }
                finally
                {
                    lock (watchersLock)
                    {
                        watchers.Remove(responseStream);
                        Console.WriteLine("[WatchMatches] Watcher disconnected");
                    }
                }
            });
        }

        private void NotifyWatchers()
        {
            var matchList = BuildMatchList();
            lock (watchersLock)
            {
                foreach (var watcher in watchers.ToArray()) 
                {
                    try
                    {
                        Task.Run(() => watcher.WriteAsync(matchList));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[NotifyWatchers] Failed: {ex.Message}");
                    }
                }
            }
        }

        private MatchList BuildMatchList()
        {
            var matches = matchRepo.FindAll();
            var response = new MatchList();

            foreach (var m in matches)
            {
                response.Matches.Add(new Ticketing.Match
                {
                    Id = m.Id,
                    TeamA = m.TeamA,
                    TeamB = m.TeamB,
                    TicketPrice = m.TicketPrice,
                    AvailableSeats = m.AvailableSeats
                });
            }

            return response;
        }
    }
}
