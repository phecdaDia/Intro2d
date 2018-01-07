using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Movement
{
	public class InstantMovePattern : IPattern
	{
		private Vector2 Delta;

		public InstantMovePattern(Vector2 delta)
		{
			this.Delta = delta;
		}
		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			host.Position += this.Delta;
			return true;
		}
	}
}
