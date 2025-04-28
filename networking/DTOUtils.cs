using System.Collections.Generic;
using System.Linq;
using model;

namespace networking
{
    public static class DTOUtils
    {
        public static Match GetFromDTO(MatchDTO dto)
        {
            return new Match(dto.Id, dto.TeamA, dto.TeamB, dto.TicketPrice, dto.AvailableSeats);
        }

        public static Match[] GetFromDTO(MatchDTO[] dtos)
        {
            return dtos.Select(GetFromDTO).ToArray();
        }

        public static MatchDTO GetDTO(Match match)
        {
            return new MatchDTO(match.Id, match.TeamA, match.TeamB, match.TicketPrice, match.AvailableSeats);
        }

        public static MatchDTO[] GetDTO(IEnumerable<Match> matches)
        {
            return matches.Select(m => GetDTO(m)).ToArray();
        }

        public static Ticket GetFromDTO(TicketDTO dto)
        {
            return new Ticket(dto.Id, dto.MatchId, dto.UserId, dto.CustomerName, dto.SeatsSold);
        }

        public static TicketDTO GetDTO(Ticket ticket)
        {
            return new TicketDTO(ticket.Id, ticket.MatchId, ticket.UserId, ticket.CustomerName, ticket.SeatsSold);
        }

        public static User GetFromDTO(UserDTO dto)
        {
            return new User(dto.Id, dto.Username, dto.Password);
        }

        public static UserDTO GetDTO(User user)
        {
            return new UserDTO(user.Id, user.Username, user.Password);
        }
    }
}