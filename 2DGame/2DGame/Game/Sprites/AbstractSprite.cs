using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework.Content;

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
		private bool Deleted;

        // This is for coloring monochrome sprites
		protected Color Hue;
        // position of our Sprite on the screen. Since we won't move the "camera" we can use this to draw
        protected Vector2 Position;

        // Decides the draw order. Higher layerDepth will draw later.
        private int LayerDepth = 0;

	    private int MaxHealth = 0;
	    private int Health = -1;

        public bool Persistence = false;
	    private bool Enemy;


	    protected AbstractSprite()
		{
			// Check if the dictionary already exists.
			if (TextureDictionary == null) TextureDictionary = new Dictionary<Type, Texture2D>();

			this.Hue = Color.White;
		}

        public AbstractSprite(String textureKey, Vector2 position) : this()
        {
            // Setting important things
            this.Texture = ImageManager.GetTexture2D(textureKey);
            this.Position = position;
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
		public bool IsDeleted()
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
	        if (Texture == null) return;

            spriteBatch.Draw(Texture, Position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
		}

		// Defines if the sprite is an enemy. 
	    protected void SetEnemy(bool isEnemy)
	    {
		    this.Persistence = isEnemy;
		    this.Enemy = isEnemy;
	    }

		// returns if the sprite is an enemy. 
	    public bool IsEnemy()
	    {
		    return Enemy;
		}

	    protected void SetMaxHealth(int maxHealth)
	    {
		    this.MaxHealth = maxHealth;
		}

	    protected void SetHealth(int health)
	    {
		    this.Health = health;
	    }

	    public int Damage(int amount)
	    {
		    if (this.MaxHealth <= 0) return -1;

		    this.Health -= amount;

		    return this.Health;
	    }

		protected void ShootOrb<T>(params object[] parameters) where T : AbstractSprite
		{
			T o = (T)Activator.CreateInstance(typeof(T), parameters);
			SceneManager.GetCurrentScene().AddSprite(o);

		}

	    public virtual bool DoesCollide(AbstractOrb orb)
	    {
		    return (orb.GetPosition() - this.Position).Length() < (this.Texture.Width / 2f);
	    }
    }
}
