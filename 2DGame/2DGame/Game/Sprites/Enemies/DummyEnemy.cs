using System;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Sprites.Enemies
{
    /// <summary>
    /// First debug enemy
    /// </summary>
	public class DummyEnemy : AbstractSprite
	{
		private double q;
		private int timer;
		private double z;

		private readonly int FrameDelay, LayerDifficulty;

		public DummyEnemy(Vector2 position, int maxHealth, int frameDelay, int layerDifficulty) : base("orb3", position)
		{
			Hue = Color.Red;

			Enemy = true;
			MaxHealth = maxHealth;
			Health = maxHealth;

			LayerDepth = 1;

			this.FrameDelay = frameDelay;
			this.LayerDifficulty = layerDifficulty;
		}

		public override void Update(GameTime gameTime)
		{
			float c = 0;
			var p = new Vector2((float) Math.Sin(c), (float) Math.Cos(c));

			if (KeyboardManager.IsKeyDown(Keys.F9)) Health = 0;
			if (KeyboardManager.IsKeyDown(Keys.F10)) Health--;
			if (KeyboardManager.IsKeyDown(Keys.F11)) Health++;


			if (++timer > FrameDelay)
			{
				timer %= FrameDelay;
				// Shoot something
				var players = SceneManager.GetSprites<PlayerSprite>();
				foreach (var ps in players)
				{
					var dir = ps.GetPosition() - GetPosition();
					var tan = 17.5d * 2 * Math.PI; // Math.Atan2(dir.X, dir.Y);
					var degrees = 2 * Math.PI * (22.5f / 360f);
					tan += q;
					var temp_ = 2 * Math.PI * 0.5d * (1f / LayerDifficulty);
					q += temp_;
					z += temp_ / 2f;

					var degrees2 = 2 * Math.PI * (Health * (Health+1) / 1.95f / LayerDifficulty);

					for (var i = 0; i < LayerDifficulty; i++)
						SpawnSprite(new LinearIncreasingOrb(Position,
							new Vector2((float) Math.Sin(tan + i * degrees2 + z), (float) Math.Cos(tan + i * degrees2 + z)), 0.235f,
							1.01025f));
				}
			}
		}

		public override bool DoesCollide(AbstractOrb orb)
		{
			return (orb.GetPosition() - this.GetPosition()).Length() <= 16;
		}
	}
}