using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.ExtensionMethods
{
	public static class DoubleExtensionMethods
	{
		public static double ToDegrees(this double number) => number / 360d * 2 * Math.PI;
		public static Vector2 ToVector2(this double degrees) => new Vector2((float)Math.Cos(degrees), (float)Math.Sin(degrees));
	}
}
