namespace MCTester.ButtonsImplementation
{
    partial class btnSignToSecurityServer
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.btnGetUsers = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCurrentToken = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Security Server Path:";
            // 
            // txtServerPath
            // 
            this.txtServerPath.Location = new System.Drawing.Point(161, 28);
            this.txtServerPath.Name = "txtServerPath";
            this.txtServerPath.Size = new System.Drawing.Size(245, 22);
            this.txtServerPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Users";
            // 
            // cbUsers
            // 
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(161, 70);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(134, 24);
            this.cbUsers.TabIndex = 3;
            this.cbUsers.SelectedIndexChanged += new System.EventHandler(this.cbUsers_SelectedIndexChanged);
            // 
            // btnSignIn
            // 
            this.btnSignIn.Enabled = false;
            this.btnSignIn.Location = new System.Drawing.Point(415, 67);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(82, 29);
            this.btnSignIn.TabIndex = 4;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnGetUsers
            // 
            this.btnGetUsers.Location = new System.Drawing.Point(415, 25);
            this.btnGetUsers.Name = "btnGetUsers";
            this.btnGetUsers.Size = new System.Drawing.Size(82, 29);
            this.btnGetUsers.TabIndex = 5;
            this.btnGetUsers.Text = "Get Users";
            this.btnGetUsers.UseVisualStyleBackColor = true;
            this.btnGetUsers.Click += new System.EventHandler(this.btnGetUsers_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(13, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Current User:";
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Enabled = false;
            this.lblCurrentUser.Location = new System.Drawing.Point(158, 108);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(93, 17);
            this.lblCurrentUser.TabIndex = 7;
            this.lblCurrentUser.Text = "Current User:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(13, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Current Token:";
            // 
            // txtCurrentToken
            // 
            this.txtCurrentToken.Location = new System.Drawing.Point(161, 134);
            this.txtCurrentToken.Multiline = true;
            this.txtCurrentToken.Name = "txtCurrentToken";
            this.txtCurrentToken.ReadOnly = true;
            this.txtCurrentToken.Size = new System.Drawing.Size(245, 75);
            this.txtCurrentToken.TabIndex = 10;
            // 
            // btnSignToSecurityServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(509, 212);
            this.Controls.Add(this.txtCurrentToken);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGetUsers);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerPath);
            this.Controls.Add(this.label1);
            this.Name = "btnSignToSecurityServer";
            this.Text = "btnSignToSecurityServer";
            this.Load += new System.EventHandler(this.btnSignToSecurityServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Button btnGetUsers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCurrentToken;
    }
}