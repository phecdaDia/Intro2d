using System;
using System.Collections;
using System.Collections.Generic;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
	/*
		This is the basic level logic.
		This allows us to change scenes and update/draw sprites.
	*/
	public abstract class Scene
	{
		// Dictionary of all sprites. 
		// This has to be a Dict<TypeA, Dict<TypeB, IList>>
		// TypeA is the scene type
		// TypeB is the sprite type
		// The list contains all sprites of TypeB
		private static Dictionary<string, Dictionary<Type, IList>> SpriteDictionary;

		// We have to do it this way, otherwise Sprites can't spawn other sprites. 
		//private static Dictionary<Type, Dictionary<Type, IList>> BufferedSpriteDictionary;
		private static Dictionary<string, List<AbstractSprite>> BufferedSpriteDictionary;


		public Scene(string key)
		{
			// Setting our sceneKey
			SceneKey = key;

			// Checks if the Dictionaries already exist.
			if (SpriteDictionary == null) SpriteDictionary = new Dictionary<string, Dictionary<Type, IList>>();
			if (BufferedSpriteDictionary == null) BufferedSpriteDictionary = new Dictionary<string, List<AbstractSprite>>();

			// Checks if the scene is in the Dictionary. 
			if (!SpriteDictionary.ContainsKey(this.SceneKey)) SpriteDictionary[this.SceneKey] = new Dictionary<Type, IList>();
			//if (!BufferedSpriteDictionary.ContainsKey(this.GetType())) BufferedSpriteDictionary[this.GetType()] = new List<AbstractSprite>();
		}

		public string SceneKey { get; }

		// Tries to get all Sprites of T
		// GetSprite<PlayerSprite>() would result in all players.
		public List<T> GetSprites<T>()
		{
			// If we have a List of sprites return it, otherwise give a blank list to avoid NPE
			if (SpriteDictionary[this.SceneKey].ContainsKey(typeof(T))) return (List<T>) SpriteDictionary[this.SceneKey][typeof(T)];
			return new List<T>();
		}

		public void AddSprite(AbstractSprite s)
		{
			if (!BufferedSpriteDictionary.ContainsKey(this.SceneKey))
				BufferedSpriteDictionary[this.SceneKey] = new List<AbstractSprite>();
			BufferedSpriteDictionary[this.SceneKey].Add(s);
		}

		// This returns all Sprites of the current scene
		public Dictionary<Type, IList> GetAllSprites()
		{
			return SpriteDictionary[this.SceneKey];
		}

		// This returns all Sprites of a scene
		public Dictionary<Type, IList> GetAllSprites(String sceneKey)
		{
			return SpriteDictionary[sceneKey];
		}

		// This is used to spawn all objects
		protected abstract void CreateScene();

		public void ResetScene()
		{
			SpriteDictionary[this.SceneKey].Clear();
			CreateScene();
		}

		// Updates all registered Sprites
		public virtual void Update(GameTime gameTime)
		{
			//if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;

			var deleted = new List<AbstractSprite>();
			foreach (var l in SpriteDictionary[this.SceneKey].Values)
			{
				foreach (AbstractSprite c in l)
				{
					c.Update(gameTime);

					if (!c.Persistence)
						if (c.GetPosition().X < 0 || c.GetPosition().Y < 0) c.Delete();
						else if (c.GetPosition().X > Game.GraphicsArea.X || c.GetPosition().Y > Game.GraphicsArea.Y) c.Delete();


					if (c.Health >= c.MaxHealth) c.Health = c.MaxHealth;
					else if (c.Health < 0 && c.Enemy)
					{
						c.Delete();
					}

					if (c.IsDeleted()) deleted.Add(c);
				}
			}

			foreach (var c in deleted)
				SpriteDictionary[this.SceneKey][c.GetType()].Remove(c);

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

		// Draws all registered Sprites
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			//if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;

			var priorityDictionary = new SortedDictionary<int, List<AbstractSprite>>();

			foreach (var l in SpriteDictionary[this.SceneKey].Values)
			foreach (AbstractSprite c in l)
				if (c.GetLayerDepth() > 0)
				{
					if (!priorityDictionary.ContainsKey(c.GetLayerDepth()))
						priorityDictionary[c.GetLayerDepth()] = new List<AbstractSprite>();

					priorityDictionary[c.GetLayerDepth()].Add(c);
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
	}
}