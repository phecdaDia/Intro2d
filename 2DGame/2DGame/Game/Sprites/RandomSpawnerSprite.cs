using Intro2DGame.Game.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Intro2DGame.Game.Scenes;

namespace Intro2DGame.Game.Sprites
{
	public class RandomSpawnerSprite<T> : AbstractSprite where T : AbstractSprite
	{
		private Type objectReference;
		private Random random;

		private int MaxAmount = -1;

		public RandomSpawnerSprite() : base()
		{
			this.objectReference = typeof(T);
			this.random = new Random();
		}
		public RandomSpawnerSprite(int max) : this()
		{
			this.MaxAmount = max;
		}

		int i = 0;
		public override void Update(GameTime gameTime)
		{
			List<T> list = SceneManager.GetSprites<T>();

			if (this.MaxAmount > 0 && list.Count < this.MaxAmount)
			{
				i++;
				if (i >= 1)
				{
					i %= 1;
					T o = (T)Activator.CreateInstance(objectReference, new Vector2(random.Next(800), random.Next(500)));
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
