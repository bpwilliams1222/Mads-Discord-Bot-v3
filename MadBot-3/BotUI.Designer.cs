namespace MadBot_3
{
    partial class frmDiscordBot
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
            this.grpMessaging = new System.Windows.Forms.GroupBox();
            this.grpChannelMessaging = new System.Windows.Forms.GroupBox();
            this.grpSelectChannel = new System.Windows.Forms.GroupBox();
            this.ddChannelSelection = new System.Windows.Forms.ComboBox();
            this.txtChanMessage = new System.Windows.Forms.TextBox();
            this.grpChannelMessage = new System.Windows.Forms.GroupBox();
            this.btnSendChannelMessage = new System.Windows.Forms.Button();
            this.grpUserMessaging = new System.Windows.Forms.GroupBox();
            this.btnSendUserMessage = new System.Windows.Forms.Button();
            this.grpUserMessage = new System.Windows.Forms.GroupBox();
            this.txtUserMessage = new System.Windows.Forms.TextBox();
            this.grpSelectUser = new System.Windows.Forms.GroupBox();
            this.ddUserSelection = new System.Windows.Forms.ComboBox();
            this.grpBotActions = new System.Windows.Forms.GroupBox();
            this.btnStopBot = new System.Windows.Forms.Button();
            this.btnStartBot = new System.Windows.Forms.Button();
            this.grpMessaging.SuspendLayout();
            this.grpChannelMessaging.SuspendLayout();
            this.grpSelectChannel.SuspendLayout();
            this.grpChannelMessage.SuspendLayout();
            this.grpUserMessaging.SuspendLayout();
            this.grpUserMessage.SuspendLayout();
            this.grpSelectUser.SuspendLayout();
            this.grpBotActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMessaging
            // 
            this.grpMessaging.Controls.Add(this.grpUserMessaging);
            this.grpMessaging.Controls.Add(this.grpChannelMessaging);
            this.grpMessaging.Location = new System.Drawing.Point(12, 12);
            this.grpMessaging.Name = "grpMessaging";
            this.grpMessaging.Size = new System.Drawing.Size(610, 270);
            this.grpMessaging.TabIndex = 0;
            this.grpMessaging.TabStop = false;
            this.grpMessaging.Text = "Messaging";
            // 
            // grpChannelMessaging
            // 
            this.grpChannelMessaging.Controls.Add(this.btnSendChannelMessage);
            this.grpChannelMessaging.Controls.Add(this.grpChannelMessage);
            this.grpChannelMessaging.Controls.Add(this.grpSelectChannel);
            this.grpChannelMessaging.Location = new System.Drawing.Point(7, 15);
            this.grpChannelMessaging.Name = "grpChannelMessaging";
            this.grpChannelMessaging.Size = new System.Drawing.Size(296, 249);
            this.grpChannelMessaging.TabIndex = 0;
            this.grpChannelMessaging.TabStop = false;
            this.grpChannelMessaging.Text = "Send Message to Channel";
            // 
            // grpSelectChannel
            // 
            this.grpSelectChannel.Controls.Add(this.ddChannelSelection);
            this.grpSelectChannel.Location = new System.Drawing.Point(7, 20);
            this.grpSelectChannel.Name = "grpSelectChannel";
            this.grpSelectChannel.Size = new System.Drawing.Size(283, 52);
            this.grpSelectChannel.TabIndex = 0;
            this.grpSelectChannel.TabStop = false;
            this.grpSelectChannel.Text = "Select Channel";
            // 
            // ddChannelSelection
            // 
            this.ddChannelSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddChannelSelection.FormattingEnabled = true;
            this.ddChannelSelection.Location = new System.Drawing.Point(6, 16);
            this.ddChannelSelection.Name = "ddChannelSelection";
            this.ddChannelSelection.Size = new System.Drawing.Size(271, 28);
            this.ddChannelSelection.TabIndex = 1;
            // 
            // txtChanMessage
            // 
            this.txtChanMessage.AcceptsReturn = true;
            this.txtChanMessage.AcceptsTab = true;
            this.txtChanMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanMessage.Location = new System.Drawing.Point(6, 19);
            this.txtChanMessage.Multiline = true;
            this.txtChanMessage.Name = "txtChanMessage";
            this.txtChanMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChanMessage.Size = new System.Drawing.Size(271, 115);
            this.txtChanMessage.TabIndex = 1;
            // 
            // grpChannelMessage
            // 
            this.grpChannelMessage.Controls.Add(this.txtChanMessage);
            this.grpChannelMessage.Location = new System.Drawing.Point(7, 70);
            this.grpChannelMessage.Name = "grpChannelMessage";
            this.grpChannelMessage.Size = new System.Drawing.Size(283, 140);
            this.grpChannelMessage.TabIndex = 1;
            this.grpChannelMessage.TabStop = false;
            this.grpChannelMessage.Text = "Message";
            // 
            // btnSendChannelMessage
            // 
            this.btnSendChannelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendChannelMessage.Location = new System.Drawing.Point(7, 214);
            this.btnSendChannelMessage.Name = "btnSendChannelMessage";
            this.btnSendChannelMessage.Size = new System.Drawing.Size(283, 29);
            this.btnSendChannelMessage.TabIndex = 2;
            this.btnSendChannelMessage.Text = "Send Message";
            this.btnSendChannelMessage.UseVisualStyleBackColor = true;
            this.btnSendChannelMessage.Click += new System.EventHandler(this.btnSendChannelMessage_Click);
            // 
            // grpUserMessaging
            // 
            this.grpUserMessaging.Controls.Add(this.btnSendUserMessage);
            this.grpUserMessaging.Controls.Add(this.grpUserMessage);
            this.grpUserMessaging.Controls.Add(this.grpSelectUser);
            this.grpUserMessaging.Location = new System.Drawing.Point(309, 15);
            this.grpUserMessaging.Name = "grpUserMessaging";
            this.grpUserMessaging.Size = new System.Drawing.Size(296, 249);
            this.grpUserMessaging.TabIndex = 3;
            this.grpUserMessaging.TabStop = false;
            this.grpUserMessaging.Text = "Send Message to User";
            // 
            // btnSendUserMessage
            // 
            this.btnSendUserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendUserMessage.Location = new System.Drawing.Point(7, 214);
            this.btnSendUserMessage.Name = "btnSendUserMessage";
            this.btnSendUserMessage.Size = new System.Drawing.Size(283, 29);
            this.btnSendUserMessage.TabIndex = 2;
            this.btnSendUserMessage.Text = "Send Message";
            this.btnSendUserMessage.UseVisualStyleBackColor = true;
            this.btnSendUserMessage.Click += new System.EventHandler(this.btnSendUserMessage_Click);
            // 
            // grpUserMessage
            // 
            this.grpUserMessage.Controls.Add(this.txtUserMessage);
            this.grpUserMessage.Location = new System.Drawing.Point(7, 70);
            this.grpUserMessage.Name = "grpUserMessage";
            this.grpUserMessage.Size = new System.Drawing.Size(283, 140);
            this.grpUserMessage.TabIndex = 1;
            this.grpUserMessage.TabStop = false;
            this.grpUserMessage.Text = "Message";
            // 
            // txtUserMessage
            // 
            this.txtUserMessage.AcceptsReturn = true;
            this.txtUserMessage.AcceptsTab = true;
            this.txtUserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserMessage.Location = new System.Drawing.Point(6, 19);
            this.txtUserMessage.Multiline = true;
            this.txtUserMessage.Name = "txtUserMessage";
            this.txtUserMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUserMessage.Size = new System.Drawing.Size(271, 115);
            this.txtUserMessage.TabIndex = 1;
            // 
            // grpSelectUser
            // 
            this.grpSelectUser.Controls.Add(this.ddUserSelection);
            this.grpSelectUser.Location = new System.Drawing.Point(7, 20);
            this.grpSelectUser.Name = "grpSelectUser";
            this.grpSelectUser.Size = new System.Drawing.Size(283, 52);
            this.grpSelectUser.TabIndex = 0;
            this.grpSelectUser.TabStop = false;
            this.grpSelectUser.Text = "Select User";
            // 
            // ddUserSelection
            // 
            this.ddUserSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddUserSelection.FormattingEnabled = true;
            this.ddUserSelection.Location = new System.Drawing.Point(6, 16);
            this.ddUserSelection.Name = "ddUserSelection";
            this.ddUserSelection.Size = new System.Drawing.Size(271, 28);
            this.ddUserSelection.TabIndex = 1;
            // 
            // grpBotActions
            // 
            this.grpBotActions.Controls.Add(this.btnStartBot);
            this.grpBotActions.Controls.Add(this.btnStopBot);
            this.grpBotActions.Location = new System.Drawing.Point(13, 289);
            this.grpBotActions.Name = "grpBotActions";
            this.grpBotActions.Size = new System.Drawing.Size(609, 79);
            this.grpBotActions.TabIndex = 1;
            this.grpBotActions.TabStop = false;
            this.grpBotActions.Text = "Bot Actions";
            // 
            // btnStopBot
            // 
            this.btnStopBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopBot.Location = new System.Drawing.Point(7, 20);
            this.btnStopBot.Name = "btnStopBot";
            this.btnStopBot.Size = new System.Drawing.Size(193, 44);
            this.btnStopBot.TabIndex = 0;
            this.btnStopBot.Text = "Shutdown Bot";
            this.btnStopBot.UseVisualStyleBackColor = true;
            this.btnStopBot.Click += new System.EventHandler(this.btnStopBot_Click);
            // 
            // btnStartBot
            // 
            this.btnStartBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartBot.Location = new System.Drawing.Point(206, 20);
            this.btnStartBot.Name = "btnStartBot";
            this.btnStartBot.Size = new System.Drawing.Size(193, 44);
            this.btnStartBot.TabIndex = 1;
            this.btnStartBot.Text = "Start Bot";
            this.btnStartBot.UseVisualStyleBackColor = true;
            this.btnStartBot.Click += new System.EventHandler(this.btnStartBot_Click);
            // 
            // frmDiscordBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 375);
            this.Controls.Add(this.grpBotActions);
            this.Controls.Add(this.grpMessaging);
            this.Name = "frmDiscordBot";
            this.Text = "Mads Discord Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpMessaging.ResumeLayout(false);
            this.grpChannelMessaging.ResumeLayout(false);
            this.grpSelectChannel.ResumeLayout(false);
            this.grpChannelMessage.ResumeLayout(false);
            this.grpChannelMessage.PerformLayout();
            this.grpUserMessaging.ResumeLayout(false);
            this.grpUserMessage.ResumeLayout(false);
            this.grpUserMessage.PerformLayout();
            this.grpSelectUser.ResumeLayout(false);
            this.grpBotActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMessaging;
        private System.Windows.Forms.GroupBox grpUserMessaging;
        private System.Windows.Forms.Button btnSendUserMessage;
        private System.Windows.Forms.GroupBox grpUserMessage;
        private System.Windows.Forms.TextBox txtUserMessage;
        private System.Windows.Forms.GroupBox grpSelectUser;
        private System.Windows.Forms.ComboBox ddUserSelection;
        private System.Windows.Forms.GroupBox grpChannelMessaging;
        private System.Windows.Forms.Button btnSendChannelMessage;
        private System.Windows.Forms.GroupBox grpChannelMessage;
        private System.Windows.Forms.TextBox txtChanMessage;
        private System.Windows.Forms.GroupBox grpSelectChannel;
        private System.Windows.Forms.ComboBox ddChannelSelection;
        private System.Windows.Forms.GroupBox grpBotActions;
        private System.Windows.Forms.Button btnStartBot;
        private System.Windows.Forms.Button btnStopBot;
    }
}

