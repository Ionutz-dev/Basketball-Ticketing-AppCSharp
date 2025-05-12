using System.Windows.Forms;

namespace client
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListBox matchListBox;
        private System.Windows.Forms.Label matchListLabel;
        private System.Windows.Forms.Label customerNameLabel;
        private System.Windows.Forms.TextBox customerNameTextBox;
        private System.Windows.Forms.Label seatsLabel;
        private System.Windows.Forms.TextBox seatsTextBox;
        private System.Windows.Forms.Button sellButton;
        private System.Windows.Forms.Button logoutButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.matchListBox = new System.Windows.Forms.ListBox();
            this.matchListLabel = new System.Windows.Forms.Label();
            this.customerNameLabel = new System.Windows.Forms.Label();
            this.customerNameTextBox = new System.Windows.Forms.TextBox();
            this.seatsLabel = new System.Windows.Forms.Label();
            this.seatsTextBox = new System.Windows.Forms.TextBox();
            this.sellButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // matchListLabel
            this.matchListLabel.AutoSize = true;
            this.matchListLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.matchListLabel.Location = new System.Drawing.Point(30, 20);
            this.matchListLabel.Text = "Available Matches";

            // matchListBox
            this.matchListBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.matchListBox.Location = new System.Drawing.Point(30, 50);
            this.matchListBox.Size = new System.Drawing.Size(520, 160);

            // customerNameLabel
            this.customerNameLabel.AutoSize = true;
            this.customerNameLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.customerNameLabel.Location = new System.Drawing.Point(30, 230);
            this.customerNameLabel.Text = "Customer Name:";

            // customerNameTextBox
            this.customerNameTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.customerNameTextBox.Location = new System.Drawing.Point(180, 227);
            this.customerNameTextBox.Size = new System.Drawing.Size(200, 25);

            // seatsLabel
            this.seatsLabel.AutoSize = true;
            this.seatsLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.seatsLabel.Location = new System.Drawing.Point(30, 270);
            this.seatsLabel.Text = "Seats to Buy:";

            // seatsTextBox
            this.seatsTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.seatsTextBox.Location = new System.Drawing.Point(180, 267);
            this.seatsTextBox.Size = new System.Drawing.Size(80, 25);

            // sellButton
            this.sellButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.sellButton.Location = new System.Drawing.Point(30, 320);
            this.sellButton.Size = new System.Drawing.Size(150, 40);
            this.sellButton.Text = "Sell Ticket";
            this.sellButton.Click += new System.EventHandler(this.sellButton_Click);

            // logoutButton
            this.logoutButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.logoutButton.Location = new System.Drawing.Point(230, 320);
            this.logoutButton.Size = new System.Drawing.Size(150, 40);
            this.logoutButton.Text = "Logout";
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);

            // MainWindow
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.matchListLabel);
            this.Controls.Add(this.matchListBox);
            this.Controls.Add(this.customerNameLabel);
            this.Controls.Add(this.customerNameTextBox);
            this.Controls.Add(this.seatsLabel);
            this.Controls.Add(this.seatsTextBox);
            this.Controls.Add(this.sellButton);
            this.Controls.Add(this.logoutButton);
            this.Name = "MainWindow";
            this.Text = "Basketball Ticket Sales";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.FormClosing += new FormClosingEventHandler(this.MainWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
