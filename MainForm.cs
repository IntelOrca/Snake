using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ted.MySnake
{
	public partial class MainForm : Form
	{
		ASCIILevelTable mLevels;

		int mStartingLevel = 1;
		int mLevel = 1;
		int mHighestLevel = 0;
		int mTotalScore = 0;

		public MainForm()
		{
			InitializeComponent();

			for (int i = 0; i < (int)Difficulty.Count; i++) {
				ToolStripMenuItem item = new ToolStripMenuItem();
				item.Text = ((Difficulty)i).ToString();
				item.Click += new EventHandler(difficultyToolStripMenuItem_Click);
				difficultyToolStripMenuItem.DropDownItems.Add(item);
			}
			((ToolStripMenuItem)difficultyToolStripMenuItem.DropDownItems[1]).Checked = true;

			//List<string> files = new List<string>(Directory.GetFiles("levels", "level*.txt"));
			//int fc = files.Count + 1;
			//for (int i = 1; i < fc; i++) {
			//    int index = files.IndexOf(String.Format("levels\\level{0:00}.txt", i));
			//    if (index >= 0) {
			//        mHighestLevel++;
			//        files.RemoveAt(index);
			//    } else {
			//        break;
			//    }
			//}

			mLevels = new ASCIILevelTable("levels\\levels.txt");
			mHighestLevel = mLevels.Count;

			snakeGame1.Images.Add("null", Image.FromFile("media\\null.png"));
			snakeGame1.Images.Add("wall", Image.FromFile("media\\wall.png"));
			snakeGame1.Images.Add("food", Image.FromFile("media\\food.png"));
			snakeGame1.LoadASCIITable("levels\\ascii.txt");

			LoadIntroLevel();
			//newToolStripMenuItem_Click(this, EventArgs.Empty);
		}

		private void LoadIntroLevel()
		{
			snakeGame1.Mode = Mode.Classic;
			snakeGame1.Difficulty = Difficulty.Medium;
			snakeGame1.ComputerSnakes = 1;
			snakeGame1.Demonstration = true;
			snakeGame1.NoPreGameScreen = true;
			snakeGame1.ExtendSnakeOnFood = 0;
			LoadLevel(0);
			Start();
		}

		private void SetLocationAndSize()
		{
			Size size = snakeGame1.MapSize;
			if (snakeGame1.ShowingASCII)
				size = snakeGame1.ASCIISize;

			Point centrePoint = new Point(Location.X + (Size.Width / 2), Location.Y + (Size.Height / 2));

			ClientSize = new Size(size.Width * SnakeGame.TileSize + 6,
				size.Height * SnakeGame.TileSize + 30 + pnlInformation.Height);
			snakeGame1.Location = new Point(3, 27);
			snakeGame1.Size = new Size(size.Width * SnakeGame.TileSize,
				size.Height * SnakeGame.TileSize);

			Location = new Point(centrePoint.X - (Size.Width / 2), centrePoint.Y - (Size.Height / 2));
		}

		private void Start()
		{
			pauseToolStripMenuItem.Checked = false;
			snakeGame1.Start();
		}

		private void LoadLevel()
		{
			snakeGame1.ComputerSnakes = SelectedComputerSnakes;
			snakeGame1.Mode = SelectedMode;
			snakeGame1.Difficulty = SelectedDifficulty;
			snakeGame1.Demonstration = demonstrationToolStripMenuItem.Checked;
			snakeGame1.ExtendSnakeOnFood = 1;
			snakeGame1.NoPreGameScreen = false;

			LoadLevel(mLevel);
			Start();
		}

		private void LoadLevel(int level)
		{
			snakeGame1.LevelNumber = level;
			snakeGame1.LoadMap(mLevels.GetLevel(level));
			SetLocationAndSize();
		}

		private void snakeGame1_LevelComplete(object sender, EventArgs e)
		{
			mTotalScore += snakeGame1.ControllingSnake.Score;
			if (mLevel == mHighestLevel)
				return;
			else
				mLevel++;

			LoadLevel();
			Start();
		}

		private void snakeGame1_LevelFail(object sender, EventArgs e)
		{
			mTotalScore += snakeGame1.ControllingSnake.Score;
			Console.Beep();

			if (snakeGame1.Mode == Mode.Classic) {

			} else {
				LoadLevel();
				Start();
			}
		}

		private void snakeGame1_LevelUpdate(object sender, EventArgs e)
		{
			SetLocationAndSize();

			prgLevelTime.Maximum = SnakeGame.InitialLevelTime;
			prgLevelTime.Value = snakeGame1.LevelTime;

			string szScore = "Score: ";

			for (int i = 0; i < snakeGame1.Snakes.Count; i++) {
				int score = snakeGame1.Snakes[i].Score;
				szScore += String.Format("{0:00000} | ", score);
			}

			if (lblScore.Text != szScore)
				lblScore.Text = szScore;
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mLevel = mStartingLevel;
			LoadLevel();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void computerSnakesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			foreach (ToolStripMenuItem i in computerSnakeToolStripMenuItem.DropDownItems) {
				i.Checked = false;
			}

			item.Checked = true;
		}

		private void modeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			foreach (ToolStripMenuItem i in modeToolStripMenuItem.DropDownItems) {
				i.Checked = false;
			}

			item.Checked = true;
		}

		private void difficultyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			foreach (ToolStripMenuItem i in difficultyToolStripMenuItem.DropDownItems) {
				i.Checked = false;
			}

			item.Checked = true;
		}

		private int SelectedComputerSnakes
		{
			get
			{
				foreach (ToolStripMenuItem i in computerSnakeToolStripMenuItem.DropDownItems) {
					if (i.Checked) {
						return Convert.ToInt32(i.Text);
					}
				}

				return 0;
			}
		}

		private Mode SelectedMode
		{
			get
			{
				if (classicToolStripMenuItem.Checked)
					return Mode.Classic;
				else if (normalToolStripMenuItem.Checked)
					return Mode.Normal;
				else
					return Mode.Normal;
			}
		}

		private Difficulty SelectedDifficulty
		{
			get
			{
				foreach (ToolStripMenuItem item in difficultyToolStripMenuItem.DropDownItems) {
					if (item.Checked) {
						for (int i = 0; i < (int)Difficulty.Count; i++) {
							if (String.Compare(((Difficulty)i).ToString(), item.Text) == 0)
								return (Difficulty)i;
						}
					}
				}

				return Difficulty.Easy;
			}
		}

		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			snakeGame1.Paused = pauseToolStripMenuItem.Checked;
		}

		private void titleScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadIntroLevel();
		}

		private void selectLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LevelSelectForm form = new LevelSelectForm(mLevels);
			form.SelectedLevel = mStartingLevel;
			if (form.ShowDialog() == DialogResult.OK) {
				mStartingLevel = form.SelectedLevel;
			}
			return;

			string s = InputForm.Show("Level select", "Enter the starting level.", mStartingLevel.ToString());
			int sl = Convert.ToInt32(s);
			if (sl > mHighestLevel || sl < 0)
				MessageBox.Show("This level does not exist!", "Level Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				mStartingLevel = sl;
		}
	}
}
