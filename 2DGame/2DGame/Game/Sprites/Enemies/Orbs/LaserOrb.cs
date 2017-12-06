﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class LaserOrb : AbstractOrb
	{
		private readonly float ChargeSpan;
		private readonly float LifeSpan;


		public LaserOrb(Vector2 position, Vector2 direction, float chargeSpan = 1.0f, float lifeSpan = 2.5f) : base("OrbLaser", position, direction,
			new Point(32, 8))
		{
			this.ChargeSpan = chargeSpan;
			this.LifeSpan = lifeSpan;

			Scale.X = 100;

			Hue = Color.Red;
		}

		protected override void AddFrames()
		{
			AddAnimation(new []
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
			//base.Update(gameTime);

			if (LifeTime.TotalGameTime.TotalSeconds > this.LifeSpan) this.Delete();
			else if (LifeTime.TotalGameTime.TotalSeconds > this.ChargeSpan)
			{
				this.CurrentFrame = 1;

				foreach (var player in SceneManager.GetSprites<PlayerSprite>())
				{
					//float Inc(Vector2 t) => t.X / t.Y;
					//var j = Inc(Direction) / Inc(player.GetPosition() - this.GetPosition());

					//// This is some black magic. TODO Will change this later
					////if (j > 0.5858f && j < 1.4142f) player.Damage(GameConstants.PLAYER_DAMAGE);

					//var invDir = new Vector2(Direction.Y, Direction.X * -1.0f);

					var ex = Position.X;
					var ey = Position.Y;

					var px = player.GetPosition().X;
					var py = player.GetPosition().Y;

					var dx = Direction.X;
					var dy = Direction.Y;

					var d2x = Direction.Y;
					var d2y = Direction.X * -1.0f;

					// (1): ex + a dx = px + b d'x
					// (2): ey + a dy = py + b d'y

					// solving for a:

					// (1) - d2q (2)
					var d2q = d2x / d2y;

					// ex - d2q ey + a dx - a dy d2q = px - py d2q + 0
					// ex - d2q ey + a (dx - dy d2q) = px - py d2q
					// a (dx - dy d2q) = px - py d2q - ex + d2q ey
					// a = (px - py d2q - ex + d2q ey)/(dx - dy d2q)
					var a = (px - py * d2q - ex + d2q * ey) / (dx - dy * d2q);

					var q = GetPosition() + a * Direction;

					//Console.WriteLine($"Distance: {(player.GetPosition() - q).Length()}");

					// Change this to different hitboxes. 
					if ((player.GetPosition() - q).Length() < 16) player.Damage((int)GameConstants.Difficulty);

				}
			}

		}
		
	}
}
