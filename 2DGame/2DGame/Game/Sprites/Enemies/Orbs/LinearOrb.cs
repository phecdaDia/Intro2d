using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class LinearOrb : AbstractOrb
	{
		private readonly Vector2 Direction;

		public LinearOrb(Vector2 position, Vector2 direction, float speed) : base("orb", position)
		{
			direction.Normalize();
			direction *= speed;

			this.Direction = direction;
		}

		protected override void UpdatePosition(GameTime gameTime)
		{
			this.Position += Direction;
		}
	}
}
