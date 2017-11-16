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

				ps.Damage(GameConstants.PLAYER_DAMAGE);
				this.Delete();
			}
		}

        protected abstract Vector2 UpdatePosition(GameTime gameTime);
    }
}