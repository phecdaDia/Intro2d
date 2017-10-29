using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Sprites
{
	public abstract class AbstractAnimatedSprite : AbstractSprite
	{
		private static Dictionary<Type, Texture2D[]> frameDictionary;

		private int delay;
		private int currentFrame, currentOffset;

		public AbstractAnimatedSprite(String key, int spriteSize, int delay) : base(key)
		{
			if (this.texture.Width % spriteSize != 0) throw new InvalidOperationException("Invalid Sprite size");


			if (frameDictionary == null) frameDictionary = new Dictionary<Type, Texture2D[]>();

			this.delay = delay;

			if (frameDictionary.ContainsKey(this.GetType()))
			{
				this.texture = frameDictionary[this.GetType()][0];
				return;

			}
			Texture2D[] frames = new Texture2D[this.texture.Width / spriteSize];

			Color[] colorData = new Color[this.texture.Height * spriteSize];

			Rectangle r = new Rectangle(0, 0, spriteSize, this.texture.Height);
			for (int w = 0; w < frames.Length; w++)
			{
				r.Location = new Point(w * spriteSize, 0);

				// Getting the data.
				this.texture.GetData<Color>(0, r, colorData, 0, colorData.Length);
				frames[w] = new Texture2D(Game.GetInstance().GraphicsDevice, spriteSize, this.texture.Height);
				frames[w].SetData<Color>(colorData);

			}

			frameDictionary.Add(this.GetType(), frames);
			this.texture = frames[0];
		}

		public AbstractAnimatedSprite(String key, Vector2 position, int spriteSize, int delay) : this(key, spriteSize, delay)
		{
			this.position = position;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			Type t = this.GetType();

			if (++this.currentOffset >= this.delay)
			{
				this.currentOffset %= this.delay;
				if (++this.currentFrame >= frameDictionary[t].Length) this.currentFrame %= frameDictionary[t].Length;


				this.texture = frameDictionary[t][currentFrame];
			}

			base.Draw(spriteBatch);
		}
	}
}
