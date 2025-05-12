using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using model;

namespace persistence
{
    public class TicketRepositoryDb : ITicketRepository
    {
        private readonly IDictionary<string, string> props;

        public TicketRepositoryDb(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public void Save(Ticket ticket)
        {
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO Tickets (matchId, userId, customerName, seatsSold) VALUES (@matchId, @userId, @customerName, @seatsSold)";

                    var paramMatchId = cmd.CreateParameter();
                    paramMatchId.ParameterName = "@matchId";
                    paramMatchId.Value = ticket.MatchId;
                    cmd.Parameters.Add(paramMatchId);

                    var paramUserId = cmd.CreateParameter();
                    paramUserId.ParameterName = "@userId";
                    paramUserId.Value = ticket.UserId;
                    cmd.Parameters.Add(paramUserId);

                    var paramCustomerName = cmd.CreateParameter();
                    paramCustomerName.ParameterName = "@customerName";
                    paramCustomerName.Value = ticket.CustomerName;
                    cmd.Parameters.Add(paramCustomerName);

                    var paramSeatsSold = cmd.CreateParameter();
                    paramSeatsSold.ParameterName = "@seatsSold";
                    paramSeatsSold.Value = ticket.SeatsSold;
                    cmd.Parameters.Add(paramSeatsSold);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}