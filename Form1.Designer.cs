
namespace C3AP_Client
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
            components = new System.ComponentModel.Container();
            dsstatus = new Label();
            levelID = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            regionLabel = new Label();
            crystal = new Label();
            crystalReserve = new Label();
            logBox = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            timer2 = new System.Windows.Forms.Timer(components);
            timer3 = new System.Windows.Forms.Timer(components);
            portNumber = new TextBox();
            hostName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panelSettings = new Panel();
            button6 = new Button();
            panel8 = new Panel();
            label4 = new Label();
            panel7 = new Panel();
            slotName = new TextBox();
            panel6 = new Panel();
            password = new TextBox();
            panel5 = new Panel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            button3 = new Button();
            panel2 = new Panel();
            titleApp = new Label();
            button5 = new Button();
            button4 = new Button();
            commandBox = new TextBox();
            panel3 = new Panel();
            duck_status = new PictureBox();
            btn_game = new Panel();
            btn_labelgame = new Label();
            btn_selectgame = new Panel();
            sharedPanel = new Panel();
            panel_gamestats = new Panel();
            panelGame = new Panel();
            latelevelId = new Label();
            panel_ReceivedItems = new Panel();
            receivedItemBox = new RichTextBox();
            panel_Hints = new Panel();
            hintsBox = new RichTextBox();
            panel_Log = new Panel();
            panelLogColor = new Panel();
            btn_sendcommand = new Button();
            panel4 = new Panel();
            regionFlag = new PictureBox();
            panel10 = new Panel();
            btn_labelreceivedItems = new Label();
            btn_selectrecItems = new Panel();
            btn_hints = new Panel();
            btn_labelhints = new Label();
            btn_selecthints = new Panel();
            btn_log = new Panel();
            btn_labellog = new Label();
            btn_selectlog = new Panel();
            panelSettings.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)duck_status).BeginInit();
            btn_game.SuspendLayout();
            sharedPanel.SuspendLayout();
            panel_gamestats.SuspendLayout();
            panelGame.SuspendLayout();
            panel_ReceivedItems.SuspendLayout();
            panel_Hints.SuspendLayout();
            panel_Log.SuspendLayout();
            panelLogColor.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)regionFlag).BeginInit();
            panel10.SuspendLayout();
            btn_hints.SuspendLayout();
            btn_log.SuspendLayout();
            SuspendLayout();
            // 
            // dsstatus
            // 
            dsstatus.AutoSize = true;
            dsstatus.Location = new Point(51, 37);
            dsstatus.Name = "dsstatus";
            dsstatus.Size = new Size(188, 15);
            dsstatus.TabIndex = 0;
            dsstatus.Text = "Duckstation Status: Is not Running";
            // 
            // levelID
            // 
            levelID.AutoSize = true;
            levelID.Location = new Point(51, 63);
            levelID.Name = "levelID";
            levelID.Size = new Size(51, 15);
            levelID.TabIndex = 1;
            levelID.Text = "Level ID:";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // regionLabel
            // 
            regionLabel.AutoSize = true;
            regionLabel.Location = new Point(51, 78);
            regionLabel.Name = "regionLabel";
            regionLabel.Size = new Size(81, 15);
            regionLabel.TabIndex = 2;
            regionLabel.Text = "Game Region:";
            // 
            // crystal
            // 
            crystal.AutoSize = true;
            crystal.Location = new Point(51, 93);
            crystal.Name = "crystal";
            crystal.Size = new Size(46, 15);
            crystal.TabIndex = 3;
            crystal.Text = "Crystal:";
            // 
            // crystalReserve
            // 
            crystalReserve.AutoSize = true;
            crystalReserve.Location = new Point(51, 108);
            crystalReserve.Name = "crystalReserve";
            crystalReserve.Size = new Size(46, 15);
            crystalReserve.TabIndex = 4;
            crystalReserve.Text = "Crystal:";
            // 
            // logBox
            // 
            logBox.BackColor = Color.FromArgb(36, 40, 47);
            logBox.BorderStyle = BorderStyle.None;
            logBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            logBox.ForeColor = SystemColors.ButtonHighlight;
            logBox.HideSelection = false;
            logBox.Location = new Point(10, 10);
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.ScrollBars = RichTextBoxScrollBars.None;
            logBox.Size = new Size(498, 550);
            logBox.TabIndex = 6;
            logBox.Text = "This Archipelago Client is";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(50, 58, 75);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(15, 139);
            button1.Name = "button1";
            button1.Size = new Size(261, 39);
            button1.TabIndex = 7;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(84, 479);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // timer2
            // 
            timer2.Interval = 1000;
            timer2.Tick += timer2_Tick;
            // 
            // portNumber
            // 
            portNumber.Location = new Point(124, 366);
            portNumber.MaxLength = 5;
            portNumber.Name = "portNumber";
            portNumber.PlaceholderText = "00000";
            portNumber.Size = new Size(100, 23);
            portNumber.TabIndex = 9;
            portNumber.KeyPress += NumbersOnly;
            // 
            // hostName
            // 
            hostName.BackColor = Color.FromArgb(50, 58, 75);
            hostName.BorderStyle = BorderStyle.None;
            hostName.Font = new Font("Segoe UI", 10F);
            hostName.ForeColor = SystemColors.ButtonHighlight;
            hostName.Location = new Point(3, 3);
            hostName.Name = "hostName";
            hostName.Size = new Size(174, 18);
            hostName.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.ImageAlign = ContentAlignment.MiddleRight;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(45, 21);
            label1.TabIndex = 11;
            label1.Text = "Host:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.ImageAlign = ContentAlignment.MiddleRight;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(40, 21);
            label2.TabIndex = 12;
            label2.Text = "Slot:";
            // 
            // panelSettings
            // 
            panelSettings.BackColor = Color.FromArgb(23, 32, 46);
            panelSettings.Controls.Add(button6);
            panelSettings.Controls.Add(panel8);
            panelSettings.Controls.Add(panel7);
            panelSettings.Controls.Add(panel6);
            panelSettings.Controls.Add(panel5);
            panelSettings.Controls.Add(button2);
            panelSettings.Controls.Add(textBox1);
            panelSettings.Controls.Add(textBox2);
            panelSettings.Controls.Add(label3);
            panelSettings.Controls.Add(label1);
            panelSettings.Controls.Add(button1);
            panelSettings.Controls.Add(label2);
            panelSettings.Controls.Add(portNumber);
            panelSettings.Location = new Point(0, 33);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(0, 667);
            panelSettings.TabIndex = 16;
            panelSettings.Paint += panel1_Paint;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(50, 58, 75);
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.Location = new Point(15, 184);
            button6.Name = "button6";
            button6.Size = new Size(261, 39);
            button6.TabIndex = 21;
            button6.Text = "Save Information";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(36, 40, 47);
            panel8.Controls.Add(label4);
            panel8.Location = new Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(290, 25);
            panel8.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.ImageAlign = ContentAlignment.MiddleRight;
            label4.Location = new Point(112, 0);
            label4.Name = "label4";
            label4.Size = new Size(66, 21);
            label4.TabIndex = 21;
            label4.Text = "Settings";
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(50, 58, 75);
            panel7.Controls.Add(slotName);
            panel7.Location = new Point(96, 71);
            panel7.Name = "panel7";
            panel7.Size = new Size(180, 25);
            panel7.TabIndex = 19;
            // 
            // slotName
            // 
            slotName.BackColor = Color.FromArgb(50, 58, 75);
            slotName.BorderStyle = BorderStyle.None;
            slotName.Font = new Font("Segoe UI", 10F);
            slotName.ForeColor = SystemColors.ButtonHighlight;
            slotName.Location = new Point(3, 3);
            slotName.Name = "slotName";
            slotName.Size = new Size(174, 18);
            slotName.TabIndex = 10;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(50, 58, 75);
            panel6.Controls.Add(password);
            panel6.Location = new Point(97, 102);
            panel6.Name = "panel6";
            panel6.Size = new Size(180, 25);
            panel6.TabIndex = 18;
            // 
            // password
            // 
            password.BackColor = Color.FromArgb(50, 58, 75);
            password.BorderStyle = BorderStyle.None;
            password.Font = new Font("Segoe UI", 10F);
            password.ForeColor = SystemColors.ButtonHighlight;
            password.Location = new Point(3, 3);
            password.Name = "password";
            password.Size = new Size(174, 18);
            password.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(50, 58, 75);
            panel5.Controls.Add(hostName);
            panel5.Location = new Point(96, 40);
            panel5.Name = "panel5";
            panel5.Size = new Size(180, 25);
            panel5.TabIndex = 17;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(96, 291);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(152, 18);
            textBox1.TabIndex = 16;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(84, 335);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(152, 25);
            textBox2.TabIndex = 15;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.ImageAlign = ContentAlignment.MiddleRight;
            label3.Location = new Point(12, 102);
            label3.Name = "label3";
            label3.Size = new Size(79, 21);
            label3.TabIndex = 14;
            label3.Text = "Password:";
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(23, 29, 37);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = Properties.Resources.hammenu1;
            button3.Location = new Point(2, 2);
            button3.Name = "button3";
            button3.Size = new Size(28, 28);
            button3.TabIndex = 14;
            button3.UseVisualStyleBackColor = false;
            button3.Click += btnMenu;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(23, 29, 37);
            panel2.Controls.Add(titleApp);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 33);
            panel2.TabIndex = 15;
            panel2.MouseDown += onMouseDown;
            panel2.MouseMove += onMouseMove;
            panel2.MouseUp += onMouseRelease;
            // 
            // titleApp
            // 
            titleApp.AutoSize = true;
            titleApp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleApp.ForeColor = SystemColors.ButtonHighlight;
            titleApp.Location = new Point(98, 7);
            titleApp.Name = "titleApp";
            titleApp.Size = new Size(333, 21);
            titleApp.TabIndex = 17;
            titleApp.Text = "Archipelago Client [ Crash Bandicoot: Warped ]";
            titleApp.MouseDown += onMouseDown;
            titleApp.MouseMove += onMouseMove;
            titleApp.MouseUp += onMouseRelease;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(50, 58, 75);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Image = Properties.Resources.min_btn;
            button5.Location = new Point(500, 7);
            button5.Name = "button5";
            button5.Size = new Size(18, 18);
            button5.TabIndex = 16;
            button5.UseVisualStyleBackColor = false;
            button5.Click += minApplication;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(50, 58, 75);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Image = Properties.Resources.exit_btn;
            button4.Location = new Point(524, 7);
            button4.Name = "button4";
            button4.Size = new Size(18, 18);
            button4.TabIndex = 15;
            button4.UseVisualStyleBackColor = false;
            button4.Click += closeApplication;
            // 
            // commandBox
            // 
            commandBox.BackColor = Color.FromArgb(50, 58, 75);
            commandBox.BorderStyle = BorderStyle.None;
            commandBox.ForeColor = SystemColors.ButtonHighlight;
            commandBox.Location = new Point(3, 4);
            commandBox.Name = "commandBox";
            commandBox.Size = new Size(431, 16);
            commandBox.TabIndex = 15;
            // 
            // panel3
            // 
            panel3.Controls.Add(duck_status);
            panel3.Controls.Add(btn_game);
            panel3.Controls.Add(sharedPanel);
            panel3.Controls.Add(regionFlag);
            panel3.Controls.Add(panel10);
            panel3.Controls.Add(btn_hints);
            panel3.Controls.Add(btn_log);
            panel3.ForeColor = SystemColors.ButtonHighlight;
            panel3.Location = new Point(12, 39);
            panel3.Name = "panel3";
            panel3.Size = new Size(530, 649);
            panel3.TabIndex = 16;
            // 
            // duck_status
            // 
            duck_status.Image = Properties.Resources.duckstation_off;
            duck_status.Location = new Point(434, 9);
            duck_status.Name = "duck_status";
            duck_status.Size = new Size(31, 31);
            duck_status.SizeMode = PictureBoxSizeMode.Zoom;
            duck_status.TabIndex = 26;
            duck_status.TabStop = false;
            // 
            // btn_game
            // 
            btn_game.Controls.Add(btn_labelgame);
            btn_game.Controls.Add(btn_selectgame);
            btn_game.Location = new Point(314, 3);
            btn_game.Name = "btn_game";
            btn_game.Size = new Size(71, 37);
            btn_game.TabIndex = 25;
            btn_game.Click += btnGame_Click;
            // 
            // btn_labelgame
            // 
            btn_labelgame.AutoSize = true;
            btn_labelgame.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_labelgame.ForeColor = Color.Gray;
            btn_labelgame.Location = new Point(3, 1);
            btn_labelgame.Name = "btn_labelgame";
            btn_labelgame.Size = new Size(67, 30);
            btn_labelgame.TabIndex = 1;
            btn_labelgame.Text = "Game";
            btn_labelgame.Click += btnGame_Click;
            btn_labelgame.MouseLeave += btnGameStats_Leave;
            btn_labelgame.MouseHover += btnGameStats_Hover;
            // 
            // btn_selectgame
            // 
            btn_selectgame.BackColor = Color.FromArgb(128, 255, 128);
            btn_selectgame.Location = new Point(0, 32);
            btn_selectgame.Name = "btn_selectgame";
            btn_selectgame.Size = new Size(162, 2);
            btn_selectgame.TabIndex = 0;
            btn_selectgame.Visible = false;
            btn_selectgame.Click += btnGame_Click;
            // 
            // sharedPanel
            // 
            sharedPanel.Controls.Add(panel_gamestats);
            sharedPanel.Controls.Add(panel_ReceivedItems);
            sharedPanel.Controls.Add(panel_Hints);
            sharedPanel.Controls.Add(panel_Log);
            sharedPanel.Location = new Point(3, 43);
            sharedPanel.Name = "sharedPanel";
            sharedPanel.Size = new Size(524, 603);
            sharedPanel.TabIndex = 19;
            // 
            // panel_gamestats
            // 
            panel_gamestats.Controls.Add(panelGame);
            panel_gamestats.Location = new Point(332, 18);
            panel_gamestats.Name = "panel_gamestats";
            panel_gamestats.Size = new Size(524, 603);
            panel_gamestats.TabIndex = 21;
            // 
            // panelGame
            // 
            panelGame.BackColor = Color.FromArgb(36, 40, 47);
            panelGame.Controls.Add(latelevelId);
            panelGame.Controls.Add(levelID);
            panelGame.Controls.Add(regionLabel);
            panelGame.Controls.Add(crystal);
            panelGame.Controls.Add(crystalReserve);
            panelGame.Controls.Add(dsstatus);
            panelGame.Location = new Point(3, 3);
            panelGame.Name = "panelGame";
            panelGame.Size = new Size(518, 597);
            panelGame.TabIndex = 0;
            // 
            // latelevelId
            // 
            latelevelId.AutoSize = true;
            latelevelId.Location = new Point(54, 128);
            latelevelId.Name = "latelevelId";
            latelevelId.Size = new Size(51, 15);
            latelevelId.TabIndex = 5;
            latelevelId.Text = "Level ID:";
            // 
            // panel_ReceivedItems
            // 
            panel_ReceivedItems.Controls.Add(receivedItemBox);
            panel_ReceivedItems.Location = new Point(320, 36);
            panel_ReceivedItems.Name = "panel_ReceivedItems";
            panel_ReceivedItems.Size = new Size(524, 603);
            panel_ReceivedItems.TabIndex = 20;
            // 
            // receivedItemBox
            // 
            receivedItemBox.BackColor = Color.FromArgb(36, 40, 47);
            receivedItemBox.BorderStyle = BorderStyle.None;
            receivedItemBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            receivedItemBox.ForeColor = SystemColors.ButtonHighlight;
            receivedItemBox.HideSelection = false;
            receivedItemBox.Location = new Point(3, 3);
            receivedItemBox.Name = "receivedItemBox";
            receivedItemBox.ReadOnly = true;
            receivedItemBox.ScrollBars = RichTextBoxScrollBars.None;
            receivedItemBox.Size = new Size(518, 597);
            receivedItemBox.TabIndex = 6;
            receivedItemBox.Text = "";
            // 
            // panel_Hints
            // 
            panel_Hints.Controls.Add(hintsBox);
            panel_Hints.Location = new Point(280, 112);
            panel_Hints.Name = "panel_Hints";
            panel_Hints.Size = new Size(524, 603);
            panel_Hints.TabIndex = 19;
            // 
            // hintsBox
            // 
            hintsBox.BackColor = Color.FromArgb(36, 40, 47);
            hintsBox.BorderStyle = BorderStyle.None;
            hintsBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hintsBox.ForeColor = SystemColors.ButtonHighlight;
            hintsBox.HideSelection = false;
            hintsBox.Location = new Point(3, 3);
            hintsBox.Name = "hintsBox";
            hintsBox.ReadOnly = true;
            hintsBox.ScrollBars = RichTextBoxScrollBars.None;
            hintsBox.Size = new Size(518, 597);
            hintsBox.TabIndex = 6;
            hintsBox.Text = "";
            // 
            // panel_Log
            // 
            panel_Log.Controls.Add(panelLogColor);
            panel_Log.Controls.Add(btn_sendcommand);
            panel_Log.Controls.Add(panel4);
            panel_Log.Location = new Point(3, 3);
            panel_Log.Name = "panel_Log";
            panel_Log.Size = new Size(524, 603);
            panel_Log.TabIndex = 18;
            // 
            // panelLogColor
            // 
            panelLogColor.BackColor = Color.FromArgb(36, 40, 47);
            panelLogColor.Controls.Add(logBox);
            panelLogColor.Location = new Point(3, 3);
            panelLogColor.Name = "panelLogColor";
            panelLogColor.Size = new Size(518, 568);
            panelLogColor.TabIndex = 18;
            // 
            // btn_sendcommand
            // 
            btn_sendcommand.BackColor = Color.FromArgb(50, 58, 75);
            btn_sendcommand.FlatAppearance.BorderSize = 0;
            btn_sendcommand.FlatStyle = FlatStyle.Flat;
            btn_sendcommand.ForeColor = SystemColors.ButtonHighlight;
            btn_sendcommand.Location = new Point(446, 577);
            btn_sendcommand.Name = "btn_sendcommand";
            btn_sendcommand.Size = new Size(75, 23);
            btn_sendcommand.TabIndex = 16;
            btn_sendcommand.Text = "Send";
            btn_sendcommand.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(50, 58, 75);
            panel4.Controls.Add(commandBox);
            panel4.Location = new Point(3, 577);
            panel4.Name = "panel4";
            panel4.Size = new Size(437, 23);
            panel4.TabIndex = 17;
            // 
            // regionFlag
            // 
            regionFlag.Image = Properties.Resources.EU;
            regionFlag.Location = new Point(471, 9);
            regionFlag.Name = "regionFlag";
            regionFlag.Size = new Size(56, 31);
            regionFlag.SizeMode = PictureBoxSizeMode.Zoom;
            regionFlag.TabIndex = 25;
            regionFlag.TabStop = false;
            // 
            // panel10
            // 
            panel10.Controls.Add(btn_labelreceivedItems);
            panel10.Controls.Add(btn_selectrecItems);
            panel10.Location = new Point(146, 3);
            panel10.Name = "panel10";
            panel10.Size = new Size(162, 37);
            panel10.TabIndex = 24;
            panel10.Click += btnReceivedItems_Click;
            // 
            // btn_labelreceivedItems
            // 
            btn_labelreceivedItems.AutoSize = true;
            btn_labelreceivedItems.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_labelreceivedItems.ForeColor = Color.Gray;
            btn_labelreceivedItems.Location = new Point(5, 1);
            btn_labelreceivedItems.Name = "btn_labelreceivedItems";
            btn_labelreceivedItems.Size = new Size(152, 30);
            btn_labelreceivedItems.TabIndex = 1;
            btn_labelreceivedItems.Text = "Received Items";
            btn_labelreceivedItems.Click += btnReceivedItems_Click;
            btn_labelreceivedItems.MouseLeave += btnReceivedItems_Leave;
            btn_labelreceivedItems.MouseHover += btnReceivedItemsHover_Click;
            // 
            // btn_selectrecItems
            // 
            btn_selectrecItems.BackColor = Color.FromArgb(128, 255, 128);
            btn_selectrecItems.Location = new Point(0, 32);
            btn_selectrecItems.Name = "btn_selectrecItems";
            btn_selectrecItems.Size = new Size(162, 2);
            btn_selectrecItems.TabIndex = 0;
            btn_selectrecItems.Visible = false;
            btn_selectrecItems.Click += btnReceivedItems_Click;
            // 
            // btn_hints
            // 
            btn_hints.Controls.Add(btn_labelhints);
            btn_hints.Controls.Add(btn_selecthints);
            btn_hints.Location = new Point(65, 3);
            btn_hints.Name = "btn_hints";
            btn_hints.Size = new Size(75, 37);
            btn_hints.TabIndex = 23;
            btn_hints.Click += btnHints_Click;
            // 
            // btn_labelhints
            // 
            btn_labelhints.AutoSize = true;
            btn_labelhints.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_labelhints.ForeColor = Color.Gray;
            btn_labelhints.Location = new Point(7, 1);
            btn_labelhints.Name = "btn_labelhints";
            btn_labelhints.Size = new Size(61, 30);
            btn_labelhints.TabIndex = 1;
            btn_labelhints.Text = "Hints";
            btn_labelhints.Click += btnHints_Click;
            btn_labelhints.MouseLeave += btnHints_Leave;
            btn_labelhints.MouseHover += btnHintsHover_Click;
            // 
            // btn_selecthints
            // 
            btn_selecthints.BackColor = Color.FromArgb(128, 255, 128);
            btn_selecthints.Location = new Point(0, 32);
            btn_selecthints.Name = "btn_selecthints";
            btn_selecthints.Size = new Size(75, 2);
            btn_selecthints.TabIndex = 0;
            btn_selecthints.Visible = false;
            btn_selecthints.Click += btnHints_Click;
            // 
            // btn_log
            // 
            btn_log.Controls.Add(btn_labellog);
            btn_log.Controls.Add(btn_selectlog);
            btn_log.Location = new Point(3, 3);
            btn_log.Name = "btn_log";
            btn_log.Size = new Size(56, 37);
            btn_log.TabIndex = 22;
            btn_log.Click += btnLog_Click;
            // 
            // btn_labellog
            // 
            btn_labellog.AutoSize = true;
            btn_labellog.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_labellog.ForeColor = SystemColors.ButtonHighlight;
            btn_labellog.Location = new Point(6, 1);
            btn_labellog.Name = "btn_labellog";
            btn_labellog.Size = new Size(47, 30);
            btn_labellog.TabIndex = 1;
            btn_labellog.Text = "Log";
            btn_labellog.Click += btnLog_Click;
            btn_labellog.MouseLeave += btnLog_Leave;
            btn_labellog.MouseHover += btnLogHover_Click;
            // 
            // btn_selectlog
            // 
            btn_selectlog.BackColor = Color.FromArgb(128, 255, 128);
            btn_selectlog.Location = new Point(0, 32);
            btn_selectlog.Name = "btn_selectlog";
            btn_selectlog.Size = new Size(75, 2);
            btn_selectlog.TabIndex = 0;
            btn_selectlog.Click += btnLog_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(23, 29, 37);
            ClientSize = new Size(550, 700);
            Controls.Add(panelSettings);
            Controls.Add(panel3);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)duck_status).EndInit();
            btn_game.ResumeLayout(false);
            btn_game.PerformLayout();
            sharedPanel.ResumeLayout(false);
            panel_gamestats.ResumeLayout(false);
            panelGame.ResumeLayout(false);
            panelGame.PerformLayout();
            panel_ReceivedItems.ResumeLayout(false);
            panel_Hints.ResumeLayout(false);
            panel_Log.ResumeLayout(false);
            panelLogColor.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)regionFlag).EndInit();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            btn_hints.ResumeLayout(false);
            btn_hints.PerformLayout();
            btn_log.ResumeLayout(false);
            btn_log.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label dsstatus;
        private Label levelID;
        private System.Windows.Forms.Timer timer1;
        private Label regionLabel;
        private Label crystal;
        private Label crystalReserve;
        private RichTextBox logBox;
        private Button button1;
        private Button button2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private TextBox portNumber;
        private TextBox hostName;
        private Label label1;
        private Label label2;
        private Panel panelSettings;
        private Button button3;
        private Label label3;
        private Panel panel2;
        private Button button5;
        private Button button4;
        private TextBox commandBox;
        private Panel panel3;
        private Button btn_sendcommand;
        private Panel panel4;
        private Panel panel_Log;
        private Panel btn_log;
        private Panel btn_selectlog;
        private Label btn_labellog;
        private Panel btn_hints;
        private Label btn_labelhints;
        private Panel btn_selecthints;
        private Panel panel10;
        private Label btn_labelreceivedItems;
        private Panel btn_selectrecItems;
        private PictureBox regionFlag;
        private Panel panel_Hints;
        private RichTextBox hintsBox;
        private Panel sharedPanel;
        private Panel panel_ReceivedItems;
        private RichTextBox receivedItemBox;
        private Panel panel_gamestats;
        private Panel panelGame;
        private Panel btn_game;
        private Label btn_labelgame;
        private Panel btn_selectgame;
        private PictureBox duck_status;
        private Label titleApp;
        private TextBox textBox1;
        private TextBox textBox2;
        private Panel panel6;
        private TextBox password;
        private Panel panel5;
        private Panel panel7;
        private TextBox slotName;
        private Panel panel8;
        private Label label4;
        private Panel panelLogColor;
        private Button button6;
        private Label latelevelId;
    }
}
