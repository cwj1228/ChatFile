
namespace ChatProgramClient
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.PWTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(22, 66);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(145, 21);
            this.IDTextBox.TabIndex = 0;
            // 
            // PWTextBox
            // 
            this.PWTextBox.Location = new System.Drawing.Point(22, 107);
            this.PWTextBox.Name = "PWTextBox";
            this.PWTextBox.Size = new System.Drawing.Size(145, 21);
            this.PWTextBox.TabIndex = 1;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(182, 66);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(64, 62);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "LogIn";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(22, 12);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(224, 37);
            this.CreateButton.TabIndex = 4;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 148);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PWTextBox);
            this.Controls.Add(this.IDTextBox);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.TextBox PWTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button CreateButton;
    }
}