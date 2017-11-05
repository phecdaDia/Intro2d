using Intro2DGame.Game.Scenes.Stages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes
{
    public class SceneManager
    {
        // Singleton instance
        private static SceneManager Instance;

        // Dictionary for all scenes. Scenes don't have to be initiated. 
        private readonly Dictionary<String, Scene> Scenes;

        // our current scene.
        private Scene CurrentScene;

        public SceneManager()
        {
            // Sets the Singleton instance
            Instance = this;

            // Creates the Scenes Dictionary
            this.Scenes = new Dictionary<string, Scene>();

            // Creates all scenes. 
            CreateScenes();
        }

        private void CreateScenes()
        {
            // Just do new SomethingScene(); to add a scene. They will register themself. 
            new ExampleScene();
            this.CurrentScene = new MainMenuScene();

			new TutorialScene();
        }

		// Returns the current scene
        public static Scene GetCurrentScene()
        {
            return GetInstance().CurrentScene;
        }

		// Sets the current scene. 
		// If the scene is the same we just return;
		public static void SetCurrentScene(string key)
		{
			SceneManager sm = GetInstance();

			if (sm.CurrentScene.SceneKey == key) return;

			if (sm.Scenes.ContainsKey(key)) sm.CurrentScene = sm.Scenes[key];

			sm.CurrentScene.ResetScene();
		}

        // Getting the Singleton instance
        private static SceneManager GetInstance()
        {
	        return Instance ?? (Instance = new SceneManager());
        }

        // Allows registering a scene,
        // Scene.cs registers here. You don't have to do this again.
        public static void RegisterScene(string key, Scene scene)
        {
            GetInstance().Scenes.Add(key, scene);
        }

		public static List<T> GetSprites<T>()
		{
			return GetCurrentScene().GetSprites<T>();
		}

		public static Dictionary<Type, IList> GetAllSprites()
		{
			return GetCurrentScene().GetAllSprites();
		}
    }
}
