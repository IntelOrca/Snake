using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ted.MySnake
{
	interface IAISnake
	{
		Point GetNextDestination(Point src);
		SnakeGame SnakeGame
		{
			get;
			set;
		}
	}
}
