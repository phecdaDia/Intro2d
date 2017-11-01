﻿using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes
{
	/*
		This is the basic level logic.
		This allows us to change scenes and update/draw sprites.
	*/
    public abstract class Scene
    {
        public String SceneKey
		{
			get;
			private set;
		}

		// Dictionary of all sprites. 
		// This has to be a Dict<TypeA, Dict<TypeB, IList>>
		// TypeA is the scene type
		// TypeB is the sprite type
		// The list contains all sprites of TypeB
		private static Dictionary<Type, Dictionary<Type, IList>> SpriteDictionary;

		// We have to do it this way, otherwise Sprites can't spawn other sprites. 
		//private static Dictionary<Type, Dictionary<Type, IList>> BufferedSpriteDictionary;
        private static Dictionary<Type, List<AbstractSprite>> BufferedSpriteDictionary;
		

        public Scene(String key)
        {
            // Setting our sceneKey
            this.SceneKey = key;

			// Checks if the Dictionaries already exist.
			if (SpriteDictionary == null) SpriteDictionary = new Dictionary<Type, Dictionary<Type, IList>>();
			if (BufferedSpriteDictionary == null) BufferedSpriteDictionary = new Dictionary<Type, List<AbstractSprite>>();

			// Checks if the scene is in the Dictionary. 
			if (!SpriteDictionary.ContainsKey(this.GetType())) SpriteDictionary[this.GetType()] = new Dictionary<Type, IList>();
            //if (!BufferedSpriteDictionary.ContainsKey(this.GetType())) BufferedSpriteDictionary[this.GetType()] = new List<AbstractSprite>();

			// Registering the scene in the SceneManager
			SceneManager.RegisterScene(key, this);

			// Initially creates the scene and initializes all sprites
            CreateScene();
        }

		// Tries to get all Sprites of T
		// GetSprite<PlayerSprite>() would result in all players.
		public List<T> GetSprites<T>()
		{
			// If we have a List of sprites return it, otherwise give a blank list to avoid NPE
			if (SpriteDictionary[this.GetType()].ContainsKey(typeof(T))) return (List<T>) SpriteDictionary[this.GetType()][typeof(T)];
			return new List<T>();
		}

		public void AddSprite(AbstractSprite s)
		{
			if (!BufferedSpriteDictionary.ContainsKey(this.GetType())) {

                BufferedSpriteDictionary[this.GetType()] = new List<AbstractSprite>();
                
            }
            BufferedSpriteDictionary[this.GetType()].Add(s);


			//if (!BufferedSpriteDictionary[this.GetType()].ContainsKey(type))
			//{
			//	//List<dynamic> l = new List<dynamic>();
			//	//l.Add(s);
			//	//this.SpriteDictionary[s.GetType()] = l;

			//	Type listType = typeof(List<>).MakeGenericType(new[] { type });
			//	IList list = (IList)Activator.CreateInstance(listType);
			//	list.Add(s);
			//	BufferedSpriteDictionary[this.GetType()][type] = list;
			//}
			//else
			//{
			//	BufferedSpriteDictionary[this.GetType()][type].Add(s);
			//}
		}

		// This returns all Sprites of the current scene
		public Dictionary<Type, IList> GetAllSprites()
		{
			return SpriteDictionary[this.GetType()];
		}

		// This is used to spawn all objects
		protected abstract void CreateScene();
        public void ResetScene()
		{
			SpriteDictionary[this.GetType()].Clear();
			CreateScene();
		}

		// Updates all registered Sprites
		public virtual void Update(GameTime gameTime)
		{
			if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;

			List<AbstractSprite> deleted = new List<AbstractSprite>();
			foreach (IList l in SpriteDictionary[this.GetType()].Values)
			{
				foreach (AbstractSprite c in l)
				{
					c.Update(gameTime);
					if (c.IsDeleted()) deleted.Add(c);
				}
			}

			foreach (AbstractSprite c in deleted)
			{
				SpriteDictionary[this.GetType()][c.GetType()].Remove(c);
			}

			// Adding spawned Sprites to the SpriteDictionary

			// foreach scene go through each sprite type and add them to the SpriteDictionary

            foreach (Type t in BufferedSpriteDictionary.Keys)
            {
                foreach (AbstractSprite c in BufferedSpriteDictionary[t])
                {
                    if (!SpriteDictionary[t].ContainsKey(c.GetType()))
                    {
                        Type listType = typeof(List<>).MakeGenericType(new[] { c.GetType() });
                        IList list = (IList)Activator.CreateInstance(listType);
                        SpriteDictionary[t][c.GetType()] = list;
                    }

                    //Console.WriteLine("Adding Sprite of Type {0}", c.GetType());
                    SpriteDictionary[t][c.GetType()].Add(c);
                }

                BufferedSpriteDictionary[t].Clear();
            }


			//foreach (Type t in BufferedSpriteDictionary.Keys)
			//{
			//	foreach (Type k in BufferedSpriteDictionary[t].Keys)
			//	{
			//		// If the scene doesn't exist in the SpriteDictionary, we'll have to create it
			//		if (!SpriteDictionary.ContainsKey(t)) SpriteDictionary[t] = new Dictionary<Type, IList>();

			//		// If the sprite doesn't exist in the SpriteDictionary, we'll have to create it
			//		if (!SpriteDictionary[t].ContainsKey(k))
			//		{
			//			Type listType = typeof(List<>).MakeGenericType(new[] { k });
			//			IList list = (IList)Activator.CreateInstance(listType);
			//			SpriteDictionary[t][k] = list;
			//		}

			//		// Add members to the SpriteDictionary
			//		foreach (var c in BufferedSpriteDictionary[t][k])
			//		{
			//			SpriteDictionary[t][k].Add(c);
			//		}
			//	}

			//	// Clear Scene in the BSD
			//	BufferedSpriteDictionary[t].Clear();
			//}
		}

		// Draws all registered Sprites
        public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;
			foreach (IList l in SpriteDictionary[this.GetType()].Values)
			{
				foreach (AbstractSprite c in l) c.Draw(spriteBatch);
			}
		}

	}
}
