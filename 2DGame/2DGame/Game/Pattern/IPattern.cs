using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	interface IPattern
	{
		/// <summary>
		/// Executes a pattern.
		/// Returns true if the pattern is completed. 
		/// </summary>
		/// <param name="host"></param>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		bool Execute(AbstractSprite host, GameTime gameTime);
	}
}