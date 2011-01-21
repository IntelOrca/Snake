using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Ted.MySnake
{
	class ASCIILevelTable : IEnumerable<ASCIILevel>
	{
		List<ASCIILevel> mLevels = new List<ASCIILevel>();

		public ASCIILevelTable()
		{

		}

		public ASCIILevelTable(string filename)
		{
			Open(filename);
		}

		public void Open(string filename)
		{
			mLevels.Clear();

			string[] lines = File.ReadAllLines(filename);
			string name;
			int width, height;

			int i;
			for (i = 0; i < lines.Length; i++) {
				if (lines[i].Length < 2)
					continue;

				if (lines[i][0] != '-')
					continue;

				name = lines[i].Substring(1).Trim();
				width = 0;
				height = 0;

				for (int j = i + 1; j < lines.Length; j++) {
					if (lines[j].Length == 0)
						break;

					height++;

					if (lines[j].Length > width)
						width = lines[j].Length;
				}

				ASCIILevel level = new ASCIILevel();
				level.Name = name;
				level.Size = new Size(width, height);
				level.Grid = new TileType[width, height];

				for (int j = i + 1; j < lines.Length; j++) {
					if (lines[j].Length == 0)
						break;

					for (int k = 0; k < lines[j].Length; k++) {
						TileType tileType = TileType.Null;

						switch (Char.ToUpper(lines[j][k])) {
							case 'X':
								tileType = TileType.Wall;
								break;
							case '#':
								level.NoFoodPoints.Add(new Point(k, j - i - 1));
								break;
						}
						level.Grid[k, j - i - 1] = tileType;
					}
				}

				mLevels.Add(level);
			}
		}

		public ASCIILevel GetLevel(int index)
		{
			return mLevels[index];
		}

		public IEnumerator<ASCIILevel> GetEnumerator()
		{
			return mLevels.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return mLevels.GetEnumerator();
		}

		public ASCIILevel this[int index]
		{
			get
			{
				return GetLevel(index);
			}
		}

		public int Count
		{
			get
			{
				return mLevels.Count;
			}
		}
	}
}
