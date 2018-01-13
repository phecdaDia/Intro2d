using System;
using System.Collections.Generic;
using System.Linq;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Scenes.Debug
{
	/// <summary>
	///     This scene is used for a debug fight
	/// </summary>
	public class ExampleScene : Scene
	{
		public ExampleScene() : base("example")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(250, 750)));

			for (var i = 0; i < 32; i++)
			{
				AddSprite(new GravityOrb(new Vector2(i * (500 / 32f), 50)));
			}



		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			var i = 0;
			foreach (var t in new List<Type>(GetAllSprites().Keys))
			foreach (AbstractSprite de in GetAllSprites()[t])
			{
				if (!de.Enemy) continue;
				spriteBatch.DrawString(Game.FontArial, $"{de.GetType().FullName?.Split('.').Last()}: {de.Health}",
					new Vector2(30, 110 + i++ * 20), Color.Black);
			}
		}
	}

	public class GravityOrb : AbstractOrb
	{
		private Vector2 LastVector2;


		public GravityOrb(Vector2 position) : base("orb3", position, new Vector2())
		{
			this.LastVector2 = this.Direction;

			this.Persistence = true;
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			// get the player
			var player = SceneManager.GetSprites<PlayerSprite>().First();

			var temp = player.Position - this.Position;
			temp.Normalize();
			temp *= 300.0f / (player.Position - this.Position).Length();

			temp *= (float)gameTime.ElapsedGameTime.TotalSeconds;

			LastVector2 = LastVector2 + temp;
			return LastVector2;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var players = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in players)
			{
				if (!ps.DoesCollide(this)) continue;

				ps.Damage((int)GameConstants.Difficulty);
				Delete();
			}
		}
	}
}