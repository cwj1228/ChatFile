namespace ChatProgramClient
{
    partial class newID
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
            this.IDBOX = new System.Windows.Forms.TextBox();
            this.PWBOX = new System.Windows.Forms.TextBox();
            this.newIDCreate = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.GroupBox();
            this.NICKNAMEBOX = new System.Windows.Forms.TextBox();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // IDBOX
            // 
            this.IDBOX.Location = new System.Drawing.Point(6, 31);
            this.IDBOX.Name = "IDBOX";
            this.IDBOX.Size = new System.Drawing.Size(119, 21);
            this.IDBOX.TabIndex = 0;
            // 
            // PWBOX
            // 
            this.PWBOX.Location = new System.Drawing.Point(6, 58);
            this.PWBOX.Name = "PWBOX";
            this.PWBOX.Size = new System.Drawing.Size(119, 21);
            this.PWBOX.TabIndex = 1;
            // 
            // newIDCreate
            // 
            this.newIDCreate.Location = new System.Drawing.Point(132, 44);
            this.newIDCreate.Name = "newIDCreate";
            this.newIDCreate.Size = new System.Drawing.Size(62, 62);
            this.newIDCreate.TabIndex = 2;
            this.newIDCreate.Text = "Create";
            this.newIDCreate.UseVisualStyleBackColor = true;
            this.newIDCreate.Click += new System.EventHandler(this.newIDCreate_Click);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.NICKNAMEBOX);
            this.Panel.Controls.Add(this.PWBOX);
            this.Panel.Controls.Add(this.newIDCreate);
            this.Panel.Controls.Add(this.IDBOX);
            this.Panel.Location = new System.Drawing.Point(5, 3);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(200, 118);
            this.Panel.TabIndex = 3;
            this.Panel.TabStop = false;
            this.Panel.Text = "ID, PW, NICKNAME";
            // 
            // NICKNAMEBOX
            // 
            this.NICKNAMEBOX.Location = new System.Drawing.Point(6, 85);
            this.NICKNAMEBOX.Name = "NICKNAMEBOX";
            this.NICKNAMEBOX.Size = new System.Drawing.Size(119, 21);
            this.NICKNAMEBOX.TabIndex = 3;
            // 
            // newID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 132);
            this.Controls.Add(this.Panel);
            this.Name = "newID";
            this.Text = "newID";
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox IDBOX;
        private System.Windows.Forms.TextBox PWBOX;
        private System.Windows.Forms.TextBox NICKNAMEBOX;
        private System.Windows.Forms.Button newIDCreate;
        private System.Windows.Forms.GroupBox Panel;
    }
}