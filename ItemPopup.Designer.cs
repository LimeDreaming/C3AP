namespace C3AP_Client
{
    partial class ItemPopup
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
            panel1 = new Panel();
            itemLocation = new Label();
            itemName = new Label();
            itemIcon = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemIcon).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gray;
            panel1.Controls.Add(itemLocation);
            panel1.Controls.Add(itemName);
            panel1.Controls.Add(itemIcon);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 120);
            panel1.TabIndex = 0;
            // 
            // itemLocation
            // 
            itemLocation.AutoSize = true;
            itemLocation.Font = new Font("Crash-a-Like", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            itemLocation.Location = new Point(171, 68);
            itemLocation.Name = "itemLocation";
            itemLocation.Size = new Size(111, 29);
            itemLocation.TabIndex = 2;
            itemLocation.Text = "Cyrstal";
            // 
            // itemName
            // 
            itemName.AutoSize = true;
            itemName.Font = new Font("Crash-a-Like", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            itemName.Location = new Point(171, 22);
            itemName.Name = "itemName";
            itemName.Size = new Size(111, 29);
            itemName.TabIndex = 1;
            itemName.Text = "Cyrstal";
            // 
            // itemIcon
            // 
            itemIcon.Location = new Point(22, 22);
            itemIcon.Name = "itemIcon";
            itemIcon.Size = new Size(75, 75);
            itemIcon.TabIndex = 0;
            itemIcon.TabStop = false;
            // 
            // ItemPopup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GrayText;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ItemPopup";
            ShowInTaskbar = false;
            Text = "ItemPopup";
            TopMost = true;
            TransparencyKey = SystemColors.GrayText;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)itemIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox itemIcon;
        private Label itemName;
        private Label itemLocation;
    }
}