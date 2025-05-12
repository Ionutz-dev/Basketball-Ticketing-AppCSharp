using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using model;

namespace persistence
{
    public class MatchRepositoryDb : IMatchRepository
    {
        private readonly IDictionary<string, string> props;

        public MatchRepositoryDb(IDictionary<string, string> props)
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
    }
}