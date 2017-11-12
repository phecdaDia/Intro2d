using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intro2DGame.Game.Scenes
{
	public class Example2Scene : Scene
	{
		public Example2Scene() : base("example2")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 350)));
			//this.AddSprite(new AnimationTestSprite(new Vector2(200, 200)));
			//this.AddSprite(new RandomSpawnerSprite<OrbSprite>(1000));

			//AddSprite(new DummyEnemy(new Vector2(700, 350)));
			AddSprite(new DummyEnemy(new Vector2(700, 250), 100, 20, 13));
			AddSprite(new DummyEnemy(new Vector2(700, 450), 100, 20, 13));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			int i = 0;
			foreach (Type t in new List<Type>(GetAllSprites().Keys))
			{
				foreach (AbstractSprite de in GetAllSprites()[t])
				{
					if (!de.Enemy) continue;
					spriteBatch.DrawString(Game.FontArial, $"{de.GetType().FullName?.Split('.').Last()}: {de.Health}", new Vector2(30, 110 + (i++ * 20)), Color.Black);
				}
			}
		}
	}
}