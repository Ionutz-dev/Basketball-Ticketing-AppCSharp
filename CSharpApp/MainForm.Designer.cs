namespace CSharpApp;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.DataGridView matchDataGridView;
    private System.Windows.Forms.TextBox customerTextBox;
    private System.Windows.Forms.TextBox seatsTextBox;
    private System.Windows.Forms.Button sellButton;
    private System.Windows.Forms.Label statusLabel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.matchDataGridView = new System.Windows.Forms.DataGridView();
        this.customerTextBox = new System.Windows.Forms.TextBox();
        this.seatsTextBox = new System.Windows.Forms.TextBox();
        this.sellButton = new System.Windows.Forms.Button();
        this.statusLabel = new System.Windows.Forms.Label();
        System.Windows.Forms.Label titleLabel = new System.Windows.Forms.Label();

        ((System.ComponentModel.ISupportInitialize)(this.matchDataGridView)).BeginInit();
        this.SuspendLayout();

        // MainForm
        this.ClientSize = new System.Drawing.Size(720, 400);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.Font = new Font("Segoe UI", 10F);
        this.Text = "Basketball Ticket Sales";

        // Title Label
        titleLabel.Text = "Basketball Ticket Sales";
        titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        titleLabel.Location = new Point(20, 10);
        titleLabel.AutoSize = true;

        // matchDataGridView
        this.matchDataGridView.AllowUserToAddRows = false;
        this.matchDataGridView.AllowUserToDeleteRows = false;
        this.matchDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.matchDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.matchDataGridView.MultiSelect = false;
        this.matchDataGridView.Location = new System.Drawing.Point(20, 50);
        this.matchDataGridView.Size = new System.Drawing.Size(670, 180);
        this.matchDataGridView.TabIndex = 0;

        // customerTextBox
        this.customerTextBox.Location = new System.Drawing.Point(20, 260);
        this.customerTextBox.Size = new System.Drawing.Size(240, 30);
        this.customerTextBox.PlaceholderText = "Customer Name";

        // seatsTextBox
        this.seatsTextBox.Location = new System.Drawing.Point(270, 260);
        this.seatsTextBox.Size = new System.Drawing.Size(120, 30);
        this.seatsTextBox.PlaceholderText = "Seats to Buy";

        // sellButton
        this.sellButton.Location = new System.Drawing.Point(400, 260);
        this.sellButton.Size = new System.Drawing.Size(120, 35);
        this.sellButton.Text = "Buy Ticket";
        this.sellButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.sellButton.Click += new EventHandler(this.sellButton_Click);

        // statusLabel
        this.statusLabel.AutoSize = true;
        this.statusLabel.Location = new System.Drawing.Point(20, 310);
        this.statusLabel.Size = new System.Drawing.Size(0, 19);

        // Add controls
        this.Controls.Add(titleLabel);
        this.Controls.Add(this.matchDataGridView);
        this.Controls.Add(this.customerTextBox);
        this.Controls.Add(this.seatsTextBox);
        this.Controls.Add(this.sellButton);
        this.Controls.Add(this.statusLabel);

        ((System.ComponentModel.ISupportInitialize)(this.matchDataGridView)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
