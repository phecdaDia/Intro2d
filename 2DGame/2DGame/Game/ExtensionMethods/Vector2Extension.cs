using System;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.ExtensionMethods
{
	public static class Vector2Extension
	{
		public static double ToAngle(this Vector2 vector)
		{
			return Math.Atan2(vector.Y, vector.X);
		}

		public static Vector2 AddDegrees(this Vector2 vector, double degrees)
		{
			return (vector.ToAngle() + degrees.ToRadiants()).ToVector2();
		}
	}
}