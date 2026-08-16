namespace C3AP_Client
{
    partial class ClientMessageBox
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
            button4 = new Button();
            panel1 = new Panel();
            titleMessage = new Label();
            btn_sendcommand = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(50, 58, 75);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Image = Properties.Resources.exit_btn;
            button4.Location = new Point(329, 3);
            button4.Name = "button4";
            button4.Size = new Size(18, 18);
            button4.TabIndex = 16;
            button4.UseVisualStyleBackColor = false;
            button4.Click += btnOk_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(36, 40, 47);
            panel1.Controls.Add(titleMessage);
            panel1.Controls.Add(button4);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 25);
            panel1.TabIndex = 17;
            panel1.MouseDown += onMouseDown;
            panel1.MouseMove += onMouseMove;
            panel1.MouseUp += onMouseRelease;
            // 
            // titleMessage
            // 
            titleMessage.AutoSize = true;
            titleMessage.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleMessage.ForeColor = SystemColors.ButtonHighlight;
            titleMessage.Location = new Point(12, 3);
            titleMessage.Name = "titleMessage";
            titleMessage.Size = new Size(32, 17);
            titleMessage.TabIndex = 18;
            titleMessage.Text = "Title";
            titleMessage.MouseDown += onMouseDown;
            titleMessage.MouseMove += onMouseMove;
            titleMessage.MouseUp += onMouseRelease;
            // 
            // btn_sendcommand
            // 
            btn_sendcommand.BackColor = Color.FromArgb(50, 58, 75);
            btn_sendcommand.FlatAppearance.BorderSize = 0;
            btn_sendcommand.FlatStyle = FlatStyle.Flat;
            btn_sendcommand.ForeColor = SystemColors.ButtonHighlight;
            btn_sendcommand.Location = new Point(134, 115);
            btn_sendcommand.Name = "btn_sendcommand";
            btn_sendcommand.Size = new Size(75, 23);
            btn_sendcommand.TabIndex = 19;
            btn_sendcommand.Text = "OK";
            btn_sendcommand.UseVisualStyleBackColor = false;
            btn_sendcommand.Click += btnOk_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(12, 64);
            label1.Name = "label1";
            label1.Size = new Size(52, 21);
            label1.TabIndex = 20;
            label1.Text = "label1";
            // 
            // ClientMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(23, 29, 37);
            ClientSize = new Size(350, 150);
            Controls.Add(label1);
            Controls.Add(btn_sendcommand);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ClientMessageBox";
            Text = "ClientMessageBox";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button4;
        private Panel panel1;
        private Label titleMessage;
        private Button btn_sendcommand;
        private Label label1;
    }
}