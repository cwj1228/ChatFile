
namespace ChatProgramServer
{
    partial class Server
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
            this.PlayerList = new System.Windows.Forms.GroupBox();
            this.z = new System.Windows.Forms.GroupBox();
            this.ServerText = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.CheckPlayer = new System.Windows.Forms.CheckedListBox();
            this.RoomList = new System.Windows.Forms.ListBox();
            this.PlayerList.SuspendLayout();
            this.z.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayerList
            // 
            this.PlayerList.Controls.Add(this.CheckPlayer);
            this.PlayerList.Location = new System.Drawing.Point(12, 21);
            this.PlayerList.Name = "PlayerList";
            this.PlayerList.Size = new System.Drawing.Size(227, 363);
            this.PlayerList.TabIndex = 0;
            this.PlayerList.TabStop = false;
            this.PlayerList.Text = "PlayerList";
            // 
            // z
            // 
            this.z.Controls.Add(this.RoomList);
            this.z.Location = new System.Drawing.Point(253, 21);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(227, 336);
            this.z.TabIndex = 1;
            this.z.TabStop = false;
            this.z.Text = "ChatRoom";
            // 
            // ServerText
            // 
            this.ServerText.Location = new System.Drawing.Point(253, 363);
            this.ServerText.Name = "ServerText";
            this.ServerText.Size = new System.Drawing.Size(168, 21);
            this.ServerText.TabIndex = 0;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(427, 361);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(53, 23);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "SEND";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // CheckPlayer
            // 
            this.CheckPlayer.FormattingEnabled = true;
            this.CheckPlayer.Location = new System.Drawing.Point(17, 20);
            this.CheckPlayer.Name = "CheckPlayer";
            this.CheckPlayer.Size = new System.Drawing.Size(190, 324);
            this.CheckPlayer.TabIndex = 0;
            // 
            // RoomList
            // 
            this.RoomList.FormattingEnabled = true;
            this.RoomList.ItemHeight = 12;
            this.RoomList.Location = new System.Drawing.Point(16, 20);
            this.RoomList.Name = "RoomList";
            this.RoomList.Size = new System.Drawing.Size(192, 304);
            this.RoomList.TabIndex = 0;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 393);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ServerText);
            this.Controls.Add(this.z);
            this.Controls.Add(this.PlayerList);
            this.Name = "Server";
            this.Text = "Server";
            this.PlayerList.ResumeLayout(false);
            this.z.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox PlayerList;
        private System.Windows.Forms.CheckedListBox CheckPlayer;
        private System.Windows.Forms.GroupBox z;
        private System.Windows.Forms.TextBox ServerText;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ListBox RoomList;
    }
}