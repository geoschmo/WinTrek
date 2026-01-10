namespace WinTrek
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpGalacticRecord = new System.Windows.Forms.GroupBox();
            this.btnCloseGalRec = new System.Windows.Forms.Button();
            this.txtGalRec = new System.Windows.Forms.RichTextBox();
            this.grpHelm = new System.Windows.Forms.GroupBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtDocked = new System.Windows.Forms.Label();
            this.lblDocked = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.Label();
            this.txtTorpedoesRemaining = new System.Windows.Forms.Label();
            this.lblTorpedoesRemaining = new System.Windows.Forms.Label();
            this.lblQuadrant = new System.Windows.Forms.Label();
            this.txtQuadrant = new System.Windows.Forms.Label();
            this.txtShieldLevel = new System.Windows.Forms.Label();
            this.lblShields = new System.Windows.Forms.Label();
            this.lblSector = new System.Windows.Forms.Label();
            this.txtSector = new System.Windows.Forms.Label();
            this.txtEnergy = new System.Windows.Forms.Label();
            this.lblEnergy = new System.Windows.Forms.Label();
            this.lblStardate = new System.Windows.Forms.Label();
            this.txtStardate = new System.Windows.Forms.Label();
            this.txtCondition = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.txtTimeRemaining = new System.Windows.Forms.Label();
            this.grpSensors = new System.Windows.Forms.GroupBox();
            this.btnLongRangeScan = new System.Windows.Forms.Button();
            this.btnShortRangeScan = new System.Windows.Forms.Button();
            this.grpComputer = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btnBaseCalc = new System.Windows.Forms.Button();
            this.btnTorpCalc = new System.Windows.Forms.Button();
            this.btnStatusReport = new System.Windows.Forms.Button();
            this.btnGalRec = new System.Windows.Forms.Button();
            this.grpShields = new System.Windows.Forms.GroupBox();
            this.btnSubShield = new System.Windows.Forms.Button();
            this.btnAddShield = new System.Windows.Forms.Button();
            this.txtUpdateShields = new System.Windows.Forms.TextBox();
            this.grpShortRangeScan = new System.Windows.Forms.GroupBox();
            this.txtSRS = new System.Windows.Forms.RichTextBox();
            this.grpNavigation = new System.Windows.Forms.GroupBox();
            this.btnEngage = new System.Windows.Forms.Button();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.txtDirection = new System.Windows.Forms.TextBox();
            this.grpCommunications = new System.Windows.Forms.GroupBox();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.grpTactical = new System.Windows.Forms.GroupBox();
            this.btnFire = new System.Windows.Forms.Button();
            this.grpTorpedoes = new System.Windows.Forms.GroupBox();
            this.lblTorpedoes = new System.Windows.Forms.Label();
            this.txtTorpedoes = new System.Windows.Forms.TextBox();
            this.grpPhasers = new System.Windows.Forms.GroupBox();
            this.lblPhasers = new System.Windows.Forms.Label();
            this.txtPhasers = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.grpGalacticRecord.SuspendLayout();
            this.grpHelm.SuspendLayout();
            this.grpSensors.SuspendLayout();
            this.grpComputer.SuspendLayout();
            this.grpShields.SuspendLayout();
            this.grpShortRangeScan.SuspendLayout();
            this.grpNavigation.SuspendLayout();
            this.grpCommunications.SuspendLayout();
            this.grpTactical.SuspendLayout();
            this.grpTorpedoes.SuspendLayout();
            this.grpPhasers.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(966, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpGalacticRecord);
            this.pnlMain.Controls.Add(this.grpHelm);
            this.pnlMain.Controls.Add(this.grpSensors);
            this.pnlMain.Controls.Add(this.grpComputer);
            this.pnlMain.Controls.Add(this.grpShields);
            this.pnlMain.Controls.Add(this.grpShortRangeScan);
            this.pnlMain.Controls.Add(this.grpNavigation);
            this.pnlMain.Controls.Add(this.grpCommunications);
            this.pnlMain.Controls.Add(this.grpTactical);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(966, 345);
            this.pnlMain.TabIndex = 1;
            // 
            // grpGalacticRecord
            // 
            this.grpGalacticRecord.Controls.Add(this.btnCloseGalRec);
            this.grpGalacticRecord.Controls.Add(this.txtGalRec);
            this.grpGalacticRecord.Location = new System.Drawing.Point(530, 3);
            this.grpGalacticRecord.Name = "grpGalacticRecord";
            this.grpGalacticRecord.Size = new System.Drawing.Size(413, 265);
            this.grpGalacticRecord.TabIndex = 32;
            this.grpGalacticRecord.TabStop = false;
            this.grpGalacticRecord.Text = "Cumulative Galactic Record";
            this.grpGalacticRecord.Visible = false;
            // 
            // btnCloseGalRec
            // 
            this.btnCloseGalRec.Location = new System.Drawing.Point(356, 119);
            this.btnCloseGalRec.Name = "btnCloseGalRec";
            this.btnCloseGalRec.Size = new System.Drawing.Size(51, 27);
            this.btnCloseGalRec.TabIndex = 3;
            this.btnCloseGalRec.Text = "Close";
            this.btnCloseGalRec.UseVisualStyleBackColor = true;
            this.btnCloseGalRec.Click += new System.EventHandler(this.btnCloseGalRec_Click);
            // 
            // txtGalRec
            // 
            this.txtGalRec.BackColor = System.Drawing.Color.Black;
            this.txtGalRec.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGalRec.ForeColor = System.Drawing.Color.White;
            this.txtGalRec.Location = new System.Drawing.Point(4, 16);
            this.txtGalRec.Name = "txtGalRec";
            this.txtGalRec.ReadOnly = true;
            this.txtGalRec.Size = new System.Drawing.Size(352, 244);
            this.txtGalRec.TabIndex = 2;
            this.txtGalRec.Text = "";
            // 
            // grpHelm
            // 
            this.grpHelm.Controls.Add(this.lblRegion);
            this.grpHelm.Controls.Add(this.txtDocked);
            this.grpHelm.Controls.Add(this.lblDocked);
            this.grpHelm.Controls.Add(this.txtRegion);
            this.grpHelm.Controls.Add(this.txtTorpedoesRemaining);
            this.grpHelm.Controls.Add(this.lblTorpedoesRemaining);
            this.grpHelm.Controls.Add(this.lblQuadrant);
            this.grpHelm.Controls.Add(this.txtQuadrant);
            this.grpHelm.Controls.Add(this.txtShieldLevel);
            this.grpHelm.Controls.Add(this.lblShields);
            this.grpHelm.Controls.Add(this.lblSector);
            this.grpHelm.Controls.Add(this.txtSector);
            this.grpHelm.Controls.Add(this.txtEnergy);
            this.grpHelm.Controls.Add(this.lblEnergy);
            this.grpHelm.Controls.Add(this.lblStardate);
            this.grpHelm.Controls.Add(this.txtStardate);
            this.grpHelm.Controls.Add(this.txtCondition);
            this.grpHelm.Controls.Add(this.lblCondition);
            this.grpHelm.Controls.Add(this.lblTimeRemaining);
            this.grpHelm.Controls.Add(this.txtTimeRemaining);
            this.grpHelm.Location = new System.Drawing.Point(192, 3);
            this.grpHelm.Name = "grpHelm";
            this.grpHelm.Size = new System.Drawing.Size(224, 201);
            this.grpHelm.TabIndex = 31;
            this.grpHelm.TabStop = false;
            this.grpHelm.Text = "Helm";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(55, 18);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(44, 13);
            this.lblRegion.TabIndex = 7;
            this.lblRegion.Text = "Region:";
            // 
            // txtDocked
            // 
            this.txtDocked.AutoSize = true;
            this.txtDocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocked.Location = new System.Drawing.Point(105, 180);
            this.txtDocked.Name = "txtDocked";
            this.txtDocked.Size = new System.Drawing.Size(55, 13);
            this.txtDocked.TabIndex = 26;
            this.txtDocked.Text = "Docked:";
            // 
            // lblDocked
            // 
            this.lblDocked.AutoSize = true;
            this.lblDocked.Location = new System.Drawing.Point(51, 180);
            this.lblDocked.Name = "lblDocked";
            this.lblDocked.Size = new System.Drawing.Size(48, 13);
            this.lblDocked.TabIndex = 16;
            this.lblDocked.Text = "Docked:";
            // 
            // txtRegion
            // 
            this.txtRegion.AutoSize = true;
            this.txtRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegion.Location = new System.Drawing.Point(105, 18);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(110, 13);
            this.txtRegion.TabIndex = 17;
            this.txtRegion.Text = "Epsilon Caneris III";
            // 
            // txtTorpedoesRemaining
            // 
            this.txtTorpedoesRemaining.AutoSize = true;
            this.txtTorpedoesRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTorpedoesRemaining.Location = new System.Drawing.Point(105, 162);
            this.txtTorpedoesRemaining.Name = "txtTorpedoesRemaining";
            this.txtTorpedoesRemaining.Size = new System.Drawing.Size(115, 13);
            this.txtTorpedoesRemaining.TabIndex = 25;
            this.txtTorpedoesRemaining.Text = "Photon Torpedoes:";
            // 
            // lblTorpedoesRemaining
            // 
            this.lblTorpedoesRemaining.AutoSize = true;
            this.lblTorpedoesRemaining.Location = new System.Drawing.Point(1, 162);
            this.lblTorpedoesRemaining.Name = "lblTorpedoesRemaining";
            this.lblTorpedoesRemaining.Size = new System.Drawing.Size(98, 13);
            this.lblTorpedoesRemaining.TabIndex = 15;
            this.lblTorpedoesRemaining.Text = "Photon Torpedoes:";
            // 
            // lblQuadrant
            // 
            this.lblQuadrant.AutoSize = true;
            this.lblQuadrant.Location = new System.Drawing.Point(45, 36);
            this.lblQuadrant.Name = "lblQuadrant";
            this.lblQuadrant.Size = new System.Drawing.Size(54, 13);
            this.lblQuadrant.TabIndex = 8;
            this.lblQuadrant.Text = "Quadrant:";
            // 
            // txtQuadrant
            // 
            this.txtQuadrant.AutoSize = true;
            this.txtQuadrant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuadrant.Location = new System.Drawing.Point(105, 36);
            this.txtQuadrant.Name = "txtQuadrant";
            this.txtQuadrant.Size = new System.Drawing.Size(63, 13);
            this.txtQuadrant.TabIndex = 18;
            this.txtQuadrant.Text = "Quadrant:";
            // 
            // txtShieldLevel
            // 
            this.txtShieldLevel.AutoSize = true;
            this.txtShieldLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShieldLevel.Location = new System.Drawing.Point(105, 144);
            this.txtShieldLevel.Name = "txtShieldLevel";
            this.txtShieldLevel.Size = new System.Drawing.Size(52, 13);
            this.txtShieldLevel.TabIndex = 24;
            this.txtShieldLevel.Text = "Shields:";
            // 
            // lblShields
            // 
            this.lblShields.AutoSize = true;
            this.lblShields.Location = new System.Drawing.Point(55, 144);
            this.lblShields.Name = "lblShields";
            this.lblShields.Size = new System.Drawing.Size(44, 13);
            this.lblShields.TabIndex = 14;
            this.lblShields.Text = "Shields:";
            // 
            // lblSector
            // 
            this.lblSector.AutoSize = true;
            this.lblSector.Location = new System.Drawing.Point(58, 54);
            this.lblSector.Name = "lblSector";
            this.lblSector.Size = new System.Drawing.Size(41, 13);
            this.lblSector.TabIndex = 9;
            this.lblSector.Text = "Sector:";
            // 
            // txtSector
            // 
            this.txtSector.AutoSize = true;
            this.txtSector.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSector.Location = new System.Drawing.Point(105, 54);
            this.txtSector.Name = "txtSector";
            this.txtSector.Size = new System.Drawing.Size(48, 13);
            this.txtSector.TabIndex = 19;
            this.txtSector.Text = "Sector:";
            // 
            // txtEnergy
            // 
            this.txtEnergy.AutoSize = true;
            this.txtEnergy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnergy.Location = new System.Drawing.Point(105, 126);
            this.txtEnergy.Name = "txtEnergy";
            this.txtEnergy.Size = new System.Drawing.Size(50, 13);
            this.txtEnergy.TabIndex = 23;
            this.txtEnergy.Text = "Energy:";
            // 
            // lblEnergy
            // 
            this.lblEnergy.AutoSize = true;
            this.lblEnergy.Location = new System.Drawing.Point(56, 126);
            this.lblEnergy.Name = "lblEnergy";
            this.lblEnergy.Size = new System.Drawing.Size(43, 13);
            this.lblEnergy.TabIndex = 13;
            this.lblEnergy.Text = "Energy:";
            // 
            // lblStardate
            // 
            this.lblStardate.AutoSize = true;
            this.lblStardate.Location = new System.Drawing.Point(49, 72);
            this.lblStardate.Name = "lblStardate";
            this.lblStardate.Size = new System.Drawing.Size(50, 13);
            this.lblStardate.TabIndex = 10;
            this.lblStardate.Text = "Stardate:";
            // 
            // txtStardate
            // 
            this.txtStardate.AutoSize = true;
            this.txtStardate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStardate.Location = new System.Drawing.Point(105, 72);
            this.txtStardate.Name = "txtStardate";
            this.txtStardate.Size = new System.Drawing.Size(59, 13);
            this.txtStardate.TabIndex = 20;
            this.txtStardate.Text = "Stardate:";
            // 
            // txtCondition
            // 
            this.txtCondition.AutoSize = true;
            this.txtCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCondition.Location = new System.Drawing.Point(105, 108);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(64, 13);
            this.txtCondition.TabIndex = 22;
            this.txtCondition.Text = "Condition:";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(45, 108);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(54, 13);
            this.lblCondition.TabIndex = 12;
            this.lblCondition.Text = "Condition:";
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.Location = new System.Drawing.Point(13, 90);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(86, 13);
            this.lblTimeRemaining.TabIndex = 11;
            this.lblTimeRemaining.Text = "Time Remaining:";
            // 
            // txtTimeRemaining
            // 
            this.txtTimeRemaining.AutoSize = true;
            this.txtTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeRemaining.Location = new System.Drawing.Point(105, 90);
            this.txtTimeRemaining.Name = "txtTimeRemaining";
            this.txtTimeRemaining.Size = new System.Drawing.Size(101, 13);
            this.txtTimeRemaining.TabIndex = 21;
            this.txtTimeRemaining.Text = "Time Remaining:";
            // 
            // grpSensors
            // 
            this.grpSensors.Controls.Add(this.btnLongRangeScan);
            this.grpSensors.Controls.Add(this.btnShortRangeScan);
            this.grpSensors.Location = new System.Drawing.Point(296, 205);
            this.grpSensors.Name = "grpSensors";
            this.grpSensors.Size = new System.Drawing.Size(120, 63);
            this.grpSensors.TabIndex = 30;
            this.grpSensors.TabStop = false;
            this.grpSensors.Text = "Sensors";
            // 
            // btnLongRangeScan
            // 
            this.btnLongRangeScan.Location = new System.Drawing.Point(6, 37);
            this.btnLongRangeScan.Name = "btnLongRangeScan";
            this.btnLongRangeScan.Size = new System.Drawing.Size(108, 23);
            this.btnLongRangeScan.TabIndex = 2;
            this.btnLongRangeScan.Text = "Long Range Scan";
            this.btnLongRangeScan.UseVisualStyleBackColor = true;
            this.btnLongRangeScan.Click += new System.EventHandler(this.btnLongRangeScan_Click);
            // 
            // btnShortRangeScan
            // 
            this.btnShortRangeScan.Location = new System.Drawing.Point(6, 14);
            this.btnShortRangeScan.Name = "btnShortRangeScan";
            this.btnShortRangeScan.Size = new System.Drawing.Size(108, 23);
            this.btnShortRangeScan.TabIndex = 1;
            this.btnShortRangeScan.Text = "Short Range Scan";
            this.btnShortRangeScan.UseVisualStyleBackColor = true;
            this.btnShortRangeScan.Click += new System.EventHandler(this.btnShortRangeScan_Click);
            // 
            // grpComputer
            // 
            this.grpComputer.Controls.Add(this.button5);
            this.grpComputer.Controls.Add(this.btnBaseCalc);
            this.grpComputer.Controls.Add(this.btnTorpCalc);
            this.grpComputer.Controls.Add(this.btnStatusReport);
            this.grpComputer.Controls.Add(this.btnGalRec);
            this.grpComputer.Location = new System.Drawing.Point(417, 166);
            this.grpComputer.Name = "grpComputer";
            this.grpComputer.Size = new System.Drawing.Size(94, 176);
            this.grpComputer.TabIndex = 29;
            this.grpComputer.TabStop = false;
            this.grpComputer.Text = "Computer";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 142);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(81, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Nav Calc";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // btnBaseCalc
            // 
            this.btnBaseCalc.Location = new System.Drawing.Point(7, 112);
            this.btnBaseCalc.Name = "btnBaseCalc";
            this.btnBaseCalc.Size = new System.Drawing.Size(81, 23);
            this.btnBaseCalc.TabIndex = 3;
            this.btnBaseCalc.Text = "Base Calc";
            this.btnBaseCalc.UseVisualStyleBackColor = true;
            this.btnBaseCalc.Click += new System.EventHandler(this.btnBaseCalc_Click);
            // 
            // btnTorpCalc
            // 
            this.btnTorpCalc.Location = new System.Drawing.Point(7, 82);
            this.btnTorpCalc.Name = "btnTorpCalc";
            this.btnTorpCalc.Size = new System.Drawing.Size(81, 23);
            this.btnTorpCalc.TabIndex = 2;
            this.btnTorpCalc.Text = "Torp Calc";
            this.btnTorpCalc.UseVisualStyleBackColor = true;
            this.btnTorpCalc.Click += new System.EventHandler(this.btnTorpCalc_Click);
            // 
            // btnStatusReport
            // 
            this.btnStatusReport.Location = new System.Drawing.Point(7, 52);
            this.btnStatusReport.Name = "btnStatusReport";
            this.btnStatusReport.Size = new System.Drawing.Size(81, 23);
            this.btnStatusReport.TabIndex = 1;
            this.btnStatusReport.Text = "Status Report";
            this.btnStatusReport.UseVisualStyleBackColor = true;
            this.btnStatusReport.Click += new System.EventHandler(this.btnStatusReport_Click);
            // 
            // btnGalRec
            // 
            this.btnGalRec.Location = new System.Drawing.Point(7, 22);
            this.btnGalRec.Name = "btnGalRec";
            this.btnGalRec.Size = new System.Drawing.Size(81, 23);
            this.btnGalRec.TabIndex = 0;
            this.btnGalRec.Text = "Galactic Map";
            this.btnGalRec.UseVisualStyleBackColor = true;
            this.btnGalRec.Click += new System.EventHandler(this.btnGalRec_Click);
            // 
            // grpShields
            // 
            this.grpShields.Controls.Add(this.btnSubShield);
            this.grpShields.Controls.Add(this.btnAddShield);
            this.grpShields.Controls.Add(this.txtUpdateShields);
            this.grpShields.Location = new System.Drawing.Point(216, 205);
            this.grpShields.Name = "grpShields";
            this.grpShields.Size = new System.Drawing.Size(74, 63);
            this.grpShields.TabIndex = 28;
            this.grpShields.TabStop = false;
            this.grpShields.Text = "Shields";
            // 
            // btnSubShield
            // 
            this.btnSubShield.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubShield.Location = new System.Drawing.Point(41, 39);
            this.btnSubShield.Name = "btnSubShield";
            this.btnSubShield.Size = new System.Drawing.Size(27, 20);
            this.btnSubShield.TabIndex = 4;
            this.btnSubShield.Text = "-";
            this.btnSubShield.UseVisualStyleBackColor = true;
            this.btnSubShield.Click += new System.EventHandler(this.btnSubShield_Click);
            // 
            // btnAddShield
            // 
            this.btnAddShield.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddShield.Location = new System.Drawing.Point(8, 39);
            this.btnAddShield.Name = "btnAddShield";
            this.btnAddShield.Size = new System.Drawing.Size(27, 20);
            this.btnAddShield.TabIndex = 3;
            this.btnAddShield.Text = "+";
            this.btnAddShield.UseVisualStyleBackColor = true;
            this.btnAddShield.Click += new System.EventHandler(this.btnAddShield_Click);
            // 
            // txtUpdateShields
            // 
            this.txtUpdateShields.Location = new System.Drawing.Point(8, 16);
            this.txtUpdateShields.Name = "txtUpdateShields";
            this.txtUpdateShields.Size = new System.Drawing.Size(60, 20);
            this.txtUpdateShields.TabIndex = 2;
            // 
            // grpShortRangeScan
            // 
            this.grpShortRangeScan.Controls.Add(this.txtSRS);
            this.grpShortRangeScan.Location = new System.Drawing.Point(3, 3);
            this.grpShortRangeScan.Name = "grpShortRangeScan";
            this.grpShortRangeScan.Size = new System.Drawing.Size(187, 201);
            this.grpShortRangeScan.TabIndex = 27;
            this.grpShortRangeScan.TabStop = false;
            this.grpShortRangeScan.Text = "Short Range Scan";
            // 
            // txtSRS
            // 
            this.txtSRS.BackColor = System.Drawing.Color.Black;
            this.txtSRS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSRS.ForeColor = System.Drawing.Color.White;
            this.txtSRS.Location = new System.Drawing.Point(4, 16);
            this.txtSRS.Name = "txtSRS";
            this.txtSRS.ReadOnly = true;
            this.txtSRS.Size = new System.Drawing.Size(180, 180);
            this.txtSRS.TabIndex = 1;
            this.txtSRS.Text = "";
            // 
            // grpNavigation
            // 
            this.grpNavigation.Controls.Add(this.btnEngage);
            this.grpNavigation.Controls.Add(this.txtDistance);
            this.grpNavigation.Controls.Add(this.lblSpeed);
            this.grpNavigation.Controls.Add(this.lblCourse);
            this.grpNavigation.Controls.Add(this.txtDirection);
            this.grpNavigation.Location = new System.Drawing.Point(3, 205);
            this.grpNavigation.Name = "grpNavigation";
            this.grpNavigation.Size = new System.Drawing.Size(209, 63);
            this.grpNavigation.TabIndex = 6;
            this.grpNavigation.TabStop = false;
            this.grpNavigation.Text = "Navigation";
            // 
            // btnEngage
            // 
            this.btnEngage.Location = new System.Drawing.Point(149, 20);
            this.btnEngage.Name = "btnEngage";
            this.btnEngage.Size = new System.Drawing.Size(54, 23);
            this.btnEngage.TabIndex = 5;
            this.btnEngage.Text = "Engage";
            this.btnEngage.UseVisualStyleBackColor = true;
            this.btnEngage.Click += new System.EventHandler(this.btnEngage_Click);
            // 
            // txtDistance
            // 
            this.txtDistance.Location = new System.Drawing.Point(83, 36);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(60, 20);
            this.txtDistance.TabIndex = 4;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(8, 39);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(69, 13);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "Warp Factor:";
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Location = new System.Drawing.Point(34, 16);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(43, 13);
            this.lblCourse.TabIndex = 2;
            this.lblCourse.Text = "Course:";
            // 
            // txtDirection
            // 
            this.txtDirection.Location = new System.Drawing.Point(83, 13);
            this.txtDirection.Name = "txtDirection";
            this.txtDirection.Size = new System.Drawing.Size(60, 20);
            this.txtDirection.TabIndex = 1;
            // 
            // grpCommunications
            // 
            this.grpCommunications.Controls.Add(this.txtOutput);
            this.grpCommunications.Location = new System.Drawing.Point(3, 268);
            this.grpCommunications.Name = "grpCommunications";
            this.grpCommunications.Size = new System.Drawing.Size(413, 75);
            this.grpCommunications.TabIndex = 5;
            this.grpCommunications.TabStop = false;
            this.grpCommunications.Text = "Communications";
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.Black;
            this.txtOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.ForeColor = System.Drawing.Color.White;
            this.txtOutput.Location = new System.Drawing.Point(6, 19);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(401, 49);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.Text = "";
            // 
            // grpTactical
            // 
            this.grpTactical.Controls.Add(this.btnFire);
            this.grpTactical.Controls.Add(this.grpTorpedoes);
            this.grpTactical.Controls.Add(this.grpPhasers);
            this.grpTactical.Location = new System.Drawing.Point(417, 3);
            this.grpTactical.Name = "grpTactical";
            this.grpTactical.Size = new System.Drawing.Size(94, 160);
            this.grpTactical.TabIndex = 4;
            this.grpTactical.TabStop = false;
            this.grpTactical.Text = "Tactical";
            // 
            // btnFire
            // 
            this.btnFire.Location = new System.Drawing.Point(10, 132);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(75, 23);
            this.btnFire.TabIndex = 2;
            this.btnFire.Text = "Fire";
            this.btnFire.UseVisualStyleBackColor = true;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // grpTorpedoes
            // 
            this.grpTorpedoes.Controls.Add(this.lblTorpedoes);
            this.grpTorpedoes.Controls.Add(this.txtTorpedoes);
            this.grpTorpedoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTorpedoes.Location = new System.Drawing.Point(7, 73);
            this.grpTorpedoes.Name = "grpTorpedoes";
            this.grpTorpedoes.Size = new System.Drawing.Size(81, 55);
            this.grpTorpedoes.TabIndex = 1;
            this.grpTorpedoes.TabStop = false;
            this.grpTorpedoes.Text = "Torpedoes";
            // 
            // lblTorpedoes
            // 
            this.lblTorpedoes.AutoSize = true;
            this.lblTorpedoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTorpedoes.Location = new System.Drawing.Point(14, 39);
            this.lblTorpedoes.Name = "lblTorpedoes";
            this.lblTorpedoes.Size = new System.Drawing.Size(52, 13);
            this.lblTorpedoes.TabIndex = 5;
            this.lblTorpedoes.Text = "Direction:";
            // 
            // txtTorpedoes
            // 
            this.txtTorpedoes.Location = new System.Drawing.Point(10, 16);
            this.txtTorpedoes.Name = "txtTorpedoes";
            this.txtTorpedoes.Size = new System.Drawing.Size(60, 20);
            this.txtTorpedoes.TabIndex = 0;
            // 
            // grpPhasers
            // 
            this.grpPhasers.Controls.Add(this.lblPhasers);
            this.grpPhasers.Controls.Add(this.txtPhasers);
            this.grpPhasers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPhasers.Location = new System.Drawing.Point(7, 15);
            this.grpPhasers.Name = "grpPhasers";
            this.grpPhasers.Size = new System.Drawing.Size(81, 55);
            this.grpPhasers.TabIndex = 0;
            this.grpPhasers.TabStop = false;
            this.grpPhasers.Text = "Phasers";
            // 
            // lblPhasers
            // 
            this.lblPhasers.AutoSize = true;
            this.lblPhasers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhasers.Location = new System.Drawing.Point(20, 39);
            this.lblPhasers.Name = "lblPhasers";
            this.lblPhasers.Size = new System.Drawing.Size(40, 13);
            this.lblPhasers.TabIndex = 1;
            this.lblPhasers.Text = "Power:";
            // 
            // txtPhasers
            // 
            this.txtPhasers.Location = new System.Drawing.Point(10, 16);
            this.txtPhasers.Name = "txtPhasers";
            this.txtPhasers.Size = new System.Drawing.Size(60, 20);
            this.txtPhasers.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 369);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.grpGalacticRecord.ResumeLayout(false);
            this.grpHelm.ResumeLayout(false);
            this.grpHelm.PerformLayout();
            this.grpSensors.ResumeLayout(false);
            this.grpComputer.ResumeLayout(false);
            this.grpShields.ResumeLayout(false);
            this.grpShields.PerformLayout();
            this.grpShortRangeScan.ResumeLayout(false);
            this.grpNavigation.ResumeLayout(false);
            this.grpNavigation.PerformLayout();
            this.grpCommunications.ResumeLayout(false);
            this.grpTactical.ResumeLayout(false);
            this.grpTorpedoes.ResumeLayout(false);
            this.grpTorpedoes.PerformLayout();
            this.grpPhasers.ResumeLayout(false);
            this.grpPhasers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.RichTextBox txtSRS;
        private System.Windows.Forms.GroupBox grpTactical;
        private System.Windows.Forms.GroupBox grpTorpedoes;
        private System.Windows.Forms.GroupBox grpPhasers;
        private System.Windows.Forms.Button btnFire;
        private System.Windows.Forms.Label lblTorpedoes;
        private System.Windows.Forms.TextBox txtTorpedoes;
        private System.Windows.Forms.Label lblPhasers;
        private System.Windows.Forms.TextBox txtPhasers;
        private System.Windows.Forms.GroupBox grpNavigation;
        private System.Windows.Forms.GroupBox grpCommunications;
        private System.Windows.Forms.TextBox txtDirection;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Button btnEngage;
        private System.Windows.Forms.Label lblStardate;
        private System.Windows.Forms.Label lblSector;
        private System.Windows.Forms.Label lblQuadrant;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblDocked;
        private System.Windows.Forms.Label lblTorpedoesRemaining;
        private System.Windows.Forms.Label lblShields;
        private System.Windows.Forms.Label lblEnergy;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Label txtDocked;
        private System.Windows.Forms.Label txtTorpedoesRemaining;
        private System.Windows.Forms.Label txtShieldLevel;
        private System.Windows.Forms.Label txtEnergy;
        private System.Windows.Forms.Label txtCondition;
        private System.Windows.Forms.Label txtTimeRemaining;
        private System.Windows.Forms.Label txtStardate;
        private System.Windows.Forms.Label txtSector;
        private System.Windows.Forms.Label txtQuadrant;
        private System.Windows.Forms.Label txtRegion;
        private System.Windows.Forms.GroupBox grpShortRangeScan;
        private System.Windows.Forms.GroupBox grpShields;
        private System.Windows.Forms.Button btnSubShield;
        private System.Windows.Forms.Button btnAddShield;
        private System.Windows.Forms.TextBox txtUpdateShields;
        private System.Windows.Forms.GroupBox grpComputer;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnBaseCalc;
        private System.Windows.Forms.Button btnTorpCalc;
        private System.Windows.Forms.Button btnStatusReport;
        private System.Windows.Forms.Button btnGalRec;
        private System.Windows.Forms.GroupBox grpSensors;
        private System.Windows.Forms.Button btnLongRangeScan;
        private System.Windows.Forms.Button btnShortRangeScan;
        private System.Windows.Forms.GroupBox grpHelm;
        private System.Windows.Forms.GroupBox grpGalacticRecord;
        private System.Windows.Forms.RichTextBox txtGalRec;
        private System.Windows.Forms.Button btnCloseGalRec;



    }
}

