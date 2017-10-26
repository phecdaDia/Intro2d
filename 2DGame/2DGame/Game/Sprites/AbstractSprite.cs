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
        private Texture2D texture;
        protected Vector2 position;

        public AbstractSprite(String textureKey, Vector2 Position)
        {
            this.texture = ImageManager.GetInstance().GetTexture2D(textureKey);
            this.position = Position;
        }



        public abstract void Update(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - (new Vector2(texture.Width, texture.Height) * 0.5f), Color.White);
        }
    }
}
