using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
	/// <summary>
	/// This is the basic level logic.
	/// This allows us to change scenes and update/draw sprites.
	/// </summary>
	public abstract class Scene
	{
		/// <summary> 
		/// Dictionary of all sprites. 
		/// This has to be a Dict<TypeA, Dict<TypeB, IList>>
		/// TypeA is the scene type
		/// TypeB is the sprite type
		/// The list contains all sprites of TypeB
		/// </summary>
		private static Dictionary<string, Dictionary<Type, IList>> SpriteDictionary;

		/// <summary>
		/// We have to do it this way, otherwise Sprites can't spawn other sprites. 
		/// </summary>
		private static Dictionary<string, List<AbstractSprite>> BufferedSpriteDictionary;

		/// <summary>
		/// Every scene should have a unique scenekey for identification if it's registered
		/// This allows us to call the scene by the key.
		/// </summary>
		public string SceneKey { get; }

		public readonly GameTime LifeTime;

		protected Scene(string key)
		{
			// Setting our sceneKey
			SceneKey = key;

			// Checks if the Dictionaries already exist.
			if (SpriteDictionary == null) SpriteDictionary = new Dictionary<string, Dictionary<Type, IList>>();
			if (BufferedSpriteDictionary == null) BufferedSpriteDictionary = new Dictionary<string, List<AbstractSprite>>();

			// Checks if the scene is in the Dictionary. 
			if (!SpriteDictionary.ContainsKey(this.SceneKey)) SpriteDictionary[this.SceneKey] = new Dictionary<Type, IList>();
			//if (!BufferedSpriteDictionary.ContainsKey(this.GetType())) BufferedSpriteDictionary[this.GetType()] = new List<AbstractSprite>();
			
			this.LifeTime = new GameTime();
		}

		/// <summary>
		/// Tries to get all Sprites of T
		/// GetSprite<PlayerSprite>() would result in all players.
		/// </summary>
		public List<T> GetSprites<T>()
		{
			// If we have a List of sprites return it, otherwise give a blank list to avoid NPE
			if (SpriteDictionary[this.SceneKey].ContainsKey(typeof(T))) return (List<T>) SpriteDictionary[this.SceneKey][typeof(T)];
			return new List<T>();
		}

		/// <summary>
		/// Adds a sprite to the current top layer scene in the next frame
		/// </summary>
		/// <param name="s">Sprite that's being spawned</param>
		public void BufferedAddSprite(AbstractSprite s)
		{
			if (!BufferedSpriteDictionary.ContainsKey(this.SceneKey))
				BufferedSpriteDictionary[this.SceneKey] = new List<AbstractSprite>();
			BufferedSpriteDictionary[this.SceneKey].Add(s);

		}

		/// <summary>
		/// Adds a sprite to the current top layer scene
		/// </summary>
		/// <param name="s">Sprite that's being spawned</param>
		public void AddSprite(AbstractSprite s)
		{
			if (!SpriteDictionary[this.SceneKey].ContainsKey(s.GetType()))
			{

				var listType = typeof(List<>).MakeGenericType(s.GetType());
				var list = (IList)Activator.CreateInstance(listType);
				SpriteDictionary[this.SceneKey][s.GetType()] = list;
			}

			SpriteDictionary[this.SceneKey][s.GetType()].Add(s);
		}

		/// <summary>
		/// This returns all Sprites of the current scene
		/// </summary> 
		public Dictionary<Type, IList> GetAllSprites()
		{
			return SpriteDictionary[this.SceneKey];
		}

		/// <summary>
		/// This returns all Sprites of the current scene by the scenekey
		/// </summary> 
		/// <param name="sceneKey">Key of the scene</param>
		public Dictionary<Type, IList> GetAllSprites(String sceneKey)
		{
			return SpriteDictionary[sceneKey];
		}

		/// <summary>
		/// Initially creates all sprites a scene uses
		/// 
		/// Sprites should be added by the <see cref="AddSprite(AbstractSprite)"/> function.
		/// </summary>
		protected abstract void CreateScene();

		/// <summary>
		/// Removed all current sprites and calls <see cref="CreateScene"/>
		/// </summary>
		public void ResetScene()
		{
			SpriteDictionary[this.SceneKey].Clear();
			CreateScene();
		}

		/// <summary>
		/// Updates all sprites
		/// </summary>
		/// <param name="gameTime">GameTime</param>
		public virtual void Update(GameTime gameTime)
		{
			if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;

			this.LifeTime.ElapsedGameTime += gameTime.ElapsedGameTime;

			var deleted = new List<AbstractSprite>();
			foreach (var l in SpriteDictionary[this.SceneKey].Values)
			{
				foreach (AbstractSprite c in l)
				{
					c.LifeTime.ElapsedGameTime = gameTime.ElapsedGameTime;
					c.LifeTime.TotalGameTime += gameTime.ElapsedGameTime;
					c.Update(gameTime);

					if (!c.Persistence)
						if (c.Position.X < 0 || c.Position.Y < 0) c.Delete();
						else if (c.Position.X > Game.RenderSize.X || c.Position.Y > Game.RenderSize.Y) c.Delete();


					if (c.Health >= c.MaxHealth) c.Health = c.MaxHealth;
					else if (c.Health < 0 && c.Enemy)
					{
						c.Delete();
					}

					if (c.IsDeleted()) deleted.Add(c);
				}
			}

			foreach (var c in deleted)
			{
				c.UnloadContent();

				SpriteDictionary[this.SceneKey][c.GetType()].Remove(c);
			}

			// Adding spawned Sprites to the SpriteDictionary

			// foreach scene go through each sprite type and add them to the SpriteDictionary

			foreach (var t in BufferedSpriteDictionary.Keys)
			{
				foreach (var c in BufferedSpriteDictionary[t])
				{
					if (!SpriteDictionary[t].ContainsKey(c.GetType()))
					{
						var listType = typeof(List<>).MakeGenericType(c.GetType());
						var list = (IList) Activator.CreateInstance(listType);
						SpriteDictionary[t][c.GetType()] = list;
					}

					//Console.WriteLine("Adding Sprite of Type {0}", c.GetType());
					SpriteDictionary[t][c.GetType()].Add(c);
				}

				BufferedSpriteDictionary[t].Clear();
			}
		}

		/// <summary>
		/// Draws the scene
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			//if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;

			var priorityDictionary = new SortedDictionary<int, List<AbstractSprite>>();

			foreach (var l in SpriteDictionary[this.SceneKey].Values)
			foreach (AbstractSprite c in l)
				if (c.LayerDepth > 0)
				{
					if (!priorityDictionary.ContainsKey(c.LayerDepth))
						priorityDictionary[c.LayerDepth] = new List<AbstractSprite>();

					priorityDictionary[c.LayerDepth].Add(c);
				}
				else
				{
					c.Draw(spriteBatch);
				}

			foreach (var v in priorityDictionary.Keys)
			{
				//Console.WriteLine("Drawing Layer #{0}", v);
				foreach (var a in priorityDictionary[v])
					//Console.WriteLine("\t{0}", a.GetType());
					a.Draw(spriteBatch);

				foreach (var a in priorityDictionary[v])
					a.Draw(spriteBatch);
			}
		}

		/// <summary>
		/// Loads all custom content
		/// </summary>
		public virtual void LoadContent()
		{
			foreach (var spriteDict in GetAllSprites())
			{
				foreach (var sprite in spriteDict.Value)
				{
					((AbstractSprite) sprite).LoadContent();
				}
			}
		}

		/// <summary>
		/// Should unload all custom content
		/// </summary>
		public virtual void UnloadContent()
		{
			foreach (var spriteDict in GetAllSprites())
			{
				foreach (var sprite in spriteDict.Value)
				{
					((AbstractSprite)sprite).UnloadContent();
				}
			}
		}

	}
}