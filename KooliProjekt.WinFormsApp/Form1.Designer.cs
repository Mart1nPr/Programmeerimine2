namespace KooliProjekt.WinFormsApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            UsersGrid = new DataGridView();
            IdLabel = new Label();
            IdField = new TextBox();
            EmailLabel = new Label();
            EmailField = new TextBox();
            NameLabel = new Label();
            NameField = new TextBox();
            PasswordLabel = new Label();
            PasswordField = new TextBox();
            RegTimeLabel = new Label();
            RegTimePicker = new DateTimePicker();
            NewButton = new Button();
            SaveButton = new Button();
            DeleteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)(UsersGrid)).BeginInit();
            SuspendLayout();
            // 
            // UsersGrid
            // 
            UsersGrid.AllowUserToAddRows = false;
            UsersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UsersGrid.Location = new Point(12, 12);
            UsersGrid.MultiSelect = false;
            UsersGrid.Name = "UsersGrid";
            UsersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            UsersGrid.Size = new Size(400, 426);
            UsersGrid.TabIndex = 0;
            // 
            // IdLabel
            // 
            IdLabel.AutoSize = true;
            IdLabel.Location = new Point(430, 25);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new Size(21, 15);
            IdLabel.TabIndex = 1;
            IdLabel.Text = "ID:";
            // 
            // IdField
            // 
            IdField.Location = new Point(530, 22);
            IdField.Name = "IdField";
            IdField.ReadOnly = true;
            IdField.Size = new Size(247, 23);
            IdField.TabIndex = 2;
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.Location = new Point(430, 63);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(39, 15);
            EmailLabel.TabIndex = 3;
            EmailLabel.Text = "Email:";
            // 
            // EmailField
            // 
            EmailField.Location = new Point(530, 60);
            EmailField.Name = "EmailField";
            EmailField.Size = new Size(247, 23);
            EmailField.TabIndex = 4;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(430, 101);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(42, 15);
            NameLabel.TabIndex = 5;
            NameLabel.Text = "Name:";
            // 
            // NameField
            // 
            NameField.Location = new Point(530, 98);
            NameField.Name = "NameField";
            NameField.Size = new Size(247, 23);
            NameField.TabIndex = 6;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new Point(430, 139);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(60, 15);
            PasswordLabel.TabIndex = 7;
            PasswordLabel.Text = "Password:";
            // 
            // PasswordField
            // 
            PasswordField.Location = new Point(530, 136);
            PasswordField.Name = "PasswordField";
            PasswordField.Size = new Size(247, 23);
            PasswordField.TabIndex = 8;
            // 
            // RegTimeLabel
            // 
            RegTimeLabel.AutoSize = true;
            RegTimeLabel.Location = new Point(430, 177);
            RegTimeLabel.Name = "RegTimeLabel";
            RegTimeLabel.Size = new Size(89, 15);
            RegTimeLabel.TabIndex = 9;
            RegTimeLabel.Text = "Registered Time:";
            // 
            // RegTimePicker
            // 
            RegTimePicker.Location = new Point(530, 174);
            RegTimePicker.Name = "RegTimePicker";
            RegTimePicker.Size = new Size(247, 23);
            RegTimePicker.TabIndex = 10;
            // 
            // NewButton
            // 
            NewButton.Location = new Point(530, 220);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 23);
            NewButton.TabIndex = 11;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(611, 220);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 12;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(692, 220);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 13;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(NewButton);
            Controls.Add(RegTimePicker);
            Controls.Add(RegTimeLabel);
            Controls.Add(PasswordField);
            Controls.Add(PasswordLabel);
            Controls.Add(NameField);
            Controls.Add(NameLabel);
            Controls.Add(EmailField);
            Controls.Add(EmailLabel);
            Controls.Add(IdField);
            Controls.Add(IdLabel);
            Controls.Add(UsersGrid);
            Name = "Form1";
            Text = "User Management";
            ((System.ComponentModel.ISupportInitialize)(UsersGrid)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView UsersGrid;
        private Label IdLabel;
        private TextBox IdField;
        private Label EmailLabel;
        private TextBox EmailField;
        private Label NameLabel;
        private TextBox NameField;
        private Label PasswordLabel;
        private TextBox PasswordField;
        private Label RegTimeLabel;
        private DateTimePicker RegTimePicker;
        private Button NewButton;
        private Button SaveButton;
        private Button DeleteButton;
    }
}
