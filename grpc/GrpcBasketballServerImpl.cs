using System;
using System.Threading.Tasks;
using Grpc.Core;
using model;
using services;
using persistence;
using Ticketing;

namespace grpc
{
    public class GrpcBasketballServiceImpl : TicketService.TicketServiceBase
    {
        private readonly IMatchRepository matchRepo;
        private readonly ITicketRepository ticketRepo;

        public GrpcBasketballServiceImpl(IMatchRepository matchRepo, ITicketRepository ticketRepo)
        {
            this.matchRepo = matchRepo;
            this.ticketRepo = ticketRepo;
        }

        public override Task<MatchList> GetAllMatches(Empty request, ServerCallContext context)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("[GetAllMatches] Exception: " + ex.Message);
                throw new RpcException(new Status(StatusCode.Internal, "Failed to fetch matches: " + ex.Message));
            }
        }

        public override Task<BuyTicketResponse> BuyTicket(BuyTicketRequest request, ServerCallContext context)
        {
            var match = matchRepo.FindMatchById(request.MatchId);
            if (match == null)
            {
                return Task.FromResult(new BuyTicketResponse
                {
                    Success = false,
                    Message = "Match not found"
                });
            }

            if (match.AvailableSeats < request.Seats)
            {
                return Task.FromResult(new BuyTicketResponse
                {
                    Success = false,
                    Message = "Not enough seats"
                });
            }

            var ticket = new Ticket(0, request.MatchId, request.UserId, request.CustomerName, request.Seats);
            ticketRepo.Save(ticket);
            matchRepo.UpdateSeats(match.Id, request.Seats);

            return Task.FromResult(new BuyTicketResponse
            {
                Success = true,
                Message = "Ticket bought successfully"
            });
        }
    }
}
