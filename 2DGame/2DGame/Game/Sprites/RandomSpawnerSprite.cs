using System;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public class RandomSpawnerSprite<T> : AbstractSprite where T : AbstractSprite
	{
		private readonly int MaxAmount = -1;
		private readonly Type ObjectReference;
		private readonly Random Random;

		private int i;

		public RandomSpawnerSprite()
		{
			ObjectReference = typeof(T);
			Random = new Random();
		}

		public RandomSpawnerSprite(int max) : this()
		{
			MaxAmount = max;
		}

		public override void Update(GameTime gameTime)
		{
			var list = SceneManager.GetSprites<T>();

			if (MaxAmount > 0 && list.Count < MaxAmount)
			{
				i++;
				if (i >= 1)
				{
					i %= 1;
					var o = (T) Activator.CreateInstance(ObjectReference, new Vector2(Random.Next(800), Random.Next(500)));
					SceneManager.GetCurrentScene().AddSprite(o);
				}
			}

			foreach (var o in list)
				o.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Game.FontArial, "" + SceneManager.GetSprites<T>().Count, new Vector2(100, 500), Color.Black);
		}
	}
}