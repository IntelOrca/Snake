using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Ted.MySnake
{
	enum TileType
	{
		Null,
		Food,
		Wall,
		Snake,
	}

	class SnakeGame : Control
	{
		public const int UpdatesPerSecond = 40;
		public const int TileSize = 20;
		public const int InitialLevelTime = UpdatesPerSecond * 20;
		public const int InitialSnakeLength = 5;

		public event EventHandler LevelUpdate;
		public event EventHandler LevelComplete;
		public event EventHandler LevelFail;

		CallbackState mCallbackState;
		Random mRand = new Random();

		bool mDemo;
		Difficulty mDifficulty;
		Mode mMode;
		int mComputerSnakes;

		int mExtendSnakeOnFood = 1;
		bool mDisplayMunches = false;

		int mLevelTime;
		int mUpdateCnt;
		int mLastKeyUpdate;

		Color[] mTileColours = { Color.Black, Color.Green, Color.Blue, Color.Yellow };

		ASCIILevel mLevel;
		Size mASCIISize;
		Size mSize;
		TileType[,] mASCII;
		TileType[,] mMap;
		Timer mTimer;
		List<Point> mNoFoodPoints = new List<Point>();

		List<Snake> mSnakes;
		Snake mControlSnake;

		int mPreGameTime;
		bool mPlaying;
		int mInitialFood;
		bool mPaused;
		int mLevelNumber;
		bool mNoPreGameScreen;
		bool mShowingASCII;

		ASCIIWallTable mASCIITable;

		Dictionary<string, Image> mImages = new Dictionary<string, Image>();

		public SnakeGame()
		{
			mASCIITable = new ASCIIWallTable();

			DoubleBuffered = true;
			mTimer = new Timer();
			mTimer.Interval = 1000 / UpdatesPerSecond;
			mTimer.Enabled = true;
			mTimer.Tick += new EventHandler(mTimer_Tick);

			mSnakes = new List<Snake>();
		}

		public void LoadASCIITable(string filename)
		{
			mASCIITable = new ASCIIWallTable(filename);
		}

		void mTimer_Tick(object sender, EventArgs e)
		{
			UpdateGame();
		}

		public void LoadMap(string filename)
		{
			LoadMap(new ASCIILevel(filename));
		}

		public void LoadMap(ASCIILevel level)
		{
			mLevel = level;

			string[] lns = level.Name.Split('|');
			int lines = lns.Length;
			int ll = 0;
			foreach (string l in lns)
				ll = Math.Max(ll, l.Length);

			mASCIISize = new Size(Math.Max(6, ll) * 4 + 2, mASCIITable.GetHeight() * lines + (2 * lines));
			mASCII = new TileType[mASCIISize.Width, mASCIISize.Height];
			mMap = level.GetGridCopy();
			mSize = level.Size;
			mNoFoodPoints = level.NoFoodPoints;

			mSnakes.Clear();

			Color[] snakeColours = { Color.Orange, Color.Red, Color.White, Color.Gray, Color.Brown, Color.Purple, Color.Pink, Color.LightBlue, Color.LightGreen};

			if (Demonstration) {
				mSnakes.Add(new Snake(this, BottomDoorLocation, InitialSnakeLength, snakeColours[0]));
			} else {
				mControlSnake = new Snake(this, BottomDoorLocation, InitialSnakeLength, Color.Yellow);
				mControlSnake.AI = null;
				mSnakes.Add(mControlSnake);
			}

			for (int i = 0; i < mComputerSnakes; i++) {
				mSnakes.Add(new Snake(this, GetDoorLocation(i + 1), InitialSnakeLength, snakeColours[(i + 1) % snakeColours.Length]));
			}

			if (mMode == Mode.Classic)
				mInitialFood = mSnakes.Count;
			else
				mInitialFood = (int)Math.Round((float)level.GetSpace() * 0.02f);

			AddRandomFood(mInitialFood);
			FoodCheck();

			if (mControlSnake != null)
				mControlSnake.SetDirection(Direction.North);

			mPlaying = false;
		}

		public void Start()
		{
			float[] speeds = { 4, 6, 8, 10, 12, 15, UpdatesPerSecond };
			foreach (Snake s in mSnakes) {
				s.Speed = speeds[(int)mDifficulty];
				s.ExtendSnakeOnFood = mExtendSnakeOnFood;
				s.DisplayMunches = mDisplayMunches;
			}

			if (!mNoPreGameScreen) {
				mPreGameTime = (int)(0.8 * UpdatesPerSecond);
			} else {
				mPlaying = true;
				ClearASCII();
				WriteASCIICentred("PAUSED", mASCIISize.Width / 2, mASCIISize.Height / 2);
			}

			mLevelTime = InitialLevelTime;
			mPaused = false;
		}

		private void UpdateGame()
		{
			//Pre game timer
			if (mPreGameTime > 0) {
				mPreGameTime--;

				if (mPreGameTime == 0) {
					mPlaying = true;
					ClearASCII();
					WriteASCIICentred("PAUSED", mASCIISize.Width / 2, mASCIISize.Height / 2);
				} else {
					ClearASCII();

					string[] lns = mLevel.Name.Split('|');

					for (int i = 0; i < lns.Length; i++)
						WriteASCIICentred(lns[i], mASCIISize.Width / 2, (mASCIISize.Height * (i + 1)) / (lns.Length + 1));
				}
			}

			if (mPlaying && !mPaused) {
				//Update all the snakes
				UpdateSnakes();

				//Check block at bottom
				//if (GetTileAt(BottomDoorLocation) == TileType.Null) {
				//    if (!IsThereSnakeAt(BottomDoorLocation)) {
				//        SetTileAt(BottomDoorLocation, TileType.Wall);
				//    }
				//}

				if (mMode == Mode.Normal) {
					//mLevelTime--;
					if (mLevelTime == 0) {
						AddRandomFood(mInitialFood - GetRemainingFoodCount());
						mLevelTime = InitialLevelTime;
					}
				}
			}

			if (LevelUpdate != null)
				LevelUpdate.Invoke(this, EventArgs.Empty);

			//Repaint
			Invalidate();

			//Callback functions
			switch (mCallbackState) {
				case CallbackState.WinGame:
					mPlaying = false;

					if (LevelComplete != null)
						LevelComplete.Invoke(this, EventArgs.Empty);
					break;
				case CallbackState.LoseGame:
					mPlaying = false;
					if (LevelFail != null)
						LevelFail.Invoke(this, EventArgs.Empty);
					break;
			}

			//Reset callback
			mCallbackState = CallbackState.Null;

			//Update the update counter
			mUpdateCnt = (mUpdateCnt + 1) % UpdatesPerSecond;
		}

		private void UpdateSnakes()
		{
			foreach (Snake s in mSnakes) {
				SnakeUpdateFlag flag = s.Update();
				if ((flag & SnakeUpdateFlag.Crash) > 0) {
					mCallbackState = CallbackState.LoseGame;
				} else if (flag == SnakeUpdateFlag.ReachedExit) {
					mCallbackState = CallbackState.WinGame;
				}
			}
		}

		private void AddRandomFood(int n)
		{
			List<Point> points = new List<Point>();

			for (int x = 0; x < mSize.Width; x++) {
				for (int y = 0; y < mSize.Height; y++) {
					Point p = new Point(x, y);

					if (mNoFoodPoints.Contains(p))
						continue;

					if (GetTileAt(p) == TileType.Null && !IsThereSnakeAt(p))
						points.Add(p);
				}
			}

			//Shuffle
			for (int i = 0; i < points.Count; i++) {
				int r = mRand.Next(0, points.Count);

				//Swap
				Point tmp = points[i];
				points[i] = points[r];
				points[r] = tmp;
			}

			//Set to food
			for (int i = 0; i < Math.Min(n, points.Count); i++) {
				mMap[points[i].X, points[i].Y] = TileType.Food;
			}
		}

		public void FoodCheck()
		{
			int foodCount = 0;
			for (int x = 0; x < mSize.Width; x++) {
				for (int y = 0; y < mSize.Height; y++) {
					if (mMap[x, y] == TileType.Food)
						foodCount++;
				}
			}

			if (mMode == Mode.Classic && foodCount < mInitialFood) {
				AddRandomFood(mInitialFood - foodCount);
			} else if (foodCount == 0) {
				//Open door
				OpenTopDoor();
			}
		}

		public void GetFoodAt(Point specifiedPoint)
		{
			if (GetTileAt(specifiedPoint) != TileType.Food)
				return;

			SetTileAt(specifiedPoint, TileType.Null);
			FoodCheck();
		}

		private void OpenTopDoor()
		{
			SetTileAt(TopDoorLocation, TileType.Null);
		}

		private void CloseTopDoor()
		{
			SetTileAt(TopDoorLocation, TileType.Wall);
		}

		private void OpenBottomDoor()
		{
			SetTileAt(BottomDoorLocation, TileType.Null);
		}

		private void CloseBottomDoor()
		{
			SetTileAt(BottomDoorLocation, TileType.Wall);
		}

		public bool IsThereSnakeAt(Point searchPoint)
		{
			foreach (Snake s in mSnakes) {
				if (s.IsPartlyHere(searchPoint))
					return true;
			}

			return false;
		}

		public int GetRemainingFoodCount()
		{
			int food = 0;
			for (int x = 0; x < MapSize.Width; x++) {
				for (int y = 0; y < MapSize.Height; y++) {
					if (GetTileAt(new Point(x, y)) == TileType.Food)
						food++;
				}
			}

			return food;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Width, Height));
			for (int x = 0; x < Size.Width / TileSize; x++) {
				for (int y = 0; y < Size.Width / TileSize; y++) {
					DrawTile(e.Graphics, TileType.Null, x, y);
				}
			}

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			Font f = new Font("Arial", Math.Min(Width, Height) / 6);

			if (mPreGameTime > 0) {
				//DrawStringCentred(e.Graphics, mPreGameTime.ToString(), f, Color.White, Width / 2, Height / 2);

				DrawASCII(e.Graphics);
			} else if (mPaused) {
				DrawASCII(e.Graphics);
			} else {
				DrawMap(e.Graphics);
				DrawSnakes(e.Graphics);
			}
		}

		private void DrawStringCentred(Graphics g, string s, Font f, Color c, int cx, int cy)
		{
			SizeF size = g.MeasureString(s, f);
			int x = cx - (int)(size.Width / 2.0f);
			int y = cy - (int)(size.Height / 2.0f);
			g.DrawString(s, f, new SolidBrush(c), x, y);
		}

		private void DrawSnakes(Graphics g)
		{
			foreach (Snake s in mSnakes) {
				s.Draw(g);
			}
		}

		private void DrawMap(Graphics g)
		{
			for (int x = 0; x < mSize.Width; x++) {
				for (int y = 0; y < mSize.Height; y++) {
					if (mMap[x, y] != TileType.Null)
						DrawTile(g, mMap[x, y], x, y);
				}
			}
		}

		private void DrawASCII(Graphics g)
		{
			for (int x = 0; x < mASCIISize.Width; x++) {
				for (int y = 0; y < mASCIISize.Height; y++) {
					if (mASCII[x, y] != TileType.Null)
						DrawTile(g, mASCII[x, y], x, y);
				}
			}
		}

		private void DrawTile(Graphics g, TileType type, int x, int y)
		{
			if (type == TileType.Null && mImages.ContainsKey("null"))
				g.DrawImage(mImages["null"], x * TileSize, y * TileSize);
			else if (type == TileType.Wall && mImages.ContainsKey("wall"))
				g.DrawImage(mImages["wall"], x * TileSize, y * TileSize);
			else if (type == TileType.Food && mImages.ContainsKey("food"))
				g.DrawImage(mImages["food"], x * TileSize, y * TileSize);
			else
				DrawTile(g, mTileColours[(int)type], x, y);
		}

		public void DrawTile(Graphics g, Color c, int x, int y)
		{
			int spacing = 1;

			SolidBrush brush = new SolidBrush(c);
			g.FillRectangle(brush, x * TileSize + spacing, y * TileSize + spacing, TileSize - (spacing * 2), TileSize - (spacing * 2));
		}

		private void ClearASCII()
		{
			for (int x = 0; x < mASCIISize.Width; x++) {
				for (int y = 0; y < mASCIISize.Height; y++) {
					mASCII[x, y] = TileType.Null;
				}
			}
		}

		private void WriteASCIICentred(string s, int x, int y)
		{
			Size textSize = mASCIITable.MeasureString(s);
			x = x - (textSize.Width / 2);
			y = y - (textSize.Height / 2);

			foreach (char c in s) {
				x += WriteASCII(c, x, y) + 1;
			}
		}

		private void WriteASCII(string s, int x, int y)
		{
			foreach (char c in s) {
				x += WriteASCII(c, x, y) + 1;
			}
		}

		private int WriteASCII(char c, int dx, int dy)
		{
			bool[,] wall = mASCIITable.GetWallArray(c);
			Size wallSize = mASCIITable.GetCharSize(c);

			for (int x = 0; x < wallSize.Width; x++) {
				for (int y = 0; y < wallSize.Height; y++) {
					if (wall[x, y]) {
						if (dx + x < 0 || dx + x >= mASCIISize.Width || dy + y < 0 || dy + y >= mASCIISize.Height) {
						} else
							mASCII[dx + x, dy + y] = TileType.Wall;
					}
				}
			}

			return wallSize.Width;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (!mPlaying || mPaused)
				return base.ProcessCmdKey(ref msg, keyData);

			if (mControlSnake == null)
				return base.ProcessCmdKey(ref msg, keyData);

			//if (!mControlSnake.HasSnakeMovedSinceDirectionChanged)
			//	return base.ProcessCmdKey(ref msg, keyData);

			switch (keyData) {
				case Keys.Up:
					mControlSnake.SetDirection(Direction.North);
					break;
				case Keys.Down:
					mControlSnake.SetDirection(Direction.South);
					break;
				case Keys.Left:
					mControlSnake.SetDirection(Direction.West);
					break;
				case Keys.Right:
					mControlSnake.SetDirection(Direction.East);
					break;
			}

			mLastKeyUpdate = mUpdateCnt;

			return base.ProcessCmdKey(ref msg, keyData);
		}

		public void SetTileAt(Point specifiedPoint, TileType specifiedValue)
		{
			mMap[specifiedPoint.X, specifiedPoint.Y] = specifiedValue;
		}

		public TileType GetTileAt(Point specifiedPoint)
		{
			return mMap[specifiedPoint.X, specifiedPoint.Y];
		}

		public bool IsPointInLevel(Point specifiedPoint)
		{
			return MapBounds.Contains(specifiedPoint);
		}

		public Point GetDoorLocation(int index)
		{
			switch (index) {
				case 1:
					return TopDoorLocation;
				case 2:
					return LeftDoorLocation;
				case 3:
					return RightDoorLocation;
				default:
					return BottomDoorLocation;
			}
		}

		public Point TopDoorLocation
		{
			get
			{
				return new Point(mSize.Width / 2, 0);
			}
		}

		public Point BottomDoorLocation
		{
			get
			{
				return new Point(mSize.Width / 2, mSize.Height - 1);
			}
		}

		public Point LeftDoorLocation
		{
			get
			{
				return new Point(0, mSize.Height / 2);
			}
		}

		public Point RightDoorLocation
		{
			get
			{
				return new Point(mSize.Width - 1, mSize.Height / 2);
			}
		}

		public Rectangle MapBounds
		{
			get
			{
				return new Rectangle(Point.Empty, MapSize);
			}
		}

		public Size MapSize
		{
			get
			{
				return mSize;
			}
		}

		public int LevelTime
		{
			get
			{
				return mLevelTime;
			}
		}

		public List<Snake> Snakes
		{
			get
			{
				return mSnakes;
			}
		}

		public Snake ControllingSnake
		{
			get
			{
				return mControlSnake;
			}
		}

		public Difficulty Difficulty
		{
			get
			{
				return mDifficulty;
			}
			set
			{
				mDifficulty = value;
			}
		}

		public Mode Mode
		{
			get
			{
				return mMode;
			}
			set
			{
				mMode = value;
			}
		}

		public int ComputerSnakes
		{
			get
			{
				return mComputerSnakes;
			}
			set
			{
				mComputerSnakes = value;
			}
		}

		public bool Demonstration
		{
			get
			{
				return mDemo;
			}
			set
			{
				mDemo = value;
			}
		}

		public bool Paused
		{
			get
			{
				return mPaused;
			}
			set
			{
				mPaused = value;
			}
		}

		public int LevelNumber
		{
			get
			{
				return mLevelNumber;
			}
			set
			{
				mLevelNumber = value;
			}
		}

		public bool NoPreGameScreen
		{
			get
			{
				return mNoPreGameScreen;
			}
			set
			{
				mNoPreGameScreen = value;
			}
		}

		public int ExtendSnakeOnFood
		{
			get
			{
				return mExtendSnakeOnFood;
			}
			set
			{
				mExtendSnakeOnFood = value;
			}
		}

		public bool DisplayMunches
		{
			get
			{
				return mDisplayMunches;
			}
			set
			{
				mDisplayMunches = value;
			}
		}

		public bool ShowingASCII
		{
			get
			{
				return (mPreGameTime > 0 || mPaused);
			}
		}

		public Size ASCIISize
		{
			get
			{
				return mASCIISize;
			}
		}

		public Dictionary<string, Image> Images
		{
			get
			{
				return mImages;
			}
		}
	}
}
