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
        private static SceneManager sceneManager;

        // Dictionary for all scenes. Scenes don't have to be initiated. 
        private Dictionary<String, Scene> scenes;

        // our current scene.
        private Scene currentScene;

        public SceneManager()
        {
            // Sets the Singleton instance
            sceneManager = this;

            // Creates the Scenes Dictionary
            this.scenes = new Dictionary<string, Scene>();

            // Creates all scenes. 
            CreateScenes();
        }

        private void CreateScenes()
        {
            // Just do new SomethingScene(); to add a scene. They will register themself. 
            new ExampleScene();
            this.currentScene = new MainMenuScene();
        }

		// Returns the current scene
        public static Scene GetCurrentScene()
        {
            return GetInstance().currentScene;
        }

		// Sets the current scene. 
		// If the scene is the same we just return;
		public static void SetCurrentScene(String key)
		{
			SceneManager sm = GetInstance();

			if (sm.currentScene.SceneKey == key) return;

			if (sm.scenes.ContainsKey(key)) sm.currentScene = sm.scenes[key];

			sm.currentScene.ResetScene();
		}

        // Getting the Singleton instance
        private static SceneManager GetInstance()
        {
            if (sceneManager == null) sceneManager = new SceneManager();
            return sceneManager;
        }

        // Allows registering a scene,
        // Scene.cs registers here. You don't have to do this again.
        public static void RegisterScene(string key, Scene scene)
        {
            GetInstance().scenes.Add(key, scene);
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
