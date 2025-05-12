namespace model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public int SeatsSold { get; set; }

        public Ticket(int id, int matchId, int userId, string customerName, int seatsSold)
        {
            Id = id;
            MatchId = matchId;
            UserId = userId;
            CustomerName = customerName;
            SeatsSold = seatsSold;
        }

        public override string ToString()
        {
            return
                $"Ticket{{id={Id}, matchId={MatchId}, userId={UserId}, customerName='{CustomerName}', seatsSold={SeatsSold}}}";
        }
    }
}