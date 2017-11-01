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
        private static Dictionary<Type, Texture2D> TextureDictionary;
		protected Texture2D Texture
		{
			get { return TextureDictionary.ContainsKey(this.GetType()) ? TextureDictionary[this.GetType()] : null; }
			set { TextureDictionary[this.GetType()] = value; }
		}

		private Boolean Deleted;

		protected Color Hue;
        // Position of our Sprite on the screen. Since we won't move the "camera" we can use this to draw
        protected Vector2 Position;

        private float LayerDepth = 1;

		protected AbstractSprite()
		{
			// Check if the dictionary already exists.
			if (TextureDictionary == null) TextureDictionary = new Dictionary<Type, Texture2D>();

			this.Hue = Color.White;
		}

        public AbstractSprite(String textureKey, Vector2 Position) : this()
        {
            // Setting important things
            this.Texture = ImageManager.GetTexture2D(textureKey);
            this.Position = Position;
        }

        public AbstractSprite(String textureKey) : this()
		{
			this.Texture = ImageManager.GetTexture2D(textureKey);
		}

		public Vector2 GetPosition()
		{
			return this.Position;
		}

		public void Delete()
		{
			this.Deleted = true;
		}

		public Boolean IsDeleted()
		{
			return this.Deleted;
		}

        protected void SetLayerDepth(float layerDepth)
        {
            this.LayerDepth = layerDepth;
        }

        protected float GetLayerDepth()
        {
            return this.LayerDepth;
        }

        // Updates the Sprite Logic
        public abstract void Update(GameTime gameTime);

        // Draws a Sprite. 
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
            spriteBatch.Draw(Texture, Position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), null, Hue, 0, new Vector2(), 1f, SpriteEffects.None, this.LayerDepth);
        }
    }
}
