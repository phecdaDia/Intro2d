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
		public static Vector2 AddDegrees(this Vector2 vector, double degrees) => (vector.ToAngle() + degrees.ToDegrees()).ToVector2();
	}
}
