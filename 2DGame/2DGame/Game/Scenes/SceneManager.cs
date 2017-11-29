using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Intro2DGame.Game.Scenes.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Intro2DGame.Game.Scenes.Debug;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites;

namespace Intro2DGame.Game.Scenes
{
	/// <summary>
	/// Manages all <see cref="Scene"/>
	/// </summary>
	public class SceneManager
	{
		/// <summary>
		/// Singleton instance of the <see cref="SceneManager"/>
		/// </summary>
		private static SceneManager Instance;

		/// <summary>
		/// Dictionary for <see cref="Scene"/>
		/// Primary key is the <see cref="Scene.SceneKey"/>
		/// </summary>
		private readonly Dictionary<string, Scene> SceneDictionary;

		/// <summary>
		/// Current Stack of <see cref="Scene"/>
		/// 
		/// It's not actually a <see cref="Stack{Scene}"/>. It's a <see cref="List{Scene}"/> to draw all scenes in order.
		/// </summary>
		private readonly List<Scene> SceneStack;

		/// <summary>
		/// The current top scene.
		/// </summary>
		private Scene CurrentScene
		{
			get { return SceneStack.Count > 0 ? SceneStack.Last() : null; }
		}

		private AbstractTransition CurrentTransition = null;

		private static int ClosingScene;


		public SceneManager()
		{

			// Creates the SceneDictionary Dictionary
			SceneDictionary = new Dictionary<string, Scene>();
			SceneStack = new List<Scene>();
		}

		/// <summary>
		/// Registers all scenes to the <see cref="Scene"/>
		/// </summary>
		private void CreateScenes()
		{
			RegisterScene(new ExampleScene());
			RegisterScene(new Example2Scene());

			RegisterScene(new MainMenuScene());

			RegisterScene(new TutorialScene());

			RegisterScene(new MenuScene());


			SetScene("mainmenu");
		}

		/// <summary>
		/// Statically returns the current <see cref="Scene"/>
		/// </summary>
		/// <returns>The current top layer <see cref="Scene"/></returns>
		public static Scene GetCurrentScene()
		{
			return GetInstance().CurrentScene;
		}

		/// <summary>
		/// Clears <see cref="SceneStack"/> and adds a <see cref="Scene"/> by <paramref name="key"/>
		/// </summary>
		/// <param name="key">Key of the scene</param>
		public static void SetScene(string key)
		{
			var sm = GetInstance();

			sm.SceneStack.Clear();
			sm.SceneStack.Add(sm.SceneDictionary[key]);
			sm.CurrentScene.ResetScene();
		}

		/// <summary>
		/// Closes <paramref name="amount"/> <see cref="Scene"/> from <see cref="SceneStack"/>
		/// </summary>
		/// <param name="amount">Amount of scenes to be closed</param>
		public static void CloseScene(int amount = 1)
		{
			ClosingScene += amount;
		}

		/// <summary>
		/// Removes one <see cref="Scene"/> from the <see cref="SceneStack"/>
		/// </summary>
		private static void RemoveScene()
		{
			var sm = GetInstance();

			var c = sm.SceneStack.Count;

			if (c <= 0) return;

			sm.SceneStack.Last().UnloadContent();
			sm.SceneStack.Last().ResetScene();
			sm.SceneStack.RemoveAt(c-1);
		}

		/// <summary>
		/// Adds a <see cref="Scene"/> to the <see cref="SceneStack"/> by <paramref name="key"/>
		/// </summary>
		/// <param name="key"></param>
		/// <param name="transition"></param>
		public static void AddScene(string key, AbstractTransition transition = null)
		{
			if (key == GetCurrentScene().SceneKey) return;

			if (transition == null)
			{
				var sm = GetInstance();

				sm.SceneStack.Add(sm.SceneDictionary[key]);
				sm.CurrentScene.LoadContent();
				sm.CurrentScene.ResetScene();
			}
			else
			{
				transition.TransitioningKey = key;
				AddTransition(transition);
			}

		}

		/// <summary>
		/// Adds a <see cref="Scene"/> to the <see cref="SceneStack"/>
		/// </summary>
		/// <param name="scene"><see cref="Scene"/> that's added to the <see cref="SceneStack"/></param>
		/// <param name="transition"></param>
		public static void AddScene(Scene scene, AbstractTransition transition = null)
		{
			if (transition == null)
			{
				var sm = GetInstance();

				sm.SceneStack.Add(scene);
				sm.CurrentScene.LoadContent();
				sm.CurrentScene.ResetScene();
			}
			else
			{
				transition.TransitioningScene = scene;
				AddTransition(transition);
			}
		}

		/// <summary>
		/// TODO
		/// </summary>
		/// <param name="transition"></param>
		private static void AddTransition(AbstractTransition transition)
		{
			transition.LoadContent();

			GetInstance().CurrentTransition = transition;
		}

		/// <summary>
		/// TODO
		/// </summary>
		public static void CloseTransition()
		{
			GetInstance().CurrentTransition?.UnloadContent();

			GetInstance().CurrentTransition = null;
		}

		/// <summary>
		/// Singleton Instance
		/// </summary>
		/// <returns>Singleton instance</returns>
		private static SceneManager GetInstance()
		{
			if (Instance == null)
			{
				Instance = new SceneManager();
				Instance.CreateScenes();
			}
			return Instance;
		}

		/// <summary>
		/// Registers a <see cref="Scene"/> to the <see cref="SceneDictionary"/>
		/// </summary>
		/// <param name="scene"><see cref="Scene"/> that's added to the <see cref="SceneDictionary"/></param>
		private static void RegisterScene(Scene scene)
		{
			GetInstance().SceneDictionary.Add(scene.SceneKey, scene);
		}

		/// <summary>
		/// Returns all <see cref="AbstractSprite"/> of <see cref="CurrentScene"/>
		/// </summary>
		/// <typeparam name="T">Any <see cref="AbstractSprite"/></typeparam>
		/// <returns><see cref="List{T}"/> with all <see cref="AbstractSprite"/> of the <see cref="CurrentScene"/></returns>
		public static List<T> GetSprites<T>()
		{
			return GetCurrentScene().GetSprites<T>();
		}

		/// <summary>
		/// <see cref="Dictionary{Type, AbstractSprite}"/> of <see cref="CurrentScene"/>
		/// </summary>
		/// <returns></returns>
		public static Dictionary<Type, IList> GetAllSprites()
		{
			return GetCurrentScene().GetAllSprites();
		}

		/// <summary>
		/// Statically returns length of <see cref="SceneStack"/> 
		/// </summary>
		/// <returns><see cref="SceneStack"/> count</returns>
		public static int GetStackSize()
		{
			return GetInstance().SceneStack.Count;
		}

		/// <summary>
		/// Updates <see cref="CurrentScene"/> and closes all closed <see cref="Scene"/>
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
		{
			var sm = GetInstance();

			if (sm.CurrentTransition != null)
			{
				sm.CurrentTransition.Update(gameTime);

				//if (sm.CurrentScene?.GetAllSprites().Count <= 0)
				//	sm.CurrentScene?.Update(gameTime);
			}
			else
			{
				sm.CurrentScene?.Update(gameTime);
			}

			while (ClosingScene > 0 )
			{
				ClosingScene -= 1;
				RemoveScene();
			}
		}

		/// <summary>
		/// Draws all <see cref="Scene"/> in <see cref="SceneStack"/> in ascending order.
		/// <para />
		/// First in first drawn.
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var s in GetInstance().SceneStack)
			{
				s.Draw(spriteBatch);
			}

			GetInstance().CurrentTransition?.Draw(spriteBatch);
		}
	}
}