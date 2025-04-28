using System.Windows.Forms;

namespace client
{
    partial class LoginWindow
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button clearButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // usernameLabel
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.usernameLabel.Location = new System.Drawing.Point(30, 40);
            this.usernameLabel.Text = "Username:";

            // usernameTextBox
            this.usernameTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.usernameTextBox.Location = new System.Drawing.Point(130, 37);
            this.usernameTextBox.Size = new System.Drawing.Size(180, 25);

            // passwordLabel
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordLabel.Location = new System.Drawing.Point(30, 90);
            this.passwordLabel.Text = "Password:";

            // passwordTextBox
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordTextBox.Location = new System.Drawing.Point(130, 87);
            this.passwordTextBox.Size = new System.Drawing.Size(180, 25);
            this.passwordTextBox.PasswordChar = '*';

            // loginButton
            this.loginButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.loginButton.Location = new System.Drawing.Point(30, 140);
            this.loginButton.Size = new System.Drawing.Size(120, 40);
            this.loginButton.Text = "Login";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);

            // clearButton
            this.clearButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.clearButton.Location = new System.Drawing.Point(190, 140);
            this.clearButton.Size = new System.Drawing.Size(120, 40);
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);

            // LoginWindow
            this.ClientSize = new System.Drawing.Size(360, 220);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.clearButton);
            this.Name = "LoginWindow";
            this.Text = "Basketball Client - Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
