using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes
{
    // This is the individual level
    public abstract class Scene
    {
        public String SceneKey
		{
			get;
			private set;
		}

		private Dictionary<Type, IList> SpriteDictionary;

        public Scene(String key)
        {
            // Setting our sceneKey
            this.SceneKey = key;

			this.SpriteDictionary = new Dictionary<Type, IList>();

            // Registering the scene in the SceneManager
            SceneManager.RegisterScene(key, this);

            CreateScene();
        }

		public List<T> GetSprites<T>()
		{
			if (SpriteDictionary.ContainsKey(typeof(T))) return (List<T>) SpriteDictionary[typeof(T)];
			return new List<T>();
		}

		public void AddSprite(AbstractSprite s)
		{
			if (!SpriteDictionary.ContainsKey(s.GetType()))
			{
				//List<dynamic> l = new List<dynamic>();
				//l.Add(s);
				//this.SpriteDictionary[s.GetType()] = l;

				Type type = s.GetType();
				Type listType = typeof(List<>).MakeGenericType(new[] { type });
				IList list = (IList)Activator.CreateInstance(listType);
				list.Add(s);
				this.SpriteDictionary[type] = list;
			}
			else
			{
				this.SpriteDictionary[s.GetType()].Add(s);
			}
		}

		public Dictionary<Type, IList> GetAllSprites()
		{
			return this.SpriteDictionary;
		}

		// This is used to spawn all objects
		protected abstract void CreateScene();
        public abstract void ResetScene();

        public void Update(GameTime gameTime)
		{
			foreach (IList l in SpriteDictionary.Values)
			{
				foreach (AbstractSprite c in l) c.Update(gameTime);
			}
		}

        public void Draw(SpriteBatch spriteBatch)
		{
			foreach (IList l in SpriteDictionary.Values)
			{
				foreach (AbstractSprite c in l) c.Draw(spriteBatch);
			}
		}

	}
}
