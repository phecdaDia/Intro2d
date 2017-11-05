using System;
using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public class RandomSpawnerSprite<T> : AbstractSprite where T : AbstractSprite
	{
		private readonly Type ObjectReference;
		private readonly Random Random;

		private readonly int MaxAmount = -1;

		public RandomSpawnerSprite()
		{
			this.ObjectReference = typeof(T);
			this.Random = new Random();
		}
		public RandomSpawnerSprite(int max) : this()
		{
			this.MaxAmount = max;
		}

		int i;
		public override void Update(GameTime gameTime)
		{
			List<T> list = SceneManager.GetSprites<T>();

			if (this.MaxAmount > 0 && list.Count < this.MaxAmount)
			{
				i++;
				if (i >= 1)
				{
					i %= 1;
					T o = (T)Activator.CreateInstance(ObjectReference, new Vector2(Random.Next(800), Random.Next(500)));
					SceneManager.GetCurrentScene().AddSprite(o);

				}
			}

			foreach (T o in list)
			{
				o.Update(gameTime);
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			spriteBatch.DrawString(Game.FontArial, "" + SceneManager.GetSprites<T>().Count, new Vector2(100, 500), Color.Black);
		}
	}
}
