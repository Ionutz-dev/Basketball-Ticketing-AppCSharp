using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using model;
using services;

namespace client
{
    public partial class MainWindow : Form
    {
        private readonly BasketballClientCtrl ctrl;
        private IList<Match> matches = new List<Match>();
        private bool isLoggingOut = false;

        public MainWindow(BasketballClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            ctrl.UpdateEvent += OnUpdateEvent;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("MainWindow closing " + e.CloseReason);

            if (!isLoggingOut && ctrl != null && ctrl.IsLoggedIn())
            {
                try
                {
                    ctrl.Logout();
                    Console.WriteLine("Logout successful");
                }
                catch (BasketballException ex)
                {
                    Console.WriteLine("BasketballException during logout: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error during logout: " + ex.Message);
                }
            }

            ctrl.UpdateEvent -= OnUpdateEvent;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadMatches();
        }

        private void LoadMatches()
        {
            matches = ctrl.GetAvailableMatches().ToList();
            matchListBox.Items.Clear();

            foreach (var match in matches)
            {
                string displayText =
                    $"{match.TeamA} vs {match.TeamB} - {match.AvailableSeats} seats - ${match.TicketPrice}";
                matchListBox.Items.Add(displayText);
            }
        }

        private void OnUpdateEvent(object sender, BasketballUserEventArgs e)
        {
            if (e.UserEventType == BasketballUserEvent.TicketSold)
            {
                Console.WriteLine("Ticket sold event received - refreshing matches");

                if (matchListBox.InvokeRequired)
                {
                    matchListBox.BeginInvoke(new UpdateListBoxCallback(UpdateMatchListBox));
                }
                else
                {
                    UpdateMatchListBox();
                }
            }
        }

        private void UpdateMatchListBox()
        {
            LoadMatches();
        }

        private void sellButton_Click(object sender, EventArgs e)
        {
            if (matchListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a match first!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var selectedMatch = matches[matchListBox.SelectedIndex];
            string customerName = customerNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter customer name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(seatsTextBox.Text.Trim(), out int seats) || seats <= 0)
            {
                MessageBox.Show("Invalid number of seats!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ctrl.SellTicket(selectedMatch.Id, customerName, seats);
                MessageBox.Show("Ticket(s) sold successfully!", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                customerNameTextBox.Clear();
                seatsTextBox.Clear();
            }
            catch (BasketballException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            if (ctrl != null && ctrl.IsLoggedIn())
            {
                try
                {
                    ctrl.Logout();
                    Console.WriteLine("Logout successful via logout button.");
                }
                catch (BasketballException ex)
                {
                    Console.WriteLine("BasketballException during logout button: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error during logout button: " + ex.Message);
                }
            }
            
            isLoggingOut = true;
            this.Close(); 
        }

        public delegate void UpdateListBoxCallback();
    }

    public static class MatchExtensions
    {
        public static string MatchDisplay(this Match match)
        {
            return $"{match.TeamA} vs {match.TeamB} - {match.AvailableSeats} seats - ${match.TicketPrice}";
        }
    }
}