using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ted.MySnake
{
	enum Direction
	{
		Null,
		North,
		East,
		South,
		West,
		Count,
	}

	static class DirectionFunctions
	{
		public static int GetX(Direction specifiedDirection)
		{
			if (specifiedDirection == Direction.West)
				return -1;
			else if (specifiedDirection == Direction.East)
				return 1;
			else
				return 0;
		}

		public static int GetY(Direction specifiedDirection)
		{
			if (specifiedDirection == Direction.North)
				return -1;
			else if (specifiedDirection == Direction.South)
				return 1;
			else
				return 0;
		}

		public static Point GetXY(Direction specifiedDirection)
		{
			return new Point(GetX(specifiedDirection), GetY(specifiedDirection));
		}

		public static Direction GetDirection(Point src, Point dest)
		{
			Point delta = new Point(dest.X - src.X, dest.Y - src.Y);
			for (int i = 0; i < (int)Direction.Count; i++) {
				if (delta == GetXY((Direction)i))
					return (Direction)i;
			}

			return Direction.Null;
		}

		public static Direction Opposite(Direction specifiedDirection)
		{
			switch (specifiedDirection) {
				case Direction.North:
					return Direction.South;
				case Direction.West:
					return Direction.East;
				case Direction.East:
					return Direction.West;
				case Direction.South:
					return Direction.North;
				default:
					return specifiedDirection;
			}
		}
	}
}
