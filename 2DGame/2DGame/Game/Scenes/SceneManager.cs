using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Intro2DGame.Game.Scenes.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Scenes
{
	public class SceneManager
	{
		// Singleton instance
		private static SceneManager Instance;

		// Dictionary for all scenes. Scenes don't have to be initiated. 
		private readonly Dictionary<string, Scene> Scenes;

		private readonly List<Scene> SceneStack;

		// our current scene.
		private Scene CurrentScene;

		private static int ClosingScene;


		public SceneManager()
		{
			// Sets the Singleton instance
			Instance = this;

			// Creates the Scenes Dictionary
			Scenes = new Dictionary<string, Scene>();
			SceneStack = new List<Scene>();

			// Creates all scenes. 
			CreateScenes();
		}

		private void CreateScenes()
		{
			RegisterScene(new ExampleScene());
			RegisterScene(new Example2Scene());
			RegisterScene(new MainMenuScene());

			RegisterScene(new TutorialScene());

			RegisterScene(new MenuScene());


			SetScene("mainmenu");
		}

		// Returns the current scene
		public static Scene GetCurrentScene()
		{
			return GetInstance().CurrentScene;
		}

		// Clears the Stack and opens a scene
		public static void SetScene(string key)
		{
			var sm = GetInstance();

			sm.SceneStack.Clear();
			sm.SceneStack.Add(sm.Scenes[key]);
			sm.CurrentScene = sm.SceneStack.Last();
			sm.CurrentScene.ResetScene();
		}

		public static void CloseScene()
		{
			ClosingScene += 1;
		}

		private static void RemoveScene()
		{
			var sm = GetInstance();

			var c = sm.SceneStack.Count;

			if (c <= 0) return;

			sm.SceneStack.Last().ResetScene();
			sm.SceneStack.RemoveAt(c-1);

			sm.CurrentScene = c > 1 ? sm.SceneStack.Last() : null;
		}

		public static void AddScene(string key)
		{
			if (key == GetCurrentScene().SceneKey) return;

			var sm = GetInstance();

			sm.SceneStack.Add(sm.Scenes[key]);
			sm.CurrentScene = sm.SceneStack.Last();
			sm.CurrentScene.ResetScene();
		}

		public static void AddScene(Scene scene)
		{
			var sm = GetInstance();

			sm.SceneStack.Add(scene);
			sm.CurrentScene = scene;
			sm.CurrentScene.ResetScene();
		}

		// Getting the Singleton instance
		private static SceneManager GetInstance()
		{
			return Instance ?? (Instance = new SceneManager());
		}

		// Allows registering a scene,
		private static void RegisterScene(Scene scene)
		{
			GetInstance().Scenes.Add(scene.SceneKey, scene);
		}

		public static List<T> GetSprites<T>()
		{
			return GetCurrentScene().GetSprites<T>();
		}

		public static Dictionary<Type, IList> GetAllSprites()
		{
			return GetCurrentScene().GetAllSprites();
		}

		public static int GetStackSize()
		{
			return GetInstance().SceneStack.Count;
		}

		public static void Update(GameTime gameTime)
		{
			GetInstance().CurrentScene?.Update(gameTime);

			while (ClosingScene > 0 )
			{
				ClosingScene -= 1;
				RemoveScene();
			}
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var s in GetInstance().SceneStack)
			{
				s.Draw(spriteBatch);
			}
			//GetInstance().CurrentScene?.Draw(spriteBatch);
		}
	}
}