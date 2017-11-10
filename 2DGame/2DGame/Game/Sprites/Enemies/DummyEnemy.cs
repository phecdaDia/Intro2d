using System;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies
{
	public class DummyEnemy : AbstractSprite
	{
		private double q;
		private int timer;
		private double z;

		public DummyEnemy(Vector2 position) : base("orb", position)
		{
			Hue = Color.Red;

			Enemy = true;
			MaxHealth = 20;
			Health = 20;
		}

		public override void Update(GameTime gameTime)
		{
			float c = 0;
			var p = new Vector2((float) Math.Sin(c), (float) Math.Cos(c));

			const int frames = 15;
			if (++timer > frames)
			{
				timer %= frames;
				// Shoot something
				var players = SceneManager.GetSprites<PlayerSprite>();
				foreach (var ps in players)
				{
					var dir = ps.GetPosition() - GetPosition();
					var tan = 22.5d * 2 * Math.PI; // Math.Atan2(dir.X, dir.Y);
					var degrees = 2 * Math.PI * (22.5f / 360f);
					var k = 18;
					tan += q;
					var temp_ = 2 * Math.PI * 0.5d * (1f / k);
					q += temp_;
					z += temp_ / 2f;

					var degrees2 = 2 * Math.PI * ((Health / 2.35f) / k);

					for (var i = 0; i < k; i++)
						ShootOrb<LinearIncreasingOrb>(Position,
							new Vector2((float) Math.Sin(tan + i * degrees2 + z), (float) Math.Cos(tan + i * degrees2 + z)), 0.235f,
							1.01025f);
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.DrawString(Game.FontArial, $"Enemy: {Health}", new Vector2(30, 50), Color.Black);
		}
	}
}