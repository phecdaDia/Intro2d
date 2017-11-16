using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public abstract class AbstractAnimatedSprite : AbstractSprite
	{
		// Dictionary to improve performance, since we'll use many objects at once
		private static Dictionary<Type, Dictionary<string, Point[]>> FrameDictionary;

		// delay until next frame
		private readonly int Delay;

		// current frame and the offset until the next frame.
		private int CurrentFrame, CurrentOffset;

        private string CurrentAnimation = @"default";

        private Point Size;

		public AbstractAnimatedSprite(string key, Point size, int delay) : base(key)
		{
			// Check if we already created our dictionary
			if (FrameDictionary == null) FrameDictionary = new Dictionary<Type, Dictionary<string, Point[]>>();

			// setting the delay
			Delay = delay;

            this.Size = size;

            if (FrameDictionary.ContainsKey(this.GetType())) return;

            FrameDictionary[this.GetType()] = new Dictionary<string, Point[]>();

            AddFrames();
		}

        protected abstract void AddFrames();

        protected void AddAnimation(string key, Point[] points)
        {
            FrameDictionary[this.GetType()][key] = points;
        }

        protected void AddAnimation(Point[] points)
        {
            AddAnimation(@"default", points);
        }

        protected void SetAnimation(string animation)
        {
            this.CurrentAnimation = animation;
            this.CurrentFrame = 0;
            this.CurrentOffset = 0;
        }

		public AbstractAnimatedSprite(string key, Vector2 position, Point size, int delay) : this(key, size, delay)
		{
			Position = position;
		}

        public override void Update(GameTime gameTime)
        {
            var t = GetType();

            this.CurrentOffset += gameTime.ElapsedGameTime.Milliseconds;

            while (CurrentOffset >= Delay)
            {
                CurrentOffset -= Delay;
                CurrentFrame++;

                if (CurrentFrame >= FrameDictionary[t][CurrentAnimation].Length) CurrentFrame %= FrameDictionary[t][CurrentAnimation].Length;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
		{
            Rectangle rTexture = new Rectangle(FrameDictionary[this.GetType()][CurrentAnimation][CurrentFrame], this.Size);
            Rectangle rScene = new Rectangle(this.Position.ToPoint() - (this.Size.ToVector2() / 2f).ToPoint(), this.Size);
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