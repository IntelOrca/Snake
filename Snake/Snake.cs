using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ted.MySnake
{
	class Snake
	{
		private const int ExitLevelSpeed = SnakeGame.UpdatesPerSecond;

		private SnakeGame mSnakeGame;

		private Color mColour;
		private IAISnake mAI;
		private Direction mDirection;
		private Point mHeading;
		private List<Point> mBody = new List<Point>();
		private float mSpeed;
		private int mUpdateCnt = 0;
		private bool mExitingLevel;
		private int mScore;

		private int mExtendSnakeOnFood = 1;
		private bool mDisplayMunches = false;

		private bool mHasSnakeMovedSinceDirectionChange = true;

		private int mFoodCount;

		public Snake(SnakeGame game, Point start, int length, Color c)
		{
			mSnakeGame = game;

			//Add the head, always needed
			mBody.Add(start);

			//Add the rest of the body
			ExtendSnake(length - 1);

			mAI = new AIastar();
			mAI.SnakeGame = mSnakeGame;

			mColour = c;
		}

		public void Draw(Graphics g)
		{
			foreach (Point p in mBody) {
			    mSnakeGame.DrawTile(g, mColour, p.X, p.Y);
			}

			if (mDisplayMunches) {
				Font f = new Font("Arial", 12, FontStyle.Bold);
				string szFoodCount = mFoodCount.ToString();
				for (int i = 0; i < szFoodCount.Length; i++) {
					g.DrawString(szFoodCount[i].ToString(), f, Brushes.Black, mBody[i].X * SnakeGame.TileSize, mBody[i].Y * SnakeGame.TileSize);
				}
			}
		}

		public SnakeUpdateFlag Update()
		{
			mUpdateCnt = (mUpdateCnt + 1) % (int)((float)SnakeGame.UpdatesPerSecond / mSpeed);
			if (mUpdateCnt != 0)
				return SnakeUpdateFlag.Normal;

			AIUpdate();

			Direction d = firstDirectionMove;
			if (d == Direction.Null)
				d = mDirection;

			Point delta = DirectionFunctions.GetXY(d);

			Point dest = new Point(delta.X + Head.X, delta.Y + Head.Y);
			if (dest == Head)
				return SnakeUpdateFlag.Normal;

			//Check if snake has reached level exit
			if (dest.X == mSnakeGame.MapSize.Width / 2 && dest.Y == -1) {
				mSpeed = ExitLevelSpeed;
				mExitingLevel = true;
			}

			//Check if snake is exiting the level
			if (mExitingLevel) {
				//Continue the move out of the level
				Move();

				//Check if whole snake has exited
				if (HasCompletelyExited()) {
					AddScore(10);
					return SnakeUpdateFlag.ReachedExit;
				}

				//Return normal as snake has not completely exited yet
				return SnakeUpdateFlag.Normal;
			}

			//Check if snake has hit the edge of the level
			if (!mSnakeGame.IsPointInLevel(dest)) {
				return SnakeUpdateFlag.HitEdge;
			}

			//Check if snake has hit a snake
			if (mSnakeGame.IsThereSnakeAt(dest)) {
				return SnakeUpdateFlag.HitSnake;
			}

			//Get the tile at dest
			TileType type = mSnakeGame.GetTileAt(dest);
			switch (type) {
				case TileType.Wall:
					return SnakeUpdateFlag.HitWall;
				case TileType.Food:
					mSnakeGame.GetFoodAt(dest);
					mFoodCount++;
					AddScore(1);

					ExtendSnake(mExtendSnakeOnFood);
					break;
			}

			//Move the snake
			Move();

			return SnakeUpdateFlag.Normal;
		}

		private void AIUpdate()
		{
			//If there is no AI then do nothing
			if (mAI == null)
				return;

			Point destPoint = mAI.GetNextDestination(Head);
			Direction d = DirectionFunctions.GetDirection(Head, destPoint);

			firstDirectionMove = Direction.Null;
			secondDirectionMove = Direction.Null;
			mDirection = d;

			if (d == Direction.Null && !mExitingLevel) {
				Reverse();
				AddScore(-1);
			}
		}

		private void ExtendSnake()
		{
			ExtendSnake(1);
		}

		private void ExtendSnake(int length)
		{
			for (int i = 0; i < length; i++)
				mBody.Add(Tail);
		}

		private void Move()
		{
			FinaliseDirection();
			for (int i = mBody.Count - 1; i >= 1; i--) {
				mBody[i] = mBody[i - 1];
			}

			Point delta = DirectionFunctions.GetXY(mDirection);
			Head = new Point(delta.X + Head.X, delta.Y + Head.Y);

			mHasSnakeMovedSinceDirectionChange = true;
		}

		private void Reverse()
		{
			mBody.Reverse();
		}

		private bool HasCompletelyExited()
		{
			return (Tail.Y == -1);
		}

		private Direction firstDirectionMove;
		private Direction secondDirectionMove;
		public void SetDirection(Direction specifiedDirection)
		{
			if (mExitingLevel)
				return;

			if (mDirection == Direction.East || mDirection == Direction.West) {
				if ((specifiedDirection == Direction.East || specifiedDirection == Direction.West) && firstDirectionMove != Direction.Null)
					secondDirectionMove = specifiedDirection;
				else
					firstDirectionMove = specifiedDirection;
			} else if (mDirection == Direction.North || mDirection == Direction.South) {
				if ((specifiedDirection == Direction.North || specifiedDirection == Direction.South) && firstDirectionMove != Direction.Null)
					secondDirectionMove = specifiedDirection;
				else
					firstDirectionMove = specifiedDirection;
			} else {
				firstDirectionMove = specifiedDirection;
			}

			if (firstDirectionMove == DirectionFunctions.Opposite(mDirection))
				firstDirectionMove = Direction.Null;
		}

		private void FinaliseDirection()
		{
			if (firstDirectionMove != Direction.Null) {
				mDirection = firstDirectionMove;
			}
			firstDirectionMove = secondDirectionMove;
			secondDirectionMove = Direction.Null;
			mHeading = DirectionFunctions.GetXY(mDirection);
			mHasSnakeMovedSinceDirectionChange = false;
		}

		public bool IsPartlyHere(Point searchPoint)
		{
			foreach (Point p in mBody) {
				if (p == searchPoint)
					return true;
			}

			return false;
		}

		public void AddScore(int score)
		{
			mScore += score + (((int)mSnakeGame.Difficulty + 1) * mSnakeGame.Snakes.Count);
		}

		public Point Head
		{
			get
			{
				return mBody[0];
			}
			set
			{
				mBody[0] = value;
			}
		}

		public Point Tail
		{
			get
			{
				return mBody[mBody.Count - 1];
			}
			set
			{
				mBody[mBody.Count - 1] = value;
			}
		}

		public IAISnake AI
		{
			get
			{
				return mAI;
			}
			set
			{
				mAI = value;
			}
		}

		public float Speed
		{
			get
			{
				return mSpeed;
			}
			set
			{
				mSpeed = value;
			}
		}

		public bool HasSnakeMovedSinceDirectionChanged
		{
			get
			{
				return mHasSnakeMovedSinceDirectionChange;
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

		public int Score
		{
			get
			{
				return mScore;
			}
		}
	}
}
