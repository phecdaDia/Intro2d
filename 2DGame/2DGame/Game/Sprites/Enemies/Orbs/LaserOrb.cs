using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class LaserOrb : AbstractOrb
	{
		public LaserOrb(Vector2 position, Vector2 direction) : base("OrbLaser", position, direction, 1000, new Point(32, 8))
		{
			Scale.X = 100;

			Hue = Color.Red;
			
			
		}

		protected override void AddFrames()
		{
			AddAnimation(new Point[]
			{
				new Point(0, 0),
				new Point(32, 0),
			});
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			return Vector2.Zero;

		}
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (LifeTime.TotalGameTime.TotalSeconds > 1.5f) this.Delete();
			else if (LifeTime.TotalGameTime.TotalSeconds > 1.0f)
			{
				foreach (var player in SceneManager.GetSprites<PlayerSprite>())
				{
					float Inc(Vector2 t) => t.X / t.Y;
					var j = Inc(Direction) / Inc(player.GetPosition() - this.GetPosition());

					// This is some black magic. TODO Will change this later
					if (j > 0.5858f && j < 1.4142f) player.Damage(GameConstants.PLAYER_DAMAGE);
				}
			}

		}
		
	}
}
