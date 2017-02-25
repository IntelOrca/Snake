using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Diagnostics;

namespace Ted.MySnake
{
	class AIastar : IAISnake
	{
		private Random mRand = new Random();
		private SnakeGame mSnakeGame;

		public Point GetNextDestination(Point src)
		{
			//for (int x = 0; x < mSnakeGame.MapSize.Width; x++) {
			//    for (int y = 0; y < mSnakeGame.MapSize.Height; y++) {
			//        Point p = new Point(x, y);
			//        if (mSnakeGame.GetTileAt(p) == TileType.Food) {
			//            return AStar(src, p);
			//        }
			//    }
			//}

			int food = mSnakeGame.GetRemainingFoodCount();

			if (mSnakeGame.Mode == Mode.Normal && food == 0) {
				if (src.Y == 0) {
					return new Point(src.X, src.Y - 1);
				} else {
					return AStar(src, mSnakeGame.TopDoorLocation);
				}
			}

			if (food == 0) {
				return AStar(src, new Point(mRand.Next(1, mSnakeGame.MapSize.Width - 1), mRand.Next(1, mSnakeGame.MapSize.Height - 1)));
			}

			return AStar(src, GetClosestFood(src));
		}

		private Point GetClosestFood(Point src)
		{
			List<Point> food_points = new List<Point>();
			for (int x = 0; x < mSnakeGame.MapSize.Width; x++) {
				for (int y = 0; y < mSnakeGame.MapSize.Height; y++) {
					Point p = new Point(x, y);
					if (mSnakeGame.GetTileAt(p) == TileType.Food) {
						food_points.Add(p);
					}
				}
			}

			food_points.Remove(src);

			int lowestDistance = Int32.MaxValue;
			Point lowestPoint = Point.Empty;
			foreach (Point p in food_points) {
				int dist = dist_between(src, p);
				if (dist < lowestDistance) {
					lowestDistance = dist;
					lowestPoint = p;
				}
			}

			return lowestPoint;
		}

		private Point AStar(Point start, Point goal)
		{
			if (start == goal)
				return start;

			NodeList closedset = new NodeList();
			NodeList openset = new NodeList();

			NodeList g_score = new NodeList();
			NodeList h_score = new NodeList();
			NodeList f_score = new NodeList();

			Node startNode = new Node(start);
			startNode.GScore = 0;
			startNode.HScore = 0;
			startNode.FScore = startNode.HScore;
			openset.Add(startNode);

			while (openset.Count > 0) {
				//Get node with lowest f_score
				Node x = openset.GetLowestFScoreNode();
				if (x.Location == goal) {
					//Reconstruct path
					Node prev = x;
					for (; ; ) {
						if (prev.CameFrom.CameFrom == null) {
							return prev.Location;
						} else {
							prev = prev.CameFrom;
						}
					}
				}

				openset.Remove(x);
				closedset.Add(x);

				Node[] neighbours = GetNeighbourNodes(x);
				foreach (Node y in neighbours) {
					if (closedset.ContainsLocation(y)) {
						continue;
					}

					int tentative_g_score = x.GScore + dist_between(x, y);
					bool tentative_is_better = false;

					if (!openset.ContainsLocation(y)) {
						openset.Add(y);
						tentative_is_better = true;
					} else if (tentative_g_score < y.GScore) {
						tentative_is_better = true;
					}

					if (tentative_is_better) {
						y.CameFrom = x;

						y.GScore = tentative_g_score;
						y.HScore = heuristic_estimate_of_distance(y.Location, goal);
						y.FScore = y.GScore + y.HScore;
					}
				}
			}

			//Fail
			Node[] nn = GetNeighbourNodes(new Node(start));
			if (nn.Length == 0)
				return Point.Empty;

			return nn[mRand.Next(0, nn.Length)].Location;
		}

		private Node[] GetNeighbourNodes(Node n)
		{
			Point mid = n.Location;
			NodeList nodes = new NodeList();
			for (int i = 1; i < (int)Direction.Count; i++) {
				Point delta = DirectionFunctions.GetXY((Direction)i);
				Point newPnt = new Point(mid.X + delta.X, mid.Y + delta.Y);
				if (!mSnakeGame.MapBounds.Contains(newPnt)) {
					continue;
				}

				TileType type = mSnakeGame.GetTileAt(newPnt);
				if (type != TileType.Null && type != TileType.Food) {
					continue;
				}

				if (mSnakeGame.IsThereSnakeAt(newPnt)) {
					continue;
				}

				nodes.Add(new Node(newPnt));
			}

			return nodes.ToArray();
		}

		private int heuristic_estimate_of_distance(Point start, Point goal)
		{
			return Math.Abs(start.X - goal.X) + Math.Abs(start.Y - goal.Y);
		}

		private int dist_between(Node a, Node b)
		{
			return dist_between(a.Location, b.Location);
		}

		private int dist_between(Point a, Point b)
		{
			int dx = Math.Abs(a.X - b.X);
			int dy = Math.Abs(a.Y - b.Y);

			return (int)Math.Sqrt((dx * dx) + (dy * dy));
		}

		public SnakeGame SnakeGame
		{
			get
			{
				return mSnakeGame;
			}
			set
			{
				mSnakeGame = value;
			}
		}
	}
}
