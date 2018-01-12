using System;

namespace Intro2DGame.Game
{
	/// <summary>
	///     All fields must be const.
	/// </summary>
	public static class GameConstants
	{
		// defines some of the game rules

		public static Difficulty Difficulty = Difficulty.Normal;

		public static Random Random = new Random();
	}

	public enum Difficulty
	{
		Easy = 100,
		Normal = 200,
		Difficult = 400,
		Lunatic = 1000
	}
}