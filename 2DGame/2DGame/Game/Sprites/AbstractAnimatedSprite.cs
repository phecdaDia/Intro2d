using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	/// <summary>
	/// <see cref="AbstractSprite"/> with multiple frames
	/// </summary>
	public abstract class AbstractAnimatedSprite : AbstractSprite
	{
		/// <summary>
		/// Dictionary that stores <see cref="Dictionary{String, Point[]}"/> of all frames for every type
		/// <para/>
		/// Dictionary is used to increase performance as retrieving a value is linear complexity.
		/// </summary>
		private static Dictionary<Type, Dictionary<string, Point[]>> FrameDictionary;

		/// <summary>
		/// Delay for every frame in Milliseconds
		/// </summary>
		private readonly double Delay;

		private double CurrentOffset;

		/// <summary>
		/// Current Frame to be displayed.
		/// </summary>
		protected int CurrentFrame;

		/// <summary>
		/// Default animation title.
		/// </summary>
		private string CurrentAnimation = @"default";

		/// <summary>
		/// Size of a single frame
		/// </summary>
		private readonly Point Size;

		public AbstractAnimatedSprite(string key, Vector2 position, Point size, float delay) : this(key, position, size)
		{
			// setting the delay
			Delay = delay;
		}

		public AbstractAnimatedSprite(string key, Vector2 position, Point size) : base(key, position)
		{
			// Check if we already created our dictionary
			if (FrameDictionary == null) FrameDictionary = new Dictionary<Type, Dictionary<string, Point[]>>();

			this.Size = size;


			if (FrameDictionary.ContainsKey(this.GetType())) return;
			FrameDictionary[this.GetType()] = new Dictionary<string, Point[]>();

			AddFrames();
		}

		public AbstractAnimatedSprite(string key, Vector2 position): base(key, position)
		{

			// Check if we already created our dictionary
			if (FrameDictionary == null) FrameDictionary = new Dictionary<Type, Dictionary<string, Point[]>>();

			this.Size = this.Texture.Bounds.Size;
			this.Delay = Int32.MaxValue;


			if (FrameDictionary.ContainsKey(this.GetType())) return;
			FrameDictionary[this.GetType()] = new Dictionary<string, Point[]>();

			AddAnimation(new Point[] {new Point()});
		}

		/// <summary>
		/// Call <see cref="AddAnimation(Point[], string)"/> to register an animation in this function.
		/// <para />
		/// 
		/// </summary>
		protected abstract void AddFrames();

		/// <summary>
		/// Adds an animation to the <see cref="FrameDictionary"/>
		/// </summary>
		/// <param name="points"><see cref="List{Point}"/> for the animation</param>
		/// <param name="key">Name of the animation</param>
		protected void AddAnimation(Point[] points, string key = @"default")
		{
			FrameDictionary[this.GetType()][key] = points;
		}

		/// <summary>
		/// Sets the current <paramref name="animation"/> and resets the animation
		/// </summary>
		/// <param name="animation">New animation name</param>
		protected void SetAnimation(string animation)
		{
			this.CurrentAnimation = animation;
			this.CurrentFrame = 0;
			this.CurrentOffset = 0;
		}

		/// <summary>
		/// Updates the animation
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			var t = GetType();

			this.CurrentOffset += gameTime.ElapsedGameTime.TotalSeconds;

			while (CurrentOffset >= Delay)
			{
				CurrentOffset -= Delay;
				CurrentFrame++;

				if (CurrentFrame >= FrameDictionary[t][CurrentAnimation].Length) CurrentFrame %= FrameDictionary[t][CurrentAnimation].Length;
			}
		}

		/// <summary>
		/// Draws the current frame with respect the <see cref="AbstractSprite.Scale"/> and <see cref="AbstractSprite.Rotation"/>
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			Rectangle rTexture = new Rectangle(FrameDictionary[this.GetType()][CurrentAnimation][CurrentFrame], this.Size);
			//Rectangle rScene = new Rectangle(this.Position.ToPoint() - (this.Size.ToVector2() / 2f).ToPoint(), this.Size);
			//spriteBatch.Draw(this.Texture, rScene, rTexture, this.Hue);

			spriteBatch.Draw(
				this.Texture,
				this.Position,
				rTexture,
				this.Hue,
				this.Rotation,
				Size.ToVector2() / 2f,
				this.Scale,
				SpriteEffects.None,
				0f
			);
		}
	}
}