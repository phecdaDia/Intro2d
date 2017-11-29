using System;
using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	/// <summary>
	/// Base class for all Sprites.
	/// </summary>
	public abstract class AbstractSprite
	{
		/// <summary>
		/// <see cref="Dictionary{Type, Texture2D}"/> of static <see cref="Texture2D"/>
		/// </summary>
		private static Dictionary<Type, Texture2D> TextureDictionary;

		/// <summary>
		/// <see cref="true"/> when the <see cref="AbstractSprite"/> is marked for deletion
		/// </summary>
		private bool Deleted;

		/// <summary>
		/// <see cref="true"/> when the <see cref="AbstractSprite"/> is marked as enemy
		/// </summary>
		public bool Enemy;

		/// <summary>
		/// Amount of <see cref="Health"/> a <see cref="AbstractSprite"/> has.
		/// <para/>
		/// Can never be more than <see cref="MaxHealth"/>
		/// </summary>
		public int Health;

		/// <summary>
		/// Color of the <see cref="AbstractSprite"/>
		/// </summary>
		protected Color Hue;

		/// <summary>
		/// Depth of the <see cref="AbstractSprite"/>
		/// <para/>
		/// Minimum value must be <see cref="0"/>
		/// <para />
		/// Higher values will be drawn later resulting in being in the foreground
		/// </summary>
		public int LayerDepth;

		/// <summary>
		/// Maximum amount of <see cref="Health"/>
		/// </summary>
		public int MaxHealth
		{
			get;
			protected set;
		}

		/// <summary>
		/// <see cref="true"/> when the <see cref="AbstractSprite"/> is not deleted when exiting the <see cref="Scene"/>
		/// </summary>
		public bool Persistence;

		/// <summary>
		/// Current <see cref="Position"/> of the <see cref="AbstractSprite"/>
		/// </summary>
		protected Vector2 Position;

		/// <summary>
		/// Current <see cref="Texture2D"/> rotation
		/// </summary>
		protected float Rotation = 0f;

		/// <summary>
		/// Current <see cref="Texture2D"/> scale
		/// </summary>
		public Vector2 Scale = new Vector2(1);

		public GameTime LifeTime;

		protected AbstractSprite()
		{
			// Check if the dictionary already exists.
			if (TextureDictionary == null) TextureDictionary = new Dictionary<Type, Texture2D>();

			Hue = Color.White;

			this.Health = -1;

			this.LifeTime = new GameTime();
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

		/// <summary>
		/// Current <see cref="Texture2D"/> of the <see cref="AbstractSprite"/>
		/// </summary>
		protected Texture2D Texture
		{
			//get => TextureDictionary.ContainsKey(GetType()) ? TextureDictionary[GetType()] : null;
			get { return TextureDictionary.ContainsKey(GetType()) ? TextureDictionary[GetType()] : null; }
			//set => TextureDictionary[GetType()] = value;
			set { TextureDictionary[GetType()] = value; }
		}

		/// <summary>
		/// Returns <see cref="Position"/>
		/// </summary>
		/// <returns></returns>
		public Vector2 GetPosition()
		{
			return Position;
		}

		/// <summary>
		/// Marks a <see cref="AbstractSprite"/> as deleted
		/// </summary>
		public void Delete()
		{
			Deleted = true;
		}

		/// <summary>
		/// Returns true if the <see cref="AbstractSprite"/> is marked for deletion by <see cref="Delete()"/>
		/// </summary>
		/// <returns></returns>
		public bool IsDeleted()
		{
			return Deleted;
		}

		/// <summary>
		/// Updates <see cref="AbstractSprite"/> logic
		/// </summary>
		/// <param name="gameTime"></param>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// Draws the <see cref="AbstractSprite"/> with <see cref="SpriteBatch"/>
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(Texture, position - (new Vector2(Texture.Width, Texture.Height) * 0.5f), Hue);
			if (Texture == null) return;


			//spriteBatch.Draw(Texture, Position - new Vector2(Texture.Width, Texture.Height) * 0.5f, Hue);


			spriteBatch.Draw(
				Texture,
				Position,
				null,
				Hue,
				Rotation,
				new Vector2(Texture.Width / 2f, Texture.Height / 2f),
				Scale,
				SpriteEffects.None,
				0f
			);
		}

		/// <summary>
		/// Dynamically creates an <see cref="AbstractSprite"/> with <paramref name="parameters"/>
		/// <para/>
		/// This method should not be used!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		[System.Obsolete("Method is deprecated, please use SpawnSprite instead.", true)]
		protected void ShootOrb<T>(params object[] parameters) where T : AbstractSprite
		{
			var o = (T) Activator.CreateInstance(typeof(T), parameters);
			SceneManager.GetCurrentScene().AddSprite(o);
		}

		/// <summary>
		/// Spawns a <see cref="AbstractSprite"/> in the <see cref="SceneManager.CurrentScene"/>
		/// </summary>
		/// <param name="sprite"></param>
		protected void SpawnSprite(AbstractSprite sprite)
		{
			SceneManager.GetCurrentScene().BufferedAddSprite(sprite);
		}

		/// <summary>
		/// Default method for collision checks
		/// <para/>
		/// This method should be overwritten by more complex <see cref="AbstractSprite"/>
		/// </summary>
		/// <param name="orb"></param>
		/// <returns></returns>
		public virtual bool DoesCollide(AbstractOrb orb)
		{
			return (orb.GetPosition() - Position).Length() < (Texture.Width / 2f + orb.Texture.Width);
		}
	}
}