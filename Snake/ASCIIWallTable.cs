using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Ted.MySnake
{
	class ASCIIWallTable
	{
		Dictionary<char, bool[,]> mCharacters = new Dictionary<char, bool[,]>();
		Dictionary<char, Size> mCharacterSizes = new Dictionary<char, Size>();

		int mHighestCharLength;

		public ASCIIWallTable()
		{

		}

		public ASCIIWallTable(string filename)
		{
			Open(filename);
		}

		public void Open(string filename)
		{
			string[] lines = File.ReadAllLines(filename);
			int width, height;

			int i;
			for (i = 0; i < lines.Length; i++) {
				if (lines[i].Length != 2)
					continue;

				if (lines[i][0] != '-')
					continue;

				char c = lines[i][1];
				width = 0;
				height = 0;

				for (int j = i + 1; j < lines.Length; j++) {
					if (lines[j].Length == 0)
						break;

					height++;

					if (lines[j].Length > width)
						width = lines[j].Length;
				}

				if (mHighestCharLength < height)
					mHighestCharLength = height;

				mCharacterSizes.Add(c, new Size(width, height));
				bool[,] charWall = new bool[width, height];

				for (int j = i + 1; j < lines.Length; j++) {
					if (lines[j].Length == 0)
						break;

					for (int k = 0; k < lines[j].Length; k++) {
						if (Char.ToUpper(lines[j][k]) == 'X')
							charWall[k, j - i - 1] = true;
					}
				}

				mCharacters.Add(c, charWall);
			}
		}

		public Size GetCharSize(char c)
		{
			if (!mCharacterSizes.ContainsKey(c))
				return new Size(0, 0);

			return mCharacterSizes[c];
		}

		public bool[,] GetWallArray(char c)
		{
			if (!mCharacterSizes.ContainsKey(c))
				return new bool[0, 0];

			return mCharacters[c];
		}

		public Size MeasureString(string s)
		{
			Size total = new Size();
			foreach (char c in s) {
				Size csize = GetCharSize(c);
				total.Width += csize.Width + 1;
				total.Height = Math.Max(total.Height, csize.Height);
			}

			if (total.Width > 0)
				total.Width--;

			return total;
		}

		public int GetHeight()
		{
			return mHighestCharLength;
		}
	}
}
