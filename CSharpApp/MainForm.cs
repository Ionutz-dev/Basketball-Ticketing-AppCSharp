using System;
using System.Windows.Forms;
using CSharpApp.Model;
using CSharpApp.Repository;
using CSharpApp.Service;

namespace CSharpApp;

public partial class MainForm : Form
{
    private readonly TicketService ticketService;

    public MainForm(IMatchRepository matchRepo, ITicketRepository ticketRepo)
    {
        InitializeComponent();
        ticketService = new TicketService(matchRepo, ticketRepo);
        LoadMatches();
    }

    private void LoadMatches()
    {
        var matches = ticketService.GetAvailableMatches();
        matchDataGridView.DataSource = matches;
    }

    private void sellButton_Click(object sender, EventArgs e)
    {
        if (matchDataGridView.SelectedRows.Count == 0)
        {
            statusLabel.Text = "Please select a match.";
            return;
        }

        var selectedMatch = (Match)matchDataGridView.SelectedRows[0].DataBoundItem;
        string customer = customerTextBox.Text.Trim();
        string seatsText = seatsTextBox.Text.Trim();

        if (string.IsNullOrEmpty(customer) || string.IsNullOrEmpty(seatsText))
        {
            statusLabel.Text = "Enter customer name and seats.";
            return;
        }

        if (!int.TryParse(seatsText, out int seats) || seats <= 0)
        {
            statusLabel.Text = "Invalid number of seats.";
            return;
        }

        if (selectedMatch.AvailableSeats < seats)
        {
            statusLabel.Text = "Not enough available seats.";
            return;
        }

        ticketService.BuyTicket(selectedMatch.Id, customer, seats, 1); // userId = 1
        statusLabel.Text = "Ticket sold successfully.";
        customerTextBox.Clear();
        seatsTextBox.Clear();
        LoadMatches();
    }
}