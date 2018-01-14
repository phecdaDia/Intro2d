using System;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.ExtensionMethods
{
	public static class DoubleExtensionMethods
	{
		public static double ToRadiants(this double number)
		{
			return number / 360d * 2 * Math.PI;
		}

		public static double ToDegrees(this double number)
		{
			return number * 360d / (2 * Math.PI);
		}

		public static Vector2 ToVector2(this double degrees)
		{
			return new Vector2((float) Math.Cos(degrees), (float) Math.Sin(degrees));
		}
	}
}