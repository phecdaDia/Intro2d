using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game
{
	/// <summary>
	/// All fields must be const.
	/// </summary>
	public static class GameConstants
	{
		// defines some of the game rules

		public const int PLAYER_DAMAGE = 200; // damage the player takes when getting hit

		public const bool IS_AUTOREGEN_ENABLED = true; // if the player does auto regenerate
		public const bool IS_AUTOREGEN_RESTRICTED = true; // if the player only auto regenerates if he doesn't shoot.
		public const bool IS_MOVEMENT_RESTRICTED = true; // if the player moves slower when shooting.
	}
}
