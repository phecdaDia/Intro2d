using System;
using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
    public abstract class AbstractOrb : AbstractSprite
    {
	    protected Vector2 Direction;

        public AbstractOrb(string textureKey, Vector2 position, Vector2 direction) : base(textureKey, position)
		{
			this.Direction = direction;
			if (this.Direction.LengthSquared() > 0) this.Direction.Normalize();
			this.Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 up = UpdatePosition(gameTime);
	        this.Rotation = (float)Math.Atan2(up.Y, up.X);

	        this.Position += up;

			List<PlayerSprite> players = SceneManager.GetSprites<PlayerSprite>();
            foreach (PlayerSprite ps in players)
            {
                if (ps.DoesCollide(this)) this.Hue = Color.Red;
            }
        }

        protected abstract Vector2 UpdatePosition(GameTime gameTime);


	    public override void Draw(SpriteBatch spriteBatch)
	    {
		    spriteBatch.Draw(
			    this.Texture,
				Position,
				null,
			    this.Hue,
			    this.Rotation,
			    new Vector2(this.Texture.Width / 2f, this.Texture.Height / 2f),
			    new Vector2(this.Scale),
			    SpriteEffects.None,
			    0f
		    );
	    }
    }
}
