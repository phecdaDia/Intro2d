using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.ExtensionMethods
{
	public static class Vector2Extension
	{
		public static double ToAngle(this Vector2 vector) => Math.Atan2(vector.Y, vector.X);
		private static Vector2 ToVector2(this double degrees) => new Vector2((float)Math.Cos(degrees), (float)Math.Sin(degrees));
		public static Vector2 AddDegrees(this Vector2 vector, double degrees) => (vector.ToAngle() + degrees.ToDegrees()).ToVector2();
		private static double ToDegrees(this double number) => number / 360d * 2 * Math.PI;
	}
}
