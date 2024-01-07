using System;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	internal struct IntPoint : IEquatable<IntPoint>
	{
		public static IntPoint MaxValue = new IntPoint
		{
			X = int.MaxValue,
			Y = int.MaxValue
		};

		public int X;

		public int Y;

		public IntPoint(int x, int y)
		{
			X = x;
			Y = y;
		}

		public bool IsEqual(IntPoint other)
		{
			return other.X == X && other.Y == Y;
		}

		public override string ToString()
		{
			return string.Format("X: " + X + " - Y: " + Y);
		}

		public bool Equals(IntPoint other)
		{
			return other.X == X && other.Y == Y;
		}

		public override int GetHashCode()
		{
			int num = 0;
			num = (num * 397) ^ X;
			return (num * 397) ^ Y;
		}
	}
}
