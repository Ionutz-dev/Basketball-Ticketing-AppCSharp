using System;
using System.Windows.Forms;
using CSharpApp.Repository;

namespace CSharpApp;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        IMatchRepository matchRepo = new MatchRepository();
        ITicketRepository ticketRepo = new TicketRepository();

        Application.Run(new MainForm(matchRepo, ticketRepo));
    }
}