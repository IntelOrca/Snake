using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ted.MySnake
{
	class Node
	{
		private Point mLocation;
		private int mGScore;
		private int mHScore;
		private int mFScore;
		private Node mCameFrom;

		public Node(Point location)
		{
			mLocation = location;
		}

		public override string ToString()
		{
			return String.Format("({0}, {1}) G: {2} H: {3} F: {4}", mLocation.X, mLocation.Y, mGScore, mHScore, mFScore);
		}

		public Point Location
		{
			get
			{
				return mLocation;
			}
			set
			{
				mLocation = value;
			}
		}

		public int GScore
		{
			get
			{
				return mGScore;
			}
			set
			{
				mGScore = value;
			}
		}

		public int HScore
		{
			get
			{
				return mHScore;
			}
			set
			{
				mHScore = value;
			}
		}

		public int FScore
		{
			get
			{
				return mFScore;
			}
			set
			{
				mFScore = value;
			}
		}

		public Node CameFrom
		{
			get
			{
				return mCameFrom;
			}
			set
			{
				mCameFrom = value;
			}
		}
	}
}
