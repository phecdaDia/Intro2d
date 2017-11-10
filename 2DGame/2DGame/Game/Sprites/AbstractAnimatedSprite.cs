using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
			if (Texture.Width % spriteSize != 0) throw new InvalidOperationException("Invalid Sprite size");

			// Check if we already created our dictionary
			if (FrameDictionary == null) FrameDictionary = new Dictionary<Type, Texture2D[]>();

			// setting the delay
			Delay = delay;

			// If this sprite already has everything loaded, just use that. 
			if (FrameDictionary.ContainsKey(GetType()))
			{
				Texture = FrameDictionary[GetType()][0];
				return;
			}

			// ... otherwise we'll have to load it. 
			var frames = new Texture2D[Texture.Width / spriteSize];

			// Loading each frame individually
			var colorData = new Color[Texture.Height * spriteSize];

			// This just puts everything together.
			var r = new Rectangle(0, 0, spriteSize, Texture.Height);
			for (var w = 0; w < frames.Length; w++)
			{
				r.Location = new Point(w * spriteSize, 0);

				// Getting the data.
				Texture.GetData(0, r, colorData, 0, colorData.Length);
				frames[w] = new Texture2D(Game.GetInstance().GraphicsDevice, spriteSize, Texture.Height);
				frames[w].SetData(colorData);
			}

			// Add our frames to the dictionary
			FrameDictionary.Add(GetType(), frames);
			Texture = frames[0];
		}

		public AbstractAnimatedSprite(string key, Vector2 position, int spriteSize, int delay) : this(key, spriteSize, delay)
		{
			Position = position;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			var t = GetType();

			if (++CurrentOffset >= Delay)
			{
				CurrentOffset %= Delay;
				if (++CurrentFrame >= FrameDictionary[t].Length) CurrentFrame %= FrameDictionary[t].Length;


				Texture = FrameDictionary[t][CurrentFrame];
			}

			base.Draw(spriteBatch);
		}
	}
}