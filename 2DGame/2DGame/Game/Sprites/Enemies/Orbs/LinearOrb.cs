using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class LinearOrb : AbstractSprite
	{
		private Vector2 Direction;

		public LinearOrb(Vector2 Position, Vector2 Direction, float Speed) : base("orb", Position)
		{
			this.Direction = Direction;
			this.Direction.Normalize();
			this.Direction *= Speed;
		}

		public override void Update(GameTime gameTime)
		{
			this.Position += Direction;
		}
	}
}
