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

        // If the sprite is marked for deleting.
		private Boolean Deleted;

        // This is for coloring monochrome sprites
		protected Color Hue;
        // Position of our Sprite on the screen. Since we won't move the "camera" we can use this to draw
        protected Vector2 Position;

        // Decides the draw order. Higher LayerDepth will draw later.
        private int LayerDepth = 0;

        public Boolean Persistence = false;


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

        // Marks this sprite as deleted
		public void Delete()
		{
			this.Deleted = true;
		}

        // Returns if the sprite is marked as deleted
		public Boolean IsDeleted()
		{
			return this.Deleted;
		}

        // Sets the current Layer Depth
        protected void SetLayerDepth(int layerDepth)
        {
            this.LayerDepth = layerDepth;
        }
        
        // Returns the current Layer Depth of the sprite
        public int GetLayerDepth()
        {
            return this.LayerDepth;
        }

        // Updates the Sprite Logic
        public abstract void Update(GameTime gameTime);

        // Draws a Sprite. 
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
            spriteBatch.Draw(Texture, Position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
        }
    }
}
