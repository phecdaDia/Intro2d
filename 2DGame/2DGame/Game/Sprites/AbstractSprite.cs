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
        // Our textures
        private static Dictionary<Type, Texture2D> textureDictionary;

		protected Texture2D Texture
		{
			get { return textureDictionary.ContainsKey(this.GetType()) ? textureDictionary[this.GetType()] : null; }
			set { textureDictionary[this.GetType()] = value; }
		}

		private Boolean deleted;

		protected Color Hue;
        // Position of our Sprite on the screen. Since we won't move the "camera" we can use this to draw
        protected Vector2 position;

		protected AbstractSprite()
		{
			// Check if the dictionary already exists.
			if (textureDictionary == null) textureDictionary = new Dictionary<Type, Texture2D>();

			this.Hue = Color.White;
		}

        public AbstractSprite(String textureKey, Vector2 Position) : this()
        {
            // Setting important things
            this.Texture = ImageManager.GetTexture2D(textureKey);
            this.position = Position;
        }

        public AbstractSprite(String textureKey) : this()
		{
			this.Texture = ImageManager.GetTexture2D(textureKey);
		}

		public Vector2 GetPosition()
		{
			return this.position;
		}

		public void Delete()
		{
			this.deleted = true;
		}

		public Boolean IsDeleted()
		{
			return this.deleted;
		}

        // Updates the Sprite Logic
        public abstract void Update(GameTime gameTime);

        // Draws a Sprite. 
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
        }
    }
}
