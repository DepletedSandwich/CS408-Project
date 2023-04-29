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
            label1.Location = new Point(50, 257);
            label1.Name = "label1";
            label1.Size = new Size(79, 28);
            label1.TabIndex = 0;
            label1.Text = "Name: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(66, 85);
            label2.Name = "label2";
            label2.Size = new Size(63, 28);
            label2.TabIndex = 1;
            label2.Text = "Port: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(86, 39);
            label3.Name = "label3";
            label3.Size = new Size(41, 28);
            label3.TabIndex = 2;
            label3.Text = "IP: ";
            // 
            // txtbxIP
            // 
            txtbxIP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxIP.Location = new Point(119, 35);
            txtbxIP.Margin = new Padding(3, 4, 3, 4);
            txtbxIP.Name = "txtbxIP";
            txtbxIP.Size = new Size(125, 34);
            txtbxIP.TabIndex = 3;
            // 
            // txtbxPort
            // 
            txtbxPort.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxPort.Location = new Point(119, 81);
            txtbxPort.Margin = new Padding(3, 4, 3, 4);
            txtbxPort.Name = "txtbxPort";
            txtbxPort.Size = new Size(125, 34);
            txtbxPort.TabIndex = 4;
            // 
            // ClientRichTxtBox
            // 
            ClientRichTxtBox.Location = new Point(491, 16);
            ClientRichTxtBox.Margin = new Padding(3, 4, 3, 4);
            ClientRichTxtBox.Name = "ClientRichTxtBox";
            ClientRichTxtBox.ReadOnly = true;
            ClientRichTxtBox.Size = new Size(449, 512);
            ClientRichTxtBox.TabIndex = 5;
            ClientRichTxtBox.Text = "";
            // 
            // btnconnect
            // 
            btnconnect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnconnect.Location = new Point(263, 25);
            btnconnect.Margin = new Padding(3, 4, 3, 4);
            btnconnect.Name = "btnconnect";
            btnconnect.Size = new Size(124, 53);
            btnconnect.TabIndex = 6;
            btnconnect.Text = "CONNECT";
            btnconnect.UseVisualStyleBackColor = true;
            btnconnect.Click += btnconnect_Click;
            // 
            // txtbxName
            // 
            txtbxName.Enabled = false;
            txtbxName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxName.Location = new Point(130, 253);
            txtbxName.Margin = new Padding(3, 4, 3, 4);
            txtbxName.Name = "txtbxName";
            txtbxName.Size = new Size(125, 34);
            txtbxName.TabIndex = 7;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDisconnect.Location = new Point(263, 87);
            btnDisconnect.Margin = new Padding(3, 4, 3, 4);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(124, 53);
            btnDisconnect.TabIndex = 8;
            btnDisconnect.Text = "LEAVE";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(113, 373);
            label4.Name = "label4";
            label4.Size = new Size(86, 28);
            label4.TabIndex = 9;
            label4.Text = "Choice: ";
            // 
            // btnchoice
            // 
            btnchoice.Enabled = false;
            btnchoice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnchoice.Location = new Point(263, 360);
            btnchoice.Margin = new Padding(3, 4, 3, 4);
            btnchoice.Name = "btnchoice";
            btnchoice.Size = new Size(106, 53);
            btnchoice.TabIndex = 11;
            btnchoice.Text = "Send";
            btnchoice.UseVisualStyleBackColor = true;
            btnchoice.Click += btnchoice_Click;
            // 
            // btnNameSend
            // 
            btnNameSend.Enabled = false;
            btnNameSend.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnNameSend.Location = new Point(263, 244);
            btnNameSend.Margin = new Padding(3, 4, 3, 4);
            btnNameSend.Name = "btnNameSend";
            btnNameSend.Size = new Size(106, 53);
            btnNameSend.TabIndex = 12;
            btnNameSend.Text = "Send";
            btnNameSend.UseVisualStyleBackColor = true;
            btnNameSend.Click += btnNameSend_Click;
            // 
            // txtbxchoice
            // 
            txtbxchoice.Enabled = false;
            txtbxchoice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtbxchoice.Location = new Point(200, 369);
            txtbxchoice.Margin = new Padding(3, 4, 3, 4);
            txtbxchoice.Name = "txtbxchoice";
            txtbxchoice.Size = new Size(55, 34);
            txtbxchoice.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 644);
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
            Margin = new Padding(3, 4, 3, 4);
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