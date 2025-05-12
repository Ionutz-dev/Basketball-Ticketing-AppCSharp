namespace model
{
    public class Match
    {
        public int Id { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public double TicketPrice { get; set; }
        public int AvailableSeats { get; set; }

        public Match(int id, string teamA, string teamB, double ticketPrice, int availableSeats)
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
                $"Match{{id={Id}, teamA='{TeamA}', teamB='{TeamB}', ticketPrice={TicketPrice}, availableSeats={AvailableSeats}}}";
        }

        // public string MatchDisplay
        // {
        //     get { return $"{TeamA} vs {TeamB} - {AvailableSeats} seats - ${TicketPrice}"; }
        // }
    }
}