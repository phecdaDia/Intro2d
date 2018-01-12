using System;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Sprites.Enemies
{
	/// <summary>
	///     First debug enemy
	/// </summary>
	public class DummyEnemy : AbstractSprite
	{
		private readonly double FrameDelay;
		private readonly int LayerDifficulty;

		private double Milliseconds;
		private double q;
		private double z;

		public DummyEnemy(Vector2 position, int maxHealth, double frameDelay, int layerDifficulty) : base("tutorialplayer",
			position)
		{
			Hue = Color.Red;

			Enemy = true;
			MaxHealth = maxHealth;
			Health = maxHealth;

			LayerDepth = 1;

			FrameDelay = frameDelay;
			LayerDifficulty = layerDifficulty;
		}

		public override void Update(GameTime gameTime)
		{
			Milliseconds += gameTime.ElapsedGameTime.Milliseconds;

			float c = 0;
			var p = new Vector2((float) Math.Sin(c), (float) Math.Cos(c));

			if (KeyboardManager.IsKeyDown(Keys.F9)) Health = 0;
			if (KeyboardManager.IsKeyDown(Keys.F10)) Health--;
			if (KeyboardManager.IsKeyDown(Keys.F11)) Health++;


			if (Milliseconds > FrameDelay)
			{
				Milliseconds %= FrameDelay;
				// Shoot something
				var players = SceneManager.GetSprites<PlayerSprite>();
				foreach (var ps in players)
				{
					var dir = ps.Position - Position;
					var tan = 17.5d * 2 * Math.PI; // Math.Atan2(dir.X, dir.Y);
					//var degrees = 2 * Math.PI * (22.5f / 360f);
					tan += q;
					var temp_ = 2 * Math.PI * 0.5d * (1f / LayerDifficulty);
					q += temp_;
					z += temp_ / 2f;

					var degrees2 = 2 * Math.PI * (Health * (Health + 1) / 1.95f / LayerDifficulty);

					for (var i = 0; i < LayerDifficulty; i++)
					{
						var orb = new LinearIncreasingOrb(Position,
							new Vector2((float) Math.Sin(tan + i * degrees2 + z), (float) Math.Cos(tan + i * degrees2 + z)), 0.235f,
							1.01025f);

						var delta = (float) Health / MaxHealth;
						orb.Hue = new Color(1.0f, delta, delta);

						SpawnSprite(orb);
					}
				}
			}
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - Position).Length() <= 16 ||
			       (position - Position - new Vector2(0, 16)).Length() <= 16 ||
			       (position - Position + new Vector2(0, 16)).Length() <= 16
				;
		}
	}
}