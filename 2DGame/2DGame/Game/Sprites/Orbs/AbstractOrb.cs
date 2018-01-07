using System;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Orbs
{
	/// <summary>
	/// Base class for all orbs
	/// <para />
	/// All Orbs must inherit this class
	/// </summary>
	public abstract class AbstractOrb : AbstractAnimatedSprite
	{
		/// <summary>
		/// Direction of the orb.
		/// <para />
		/// Used for <see cref="AbstractSprite.Rotation"/>
		/// </summary>
		protected Vector2 Direction;

		protected AbstractOrb(string textureKey, Vector2 position, Vector2 direction) : base(textureKey, position)
		{
			// default values
			Direction = direction;
			if (Direction.LengthSquared() > 0) Direction.Normalize();
			Rotation = (float)Math.Atan2(direction.Y, direction.X);
		}

		protected AbstractOrb(string textureKey, Vector2 position, Vector2 direction, float delay, Point size) : base(textureKey, position, size, delay)
		{
			// default values
			Direction = direction;
			if (Direction.LengthSquared() > 0) Direction.Normalize();
			Rotation = (float)Math.Atan2(direction.Y, direction.X);
		}

		protected AbstractOrb(string textureKey, Vector2 position, Vector2 direction, Point size) : base(textureKey, position, size)
		{
			// default values
			Direction = direction;
			if (Direction.LengthSquared() > 0) Direction.Normalize();
			Rotation = (float)Math.Atan2(direction.Y, direction.X);
		}

		protected override void AddFrames()
		{
			AddAnimation(new Point[] { new Point() });
		}

		/// <summary>
		/// Updates <see cref="AbstractSprite.Rotation"/>, calls <see cref="UpdatePosition(GameTime)"/> and damages <see cref="PlayerSprite"/>
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var up = UpdatePosition(gameTime);
			if (up.LengthSquared() > 0) Rotation = (float) Math.Atan2(up.Y, up.X);

			Position += up * 60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}

		/// <summary>
		/// Updates <see cref="AbstractSprite.Position"/> and other logic.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns><see cref="Vector2"/> with movement</returns>
		protected abstract Vector2 UpdatePosition(GameTime gameTime);
	}
}