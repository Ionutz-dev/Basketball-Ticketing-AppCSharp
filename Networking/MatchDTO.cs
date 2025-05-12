using System;

namespace networking
{
    [Serializable]
    public class MatchDTO
    {
        public int Id { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public double TicketPrice { get; set; }
        public int AvailableSeats { get; set; }

        public MatchDTO(int id, string teamA, string teamB, double ticketPrice, int availableSeats)
        {
            Id = id;
            TeamA = teamA;
            TeamB = teamB;
            TicketPrice = ticketPrice;
            AvailableSeats = availableSeats;
        }

        public override string ToString()
        {
            return
                $"MatchDTO{{id={Id}, teamA='{TeamA}', teamB='{TeamB}', ticketPrice={TicketPrice}, availableSeats={AvailableSeats}}}";
        }
    }
}