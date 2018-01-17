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

			for (var j = 0; j < 32; j++)
			for (var i = 0; i < 32; i++)
			{
				AddSprite(new GravityOrb(new Vector2(i * (500 / 32f), 50 + j * 5), new Vector2(0, 3f)));
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
}