using Intro2DGame.Game.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public class RandomSpawnerSprite<T> : AbstractSprite where T : AbstractSprite
	{
		private Type objectReference;
		private Random random;

		private List<T> list;

		public RandomSpawnerSprite() : base()
		{
			this.objectReference = typeof(T);
			this.random = new Random();

			this.list = new List<T>();
		}

		int i = 0;
		public override void Update(GameTime gameTime)
		{
			i++;
			if (i >= 1)
			{
				i %= 1;
				T o = (T)Activator.CreateInstance(objectReference, new Vector2(random.Next(800), random.Next(500)));
				list.Add(o);

			}

			foreach (T o in list)
			{
				o.Update(gameTime);
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			foreach (T o in list) o.Draw(spriteBatch);

			spriteBatch.DrawString(Game.FontArial, "" + list.Count, new Vector2(100, 500), Color.Black);
		}
	}
}
