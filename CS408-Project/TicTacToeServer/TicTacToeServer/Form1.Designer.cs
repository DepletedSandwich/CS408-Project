namespace TicTacToeServer
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
            txtbxport = new TextBox();
            btnlisten = new Button();
            txtinfogame = new RichTextBox();
            btnstartgame = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(19, 36);
            label1.Name = "label1";
            label1.Size = new Size(46, 21);
            label1.TabIndex = 0;
            label1.Text = "Port:";
            // 
            // txtbxport
            // 
            txtbxport.Location = new Point(71, 36);
            txtbxport.Name = "txtbxport";
            txtbxport.Size = new Size(132, 23);
            txtbxport.TabIndex = 1;
            // 
            // btnlisten
            // 
            btnlisten.BackColor = SystemColors.ControlLight;
            btnlisten.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnlisten.ForeColor = SystemColors.ControlText;
            btnlisten.Location = new Point(209, 23);
            btnlisten.Name = "btnlisten";
            btnlisten.Size = new Size(95, 46);
            btnlisten.TabIndex = 2;
            btnlisten.Text = "Listen";
            btnlisten.UseVisualStyleBackColor = false;
            btnlisten.Click += btnlisten_Click;
            // 
            // txtinfogame
            // 
            txtinfogame.Location = new Point(329, 12);
            txtinfogame.Name = "txtinfogame";
            txtinfogame.ReadOnly = true;
            txtinfogame.Size = new Size(387, 441);
            txtinfogame.TabIndex = 3;
            txtinfogame.Text = "";
            // 
            // btnstartgame
            // 
            btnstartgame.Enabled = false;
            btnstartgame.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnstartgame.Location = new Point(154, 162);
            btnstartgame.Name = "btnstartgame";
            btnstartgame.Size = new Size(150, 50);
            btnstartgame.TabIndex = 4;
            btnstartgame.Text = "Create Game";
            btnstartgame.UseVisualStyleBackColor = true;
            btnstartgame.Click += btnstartgame_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(728, 465);
            Controls.Add(btnstartgame);
            Controls.Add(txtinfogame);
            Controls.Add(btnlisten);
            Controls.Add(txtbxport);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtbxport;
        private Button btnlisten;
        private RichTextBox txtinfogame;
        private Button btnstartgame;
    }
}