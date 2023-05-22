
namespace ChatProgramClient
{
    partial class Chanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RoomList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChatLog = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PlayerList = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RoomPlayerList = new System.Windows.Forms.ListBox();
            this.SendTTS = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ServerChat = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RoomList);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 447);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ChanelList";
            // 
            // RoomList
            // 
            this.RoomList.FormattingEnabled = true;
            this.RoomList.ItemHeight = 12;
            this.RoomList.Location = new System.Drawing.Point(16, 20);
            this.RoomList.Name = "RoomList";
            this.RoomList.Size = new System.Drawing.Size(87, 412);
            this.RoomList.TabIndex = 0;
            this.RoomList.DoubleClick += new System.EventHandler(this.JoinRoom);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChatLog);
            this.groupBox2.Location = new System.Drawing.Point(152, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 420);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Room";
            // 
            // ChatLog
            // 
            this.ChatLog.FormattingEnabled = true;
            this.ChatLog.ItemHeight = 12;
            this.ChatLog.Location = new System.Drawing.Point(18, 20);
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.Size = new System.Drawing.Size(236, 388);
            this.ChatLog.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PlayerList);
            this.groupBox3.Location = new System.Drawing.Point(631, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 420);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PlayerList";
            // 
            // PlayerList
            // 
            this.PlayerList.FormattingEnabled = true;
            this.PlayerList.Location = new System.Drawing.Point(15, 20);
            this.PlayerList.Name = "PlayerList";
            this.PlayerList.Size = new System.Drawing.Size(116, 388);
            this.PlayerList.TabIndex = 0;
            this.PlayerList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.PlayerCheck);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RoomPlayerList);
            this.groupBox4.Location = new System.Drawing.Point(433, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(190, 223);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "RoomPlayer";
            // 
            // RoomPlayerList
            // 
            this.RoomPlayerList.FormattingEnabled = true;
            this.RoomPlayerList.ItemHeight = 12;
            this.RoomPlayerList.Location = new System.Drawing.Point(16, 20);
            this.RoomPlayerList.Name = "RoomPlayerList";
            this.RoomPlayerList.Size = new System.Drawing.Size(157, 184);
            this.RoomPlayerList.TabIndex = 0;
            // 
            // SendTTS
            // 
            this.SendTTS.Location = new System.Drawing.Point(176, 430);
            this.SendTTS.Name = "SendTTS";
            this.SendTTS.Size = new System.Drawing.Size(198, 21);
            this.SendTTS.TabIndex = 1;
            this.SendTTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyBoardInput);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(390, 430);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ServerChat);
            this.groupBox5.Location = new System.Drawing.Point(433, 232);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(190, 191);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ServerChat";
            // 
            // ServerChat
            // 
            this.ServerChat.FormattingEnabled = true;
            this.ServerChat.ItemHeight = 12;
            this.ServerChat.Location = new System.Drawing.Point(16, 19);
            this.ServerChat.Name = "ServerChat";
            this.ServerChat.Size = new System.Drawing.Size(157, 160);
            this.ServerChat.TabIndex = 0;
            // 
            // Chanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 465);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendTTS);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Chanel";
            this.Text = "Lobby";
            this.Load += new System.EventHandler(this.Chanel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox RoomList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox ChatLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox PlayerList;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox RoomPlayerList;
        private System.Windows.Forms.TextBox SendTTS;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox ServerChat;
    }
}