namespace Ted.MySnake
{
	partial class MainForm
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
			if (disposing && (components != null)) {
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
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.titleScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.classicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.computerSnakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.difficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.demonstrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutSnakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlInformation = new System.Windows.Forms.Panel();
			this.lblLives = new System.Windows.Forms.Label();
			this.prgLevelTime = new System.Windows.Forms.ProgressBar();
			this.lblScore = new System.Windows.Forms.Label();
			this.snakeGame1 = new Ted.MySnake.SnakeGame();
			this.menuStrip1.SuspendLayout();
			this.pnlInformation.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(286, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// gameToolStripMenuItem
			// 
			this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.toolStripSeparator1,
            this.titleScreenToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.gameToolStripMenuItem.Text = "Game";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.CheckOnClick = true;
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.pauseToolStripMenuItem.Text = "Pause";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// titleScreenToolStripMenuItem
			// 
			this.titleScreenToolStripMenuItem.Name = "titleScreenToolStripMenuItem";
			this.titleScreenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.titleScreenToolStripMenuItem.Text = "Title Screen";
			this.titleScreenToolStripMenuItem.Click += new System.EventHandler(this.titleScreenToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.computerSnakeToolStripMenuItem,
            this.difficultyToolStripMenuItem,
            this.selectLevelToolStripMenuItem,
            this.demonstrationToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// modeToolStripMenuItem
			// 
			this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.classicToolStripMenuItem,
            this.normalToolStripMenuItem});
			this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
			this.modeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.modeToolStripMenuItem.Text = "Mode";
			// 
			// classicToolStripMenuItem
			// 
			this.classicToolStripMenuItem.Checked = true;
			this.classicToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.classicToolStripMenuItem.Name = "classicToolStripMenuItem";
			this.classicToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.classicToolStripMenuItem.Text = "Classic";
			this.classicToolStripMenuItem.Click += new System.EventHandler(this.modeToolStripMenuItem_Click);
			// 
			// normalToolStripMenuItem
			// 
			this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
			this.normalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.normalToolStripMenuItem.Text = "Normal";
			this.normalToolStripMenuItem.Click += new System.EventHandler(this.modeToolStripMenuItem_Click);
			// 
			// computerSnakeToolStripMenuItem
			// 
			this.computerSnakeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
			this.computerSnakeToolStripMenuItem.Name = "computerSnakeToolStripMenuItem";
			this.computerSnakeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.computerSnakeToolStripMenuItem.Text = "Computer Snake";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Checked = true;
			this.toolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
			this.toolStripMenuItem2.Text = "0";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.computerSnakesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
			this.toolStripMenuItem3.Text = "1";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.computerSnakesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
			this.toolStripMenuItem4.Text = "2";
			this.toolStripMenuItem4.Click += new System.EventHandler(this.computerSnakesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
			this.toolStripMenuItem5.Text = "3";
			this.toolStripMenuItem5.Click += new System.EventHandler(this.computerSnakesToolStripMenuItem_Click);
			// 
			// difficultyToolStripMenuItem
			// 
			this.difficultyToolStripMenuItem.Name = "difficultyToolStripMenuItem";
			this.difficultyToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.difficultyToolStripMenuItem.Text = "Difficulty";
			// 
			// selectLevelToolStripMenuItem
			// 
			this.selectLevelToolStripMenuItem.Name = "selectLevelToolStripMenuItem";
			this.selectLevelToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.selectLevelToolStripMenuItem.Text = "Select level...";
			this.selectLevelToolStripMenuItem.Click += new System.EventHandler(this.selectLevelToolStripMenuItem_Click);
			// 
			// demonstrationToolStripMenuItem
			// 
			this.demonstrationToolStripMenuItem.CheckOnClick = true;
			this.demonstrationToolStripMenuItem.Name = "demonstrationToolStripMenuItem";
			this.demonstrationToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.demonstrationToolStripMenuItem.Text = "Demonstration";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSnakeToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutSnakeToolStripMenuItem
			// 
			this.aboutSnakeToolStripMenuItem.Name = "aboutSnakeToolStripMenuItem";
			this.aboutSnakeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.aboutSnakeToolStripMenuItem.Text = "About Snake";
			// 
			// pnlInformation
			// 
			this.pnlInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlInformation.Controls.Add(this.lblLives);
			this.pnlInformation.Controls.Add(this.prgLevelTime);
			this.pnlInformation.Controls.Add(this.lblScore);
			this.pnlInformation.Location = new System.Drawing.Point(0, 337);
			this.pnlInformation.Name = "pnlInformation";
			this.pnlInformation.Size = new System.Drawing.Size(286, 36);
			this.pnlInformation.TabIndex = 2;
			// 
			// lblLives
			// 
			this.lblLives.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLives.AutoSize = true;
			this.lblLives.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLives.Location = new System.Drawing.Point(216, 13);
			this.lblLives.Name = "lblLives";
			this.lblLives.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblLives.Size = new System.Drawing.Size(58, 19);
			this.lblLives.TabIndex = 2;
			this.lblLives.Text = "Lives: 0";
			// 
			// prgLevelTime
			// 
			this.prgLevelTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.prgLevelTime.Location = new System.Drawing.Point(0, 0);
			this.prgLevelTime.Name = "prgLevelTime";
			this.prgLevelTime.Size = new System.Drawing.Size(286, 10);
			this.prgLevelTime.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prgLevelTime.TabIndex = 1;
			// 
			// lblScore
			// 
			this.lblScore.AutoSize = true;
			this.lblScore.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblScore.Location = new System.Drawing.Point(3, 13);
			this.lblScore.Name = "lblScore";
			this.lblScore.Size = new System.Drawing.Size(63, 19);
			this.lblScore.TabIndex = 0;
			this.lblScore.Text = "Score: 0";
			// 
			// snakeGame1
			// 
			this.snakeGame1.ComputerSnakes = 0;
			this.snakeGame1.Demonstration = false;
			this.snakeGame1.Difficulty = Ted.MySnake.Difficulty.Easy;
			this.snakeGame1.DisplayMunches = false;
			this.snakeGame1.ExtendSnakeOnFood = 1;
			this.snakeGame1.LevelNumber = 0;
			this.snakeGame1.Location = new System.Drawing.Point(3, 27);
			this.snakeGame1.Mode = Ted.MySnake.Mode.Classic;
			this.snakeGame1.Name = "snakeGame1";
			this.snakeGame1.NoPreGameScreen = false;
			this.snakeGame1.Paused = false;
			this.snakeGame1.Size = new System.Drawing.Size(283, 276);
			this.snakeGame1.TabIndex = 0;
			this.snakeGame1.Text = "snakeGame1";
			this.snakeGame1.LevelUpdate += new System.EventHandler(this.snakeGame1_LevelUpdate);
			this.snakeGame1.LevelFail += new System.EventHandler(this.snakeGame1_LevelFail);
			this.snakeGame1.LevelComplete += new System.EventHandler(this.snakeGame1_LevelComplete);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(286, 373);
			this.Controls.Add(this.pnlInformation);
			this.Controls.Add(this.snakeGame1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Snake";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.pnlInformation.ResumeLayout(false);
			this.pnlInformation.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SnakeGame snakeGame1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
		private System.Windows.Forms.Panel pnlInformation;
		private System.Windows.Forms.Label lblScore;
		private System.Windows.Forms.ProgressBar prgLevelTime;
		private System.Windows.Forms.Label lblLives;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem classicToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem computerSnakeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem selectLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem difficultyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutSnakeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem demonstrationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem titleScreenToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

