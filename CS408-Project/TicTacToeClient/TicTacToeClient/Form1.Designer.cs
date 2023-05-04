namespace TicTacToeClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtbxIP = new TextBox();
            txtbxPort = new TextBox();
            ClientRichTxtBox = new RichTextBox();
            btnconnect = new Button();
            txtbxName = new TextBox();
            btnDisconnect = new Button();
            label4 = new Label();
            btnchoice = new Button();
            btnNameSend = new Button();
            txtbxchoice = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(44, 193);
            label1.Name = "label1";
            label1.Size = new Size(64, 21);
            label1.TabIndex = 0;
            label1.Text = "Name: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(58, 64);
            label2.Name = "label2";
            label2.Size = new Size(50, 21);
            label2.TabIndex = 1;
            label2.Text = "Port: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(75, 29);
            label3.Name = "label3";
            label3.Size = new Size(33, 21);
            label3.TabIndex = 2;
            label3.Text = "IP: ";
            // 
            // txtbxIP
            // 
            txtbxIP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxIP.Location = new Point(104, 26);
            txtbxIP.Name = "txtbxIP";
            txtbxIP.Size = new Size(110, 29);
            txtbxIP.TabIndex = 3;
            // 
            // txtbxPort
            // 
            txtbxPort.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxPort.Location = new Point(104, 61);
            txtbxPort.Name = "txtbxPort";
            txtbxPort.Size = new Size(110, 29);
            txtbxPort.TabIndex = 4;
            // 
            // ClientRichTxtBox
            // 
            ClientRichTxtBox.Location = new Point(430, 12);
            ClientRichTxtBox.Name = "ClientRichTxtBox";
            ClientRichTxtBox.ReadOnly = true;
            ClientRichTxtBox.Size = new Size(393, 385);
            ClientRichTxtBox.TabIndex = 5;
            ClientRichTxtBox.Text = "";
            // 
            // btnconnect
            // 
            btnconnect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnconnect.Location = new Point(230, 19);
            btnconnect.Name = "btnconnect";
            btnconnect.Size = new Size(93, 40);
            btnconnect.TabIndex = 6;
            btnconnect.Text = "CONNECT";
            btnconnect.UseVisualStyleBackColor = true;
            btnconnect.Click += btnconnect_Click;
            // 
            // txtbxName
            // 
            txtbxName.Enabled = false;
            txtbxName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxName.Location = new Point(114, 190);
            txtbxName.Name = "txtbxName";
            txtbxName.Size = new Size(110, 29);
            txtbxName.TabIndex = 7;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDisconnect.Location = new Point(230, 65);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(93, 40);
            btnDisconnect.TabIndex = 8;
            btnDisconnect.Text = "LEAVE";
            btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(99, 280);
            label4.Name = "label4";
            label4.Size = new Size(70, 21);
            label4.TabIndex = 9;
            label4.Text = "Choice: ";
            // 
            // btnchoice
            // 
            btnchoice.Enabled = false;
            btnchoice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnchoice.Location = new Point(230, 270);
            btnchoice.Name = "btnchoice";
            btnchoice.Size = new Size(93, 40);
            btnchoice.TabIndex = 11;
            btnchoice.Text = "Send";
            btnchoice.UseVisualStyleBackColor = true;
            // 
            // btnNameSend
            // 
            btnNameSend.Enabled = false;
            btnNameSend.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnNameSend.Location = new Point(230, 183);
            btnNameSend.Name = "btnNameSend";
            btnNameSend.Size = new Size(93, 40);
            btnNameSend.TabIndex = 12;
            btnNameSend.Text = "Send";
            btnNameSend.UseVisualStyleBackColor = true;
            btnNameSend.Click += btnNameSend_Click;
            // 
            // txtbxchoice
            // 
            txtbxchoice.Enabled = false;
            txtbxchoice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxchoice.Location = new Point(175, 277);
            txtbxchoice.Name = "txtbxchoice";
            txtbxchoice.Size = new Size(49, 29);
            txtbxchoice.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 483);
            Controls.Add(txtbxchoice);
            Controls.Add(btnNameSend);
            Controls.Add(btnchoice);
            Controls.Add(label4);
            Controls.Add(btnDisconnect);
            Controls.Add(txtbxName);
            Controls.Add(btnconnect);
            Controls.Add(ClientRichTxtBox);
            Controls.Add(txtbxPort);
            Controls.Add(txtbxIP);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtbxIP;
        private TextBox txtbxPort;
        private RichTextBox ClientRichTxtBox;
        private Button btnconnect;
        private TextBox txtbxName;
        private Button btnDisconnect;
        private Label label4;
        private Button btnchoice;
        private Button btnNameSend;
        private TextBox txtbxchoice;
    }
}