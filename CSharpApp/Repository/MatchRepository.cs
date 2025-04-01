namespace CSharpApp.Repository;

using Model;
using DB;
using System;
using System.Collections.Generic;

public class MatchRepository : IMatchRepository
{
    public List<Match> FindAll()
    {
        var list = new List<Match>();
        using var conn = DBUtils.GetConnection();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Matches";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Match
            {
                Id = reader.GetInt32(0),
                TeamA = reader.GetString(1),
                TeamB = reader.GetString(2),
                TicketPrice = reader.GetDouble(3),
                AvailableSeats = reader.GetInt32(4)
            });
        }

        return list;
    }

    public List<Match> FindAvailableMatches()
    {
        var list = new List<Match>();
        using var conn = DBUtils.GetConnection();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Matches WHERE availableSeats > 0 ORDER BY availableSeats DESC";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Match
            {
                Id = reader.GetInt32(0),
                TeamA = reader.GetString(1),
                TeamB = reader.GetString(2),
                TicketPrice = reader.GetDouble(3),
                AvailableSeats = reader.GetInt32(4)
            });
        }

        return list;
    }

    public void UpdateSeats(int matchId, int seatsSold)
    {
        using var conn = DBUtils.GetConnection();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Matches SET availableSeats = availableSeats - @seats WHERE id = @id";
        cmd.Parameters.AddWithValue("@seats", seatsSold);
        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }
}