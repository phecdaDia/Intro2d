using System;
using System.Collections;
using System.Collections.Generic;
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

		// our current scene.
		private Scene CurrentScene;

		private readonly Stack<Scene> SceneStack;


		public SceneManager()
		{
			// Sets the Singleton instance
			Instance = this;

			// Creates the Scenes Dictionary
			Scenes = new Dictionary<string, Scene>();
			SceneStack = new Stack<Scene>();

			// Creates all scenes. 
			CreateScenes();
		}

		private void CreateScenes()
		{
			RegisterScene(new ExampleScene());
			RegisterScene(new MainMenuScene());

			RegisterScene(new TutorialScene());


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
			sm.SceneStack.Push(sm.Scenes[key]);
			sm.CurrentScene = sm.SceneStack.Peek();
		}

		public static void CloseScene()
		{
			var sm = GetInstance();

			sm.SceneStack.Pop().ResetScene();
			if (sm.SceneStack.Count > 0) sm.CurrentScene = sm.SceneStack.Peek();
			else sm.CurrentScene = null;
		}

		public static void AddScene(string key)
		{
			var sm = GetInstance();

			sm.SceneStack.Push(sm.Scenes[key]);
			sm.CurrentScene = sm.SceneStack.Peek();
		}

		// Getting the Singleton instance
		private static SceneManager GetInstance()
		{
			return Instance ?? (Instance = new SceneManager());
		}

		// Allows registering a scene,
		public static void RegisterScene(Scene scene)
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
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			GetInstance().CurrentScene?.Draw(spriteBatch);
		}
	}
}