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
		// Dictionary to improve performance, since we'll use many objects at once
		private static Dictionary<Type, Texture2D[]> FrameDictionary;

		// delay until next frame
		private readonly int Delay;

		// current frame and the offset until the next frame.
		private int CurrentFrame, CurrentOffset;

		public AbstractAnimatedSprite(string key, int spriteSize, int delay) : base(key)
		{
			// If the sprite is of invalid size we'll have to throw an exception
			if (this.Texture.Width % spriteSize != 0) throw new InvalidOperationException("Invalid Sprite size");

			// Check if we already created our dictionary
			if (FrameDictionary == null) FrameDictionary = new Dictionary<Type, Texture2D[]>();

			// setting the delay
			this.Delay = delay;

			// If this sprite already has everything loaded, just use that. 
			if (FrameDictionary.ContainsKey(this.GetType()))
			{
				this.Texture = FrameDictionary[this.GetType()][0];
				return;

			}

			// ... otherwise we'll have to load it. 
			Texture2D[] frames = new Texture2D[this.Texture.Width / spriteSize];

			// Loading each frame individually
			Color[] colorData = new Color[this.Texture.Height * spriteSize];

			// This just puts everything together.
			Rectangle r = new Rectangle(0, 0, spriteSize, this.Texture.Height);
			for (int w = 0; w < frames.Length; w++)
			{
				r.Location = new Point(w * spriteSize, 0);

				// Getting the data.
				this.Texture.GetData<Color>(0, r, colorData, 0, colorData.Length);
				frames[w] = new Texture2D(Game.GetInstance().GraphicsDevice, spriteSize, this.Texture.Height);
				frames[w].SetData<Color>(colorData);

			}

			// Add our frames to the dictionary
			FrameDictionary.Add(this.GetType(), frames);
			this.Texture = frames[0];
		}

		public AbstractAnimatedSprite(string key, Vector2 position, int spriteSize, int delay) : this(key, spriteSize, delay)
		{
			this.Position = position;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			Type t = this.GetType();

			if (++this.CurrentOffset >= this.Delay)
			{
				this.CurrentOffset %= this.Delay;
				if (++this.CurrentFrame >= FrameDictionary[t].Length) this.CurrentFrame %= FrameDictionary[t].Length;


				this.Texture = FrameDictionary[t][CurrentFrame];
			}

			base.Draw(spriteBatch);
		}
	}
}
