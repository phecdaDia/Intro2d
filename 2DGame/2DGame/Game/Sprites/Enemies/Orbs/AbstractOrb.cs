using System;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public abstract class AbstractOrb : AbstractSprite
	{
		protected Vector2 Direction;

		protected AbstractOrb(string textureKey, Vector2 position, Vector2 direction) : base(textureKey, position)
		{
			Direction = direction;
			if (Direction.LengthSquared() > 0) Direction.Normalize();
			Rotation = (float) Math.Atan2(direction.Y, direction.X);
		}

		public override void Update(GameTime gameTime)
		{
			var up = UpdatePosition(gameTime);
			Rotation = (float) Math.Atan2(up.Y, up.X);

			Position += up;

			var players = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in players)
			{
				if (!ps.DoesCollide(this)) continue;

				ps.Damage(250);
				this.Delete();
			}
		}

		protected abstract Vector2 UpdatePosition(GameTime gameTime);


		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				Texture,
				Position,
				null,
				Hue,
				Rotation,
				new Vector2(Texture.Width / 2f, Texture.Height / 2f),
				new Vector2(Scale),
				SpriteEffects.None,
				0f
			);
		}
	}
}