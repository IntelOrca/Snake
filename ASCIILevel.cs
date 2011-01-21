using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Ted.MySnake
{
	class ASCIILevel
	{
		string mName = String.Empty;
		Size mSize;
		TileType[,] mGrid;
		List<Point> mNoFoodPoints = new List<Point>();

		public ASCIILevel()
		{

		}

		public ASCIILevel(string filename)
		{
			List<string> lines = new List<string>(File.ReadAllLines(filename));
			for (int i = 0; i < lines.Count; i++) {
				if (lines[i].Length == 0) {
					lines[i].Remove(i);
					i--;
				}
			}

			int space = 0;

			mNoFoodPoints = new List<Point>();
			mSize = new Size(lines[0].Length, lines.Count);
			mGrid = new TileType[mSize.Width, mSize.Height];
			for (int y = 0; y < lines.Count; y++) {
				for (int x = 0; x < lines[y].Length; x++) {
					switch (Char.ToUpper(lines[y][x])) {
						case 'X':
							mGrid[x, y] = TileType.Wall;
							break;
						case '#':
							mNoFoodPoints.Add(new Point(x, y));
							break;
						default:
							space++;
							break;
					}
				}
			}

			mName = Path.GetFileNameWithoutExtension(filename);
		}

		public int GetSpace()
		{
			int space = 0;
			for (int y = 0; y < mSize.Height; y++) {
				for (int x = 0; x < mSize.Width; x++) {
					if (mGrid[x, y] == TileType.Null)
						space++;
				}
			}

			return space;
		}

		public TileType[,] GetGridCopy()
		{
			return (TileType[,])mGrid.Clone();
		}

		public Image GetThumbnail(int size)
		{
			Bitmap bmp = new Bitmap(mSize.Width * size, mSize.Height * size);
			Graphics g = Graphics.FromImage(bmp);

			g.Clear(Color.Black);

			for (int y = 0; y < mSize.Height; y++) {
				for (int x = 0; x < mSize.Width; x++) {
					if (mGrid[x, y] == TileType.Wall)
						g.FillRectangle(Brushes.Blue, x * size, y * size, size, size);
				}
			}

			g.Dispose();

			return bmp;
		}

		public override string ToString()
		{
			return mName;
		}

		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		public Size Size
		{
			get
			{
				return mSize;
			}
			set
			{
				mSize = value;
			}
		}

		public TileType[,] Grid
		{
			get
			{
				return mGrid;
			}
			set
			{
				mGrid = value;
			}
		}

		public List<Point> NoFoodPoints
		{
			get
			{
				return mNoFoodPoints;
			}
		}
	}
}
