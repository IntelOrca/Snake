using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ted.MySnake
{
	[Flags]
	enum SnakeUpdateFlag
	{
		Normal = 1,
		ReachedExit = 2,
		HitWall = 4,
		HitEdge = 8,
		HitSnake = 16,
		Crash = HitWall | HitEdge | HitSnake,
	}
}
