using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Sprites
{
    public abstract class AbstractSprite
    {
        // Our texture. Might be replaced by a String. We would then just use the ImageManager
        private Texture2D texture;
        // Position of our Sprite on the screen. Since we won't move the "camera" we can use this to draw
        protected Vector2 position;

        public AbstractSprite(String textureKey, Vector2 Position)
        {
            // Setting important things
            this.texture = ImageManager.GetTexture2D(textureKey);
            this.position = Position;
        }

        public AbstractSprite(Vector2 Position)
        {
            this.position = Position;
        }

        // Updates the Sprite Logic
        public abstract void Update(GameTime gameTime);

        // Draws a Sprite. 
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - (new Vector2(texture.Width, texture.Height) * 0.5f), Color.White);
        }
    }
}
