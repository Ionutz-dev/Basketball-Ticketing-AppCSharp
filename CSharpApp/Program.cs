using System;
using CSharpApp.Repository;

class Program {
    static void Main(string[] args) {
        var repo = new MatchRepository();
        var matches = repo.FindAll();
        foreach (var match in matches) {
            Console.WriteLine($"{match.TeamA} vs {match.TeamB}");
        }
    }
}