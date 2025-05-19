using System;
using System.Collections.Generic;
using System.Data;
using model;
using Mono.Data.Sqlite;

namespace persistence
{
    public class MatchRestRepository : IMatchRepository
    {
        private readonly IDictionary<string, string> props;

        public MatchRestRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public List<Match> FindAll()
        {
            var matches = new List<Match>();
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Matches";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var match = new Match(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3),
                                reader.GetInt32(4)
                            );
                            matches.Add(match);
                        }
                    }
                }
            }

            return matches;
        }

        public List<Match> FindAvailableMatches()
        {
            var matches = new List<Match>();
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Matches WHERE availableSeats > 0 ORDER BY availableSeats DESC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var match = new Match(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3),
                                reader.GetInt32(4)
                            );
                            matches.Add(match);
                        }
                    }
                }
            }

            return matches;
        }

        public void UpdateSeats(int matchId, int seatsSold)
        {
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Matches SET availableSeats = availableSeats - @seatsSold WHERE id = @id";

                    var paramSeatsSold = cmd.CreateParameter();
                    paramSeatsSold.ParameterName = "@seatsSold";
                    paramSeatsSold.Value = seatsSold;
                    cmd.Parameters.Add(paramSeatsSold);

                    var paramId = cmd.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = matchId;
                    cmd.Parameters.Add(paramId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Match FindMatchById(int matchId)
        {
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Matches WHERE id = @id";

                    var paramId = cmd.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = matchId;
                    cmd.Parameters.Add(paramId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Match(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3),
                                reader.GetInt32(4)
                            );
                        }
                    }
                }
            }

            return null;
        }

        // New methods for REST operations

        // Create a new match
        public Match Save(Match match)
        {
            Console.WriteLine($"Creating new match: {match}");
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    // First find the max ID
                    cmd.CommandText = "SELECT MAX(id) FROM Matches";
                    object result = cmd.ExecuteScalar();
                    int newId = 1;  // Default if table is empty
                    if (result != null && result != DBNull.Value)
                    {
                        newId = Convert.ToInt32(result) + 1;
                    }

                    // Insert the new match
                    cmd.CommandText = "INSERT INTO Matches (id, teamA, teamB, ticketPrice, availableSeats) VALUES (@id, @teamA, @teamB, @ticketPrice, @availableSeats)";
                    
                    var paramId = cmd.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = newId;
                    cmd.Parameters.Add(paramId);
                    
                    var paramTeamA = cmd.CreateParameter();
                    paramTeamA.ParameterName = "@teamA";
                    paramTeamA.Value = match.TeamA;
                    cmd.Parameters.Add(paramTeamA);
                    
                    var paramTeamB = cmd.CreateParameter();
                    paramTeamB.ParameterName = "@teamB";
                    paramTeamB.Value = match.TeamB;
                    cmd.Parameters.Add(paramTeamB);
                    
                    var paramTicketPrice = cmd.CreateParameter();
                    paramTicketPrice.ParameterName = "@ticketPrice";
                    paramTicketPrice.Value = match.TicketPrice;
                    cmd.Parameters.Add(paramTicketPrice);
                    
                    var paramAvailableSeats = cmd.CreateParameter();
                    paramAvailableSeats.ParameterName = "@availableSeats";
                    paramAvailableSeats.Value = match.AvailableSeats;
                    cmd.Parameters.Add(paramAvailableSeats);
                    
                    cmd.ExecuteNonQuery();
                    
                    match.Id = newId;
                    Console.WriteLine($"Match created with ID: {newId}");
                    return match;
                }
            }
        }

        // Update a match (full update)
        public Match Update(int matchId, Match match)
        {
            Console.WriteLine($"Updating match with id={matchId}: {match}");
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Matches SET teamA = @teamA, teamB = @teamB, ticketPrice = @ticketPrice, availableSeats = @availableSeats WHERE id = @id";
                    
                    var paramTeamA = cmd.CreateParameter();
                    paramTeamA.ParameterName = "@teamA";
                    paramTeamA.Value = match.TeamA;
                    cmd.Parameters.Add(paramTeamA);
                    
                    var paramTeamB = cmd.CreateParameter();
                    paramTeamB.ParameterName = "@teamB";
                    paramTeamB.Value = match.TeamB;
                    cmd.Parameters.Add(paramTeamB);
                    
                    var paramTicketPrice = cmd.CreateParameter();
                    paramTicketPrice.ParameterName = "@ticketPrice";
                    paramTicketPrice.Value = match.TicketPrice;
                    cmd.Parameters.Add(paramTicketPrice);
                    
                    var paramAvailableSeats = cmd.CreateParameter();
                    paramAvailableSeats.ParameterName = "@availableSeats";
                    paramAvailableSeats.Value = match.AvailableSeats;
                    cmd.Parameters.Add(paramAvailableSeats);
                    
                    var paramId = cmd.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = matchId;
                    cmd.Parameters.Add(paramId);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Successfully updated match with id={matchId}");
                        match.Id = matchId;
                        return match;
                    }
                    else
                    {
                        Console.WriteLine($"No match found with id={matchId} for update");
                        return null;
                    }
                }
            }
        }

        // Delete a match
        public bool Delete(int matchId)
        {
            Console.WriteLine($"Deleting match with id={matchId}");
            using (var con = DbUtils.GetConnection(props))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Matches WHERE id = @id";
                    
                    var paramId = cmd.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = matchId;
                    cmd.Parameters.Add(paramId);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Successfully deleted match with id={matchId}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"No match found with id={matchId} for deletion");
                        return false;
                    }
                }
            }
        }
    }
}