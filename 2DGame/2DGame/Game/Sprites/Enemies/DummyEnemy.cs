using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Intro2DGame.Game.Sprites.Enemies.Orbs;

namespace Intro2DGame.Game.Sprites.Enemies
{
	public class DummyEnemy : AbstractEnemy
	{
		public DummyEnemy(Vector2 position) : base("player", position)
		{
			this.Hue = Color.Red;
		}


		private float f = 0.0f;
		public override void Update(GameTime gameTime)
		{
			f += (float)(Math.PI / 360);
			Vector2 p = new Vector2((float)Math.Sin(f), (float)Math.Cos(f));

			ShootOrb<LinearOrb>(this.Position, p, 2);
		}
	}
}
