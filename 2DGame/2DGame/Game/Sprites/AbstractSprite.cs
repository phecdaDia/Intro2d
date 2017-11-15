using System;
using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public abstract class AbstractSprite
	{
		// Our textures
		private static Dictionary<Type, Texture2D> TextureDictionary;

		// If the sprite is marked for deleting.
		private bool Deleted;

		public bool Enemy;
		public int Health;

	    // This is for coloring monochrome sprites
	    protected Color Hue;

		// Decides the draw order. Higher layerDepth will draw later.
		protected int LayerDepth;

		public int MaxHealth
		{
			get;
			protected set;
		}

		public bool Persistence;

		// position of our Sprite on the screen. Since we won't move the "camera" we can use this to draw
		protected Vector2 Position;

		protected float Rotation = 0f;
		public float Scale = 1.0f;

		protected AbstractSprite()
		{
			// Check if the dictionary already exists.
			if (TextureDictionary == null) TextureDictionary = new Dictionary<Type, Texture2D>();

			Hue = Color.White;

			this.Health = -1;
		}

		public AbstractSprite(string textureKey, Vector2 position) : this()
		{
			// Setting important things
			Texture = ImageManager.GetTexture2D(textureKey);
			Position = position;
		}

		public AbstractSprite(string textureKey) : this()
		{
			Texture = ImageManager.GetTexture2D(textureKey);
		}

		protected Texture2D Texture
		{
            //get => TextureDictionary.ContainsKey(GetType()) ? TextureDictionary[GetType()] : null;
            get { return TextureDictionary.ContainsKey(GetType()) ? TextureDictionary[GetType()] : null; }
            //set => TextureDictionary[GetType()] = value;
            set { TextureDictionary[GetType()] = value; }
        }

		public Vector2 GetPosition()
		{
			return Position;
		}

		// Marks this sprite as deleted
		public void Delete()
		{
			Deleted = true;
		}

		// Returns if the sprite is marked as deleted
		public bool IsDeleted()
		{
			return Deleted;
		}

		// Sets the current Layer Depth
		protected void SetLayerDepth(int layerDepth)
		{
			LayerDepth = layerDepth;
		}

		// Returns the current Layer Depth of the sprite
		public int GetLayerDepth()
		{
			return LayerDepth;
		}

		// Updates the Sprite Logic
		public abstract void Update(GameTime gameTime);

		// Draws a Sprite. 
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(Texture, position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
			if (Texture == null) return;


			spriteBatch.Draw(Texture, Position - new Vector2(Texture.Width, Texture.Height) * 0.5f, Hue);


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

        // Method should not be used
        [System.Obsolete("Method is deprecated, please use SpawnSprite instead.", true)]
        protected void ShootOrb<T>(params object[] parameters) where T : AbstractSprite
		{
			var o = (T) Activator.CreateInstance(typeof(T), parameters);
			SceneManager.GetCurrentScene().AddSprite(o);
		}

        protected void SpawnSprite(AbstractSprite sprite)
        {
            SceneManager.GetCurrentScene().AddSprite(sprite);
        }

		public virtual bool DoesCollide(AbstractOrb orb)
		{
			return (orb.GetPosition() - Position).Length() < Texture.Width / 2f;
		}
	}
}