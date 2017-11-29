using System;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
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
		public Vector2 Direction;

		protected AbstractOrb(string textureKey, Vector2 position, Vector2 direction) : base(textureKey, position)
		{
			// default values
			Direction = direction;
			//if (Direction.LengthSquared() > 0) Direction.Normalize(); // TODO
			Rotation = (float)Math.Atan2(direction.Y, direction.X);
		}

		protected AbstractOrb(string textureKey, Vector2 position, Vector2 direction, int delay, Point size) : base(textureKey, position, size, delay)
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
			var up = UpdatePosition(gameTime);
			Rotation = (float) Math.Atan2(up.Y, up.X);

			Position += up;

			var players = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in players)
			{
				if (!ps.DoesCollide(this)) continue;

				ps.Damage(GameConstants.PLAYER_DAMAGE);
				this.Delete();
			}
		}

		/// <summary>
		/// Updates <see cref="AbstractSprite.Position"/> and other logic.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns><see cref="Vector2"/> with movement</returns>
		protected abstract Vector2 UpdatePosition(GameTime gameTime);
	}
}