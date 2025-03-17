namespace CSharpApp.Repository;

using Model;
using DB;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class MatchRepository : IMatchRepository
{
    public List<Match> FindAll() {
            List<Match> matches = new List<Match>();
            using (var conn = DBUtils.GetConnection()) {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Matches", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    matches.Add(new Match {
                        Id = Convert.ToInt32(reader["id"]),
                        TeamA = reader["teamA"].ToString(),
                        TeamB = reader["teamB"].ToString(),
                        TicketPrice = Convert.ToDouble(reader["ticketPrice"]),
                        AvailableSeats = Convert.ToInt32(reader["availableSeats"])
                    });
                }
                Console.WriteLine("Fetched all matches.");
            }
            return matches;
        }

        public List<Match> FindAvailableMatches() {
            List<Match> matches = new List<Match>();
            using (var conn = DBUtils.GetConnection()) {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Matches WHERE availableSeats > 0 ORDER BY availableSeats DESC", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    matches.Add(new Match {
                        Id = Convert.ToInt32(reader["id"]),
                        TeamA = reader["teamA"].ToString(),
                        TeamB = reader["teamB"].ToString(),
                        TicketPrice = Convert.ToDouble(reader["ticketPrice"]),
                        AvailableSeats = Convert.ToInt32(reader["availableSeats"])
                    });
                }
                Console.WriteLine("Fetched available matches.");
            }
            return matches;
        }

        public void UpdateSeats(int matchId, int seatsSold) {
            using (var conn = DBUtils.GetConnection()) {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Matches SET availableSeats = availableSeats - @seats WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@seats", seatsSold);
                cmd.Parameters.AddWithValue("@id", matchId);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Updated seats for matchId={matchId}");
            }
        }
}