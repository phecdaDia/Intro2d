using System;
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

        public Scene GetCurrentScene()
        {
            return this.currentScene;
        }

        // Getting the Singleton instance
        public static SceneManager GetInstance()
        {
            if (sceneManager == null) sceneManager = new SceneManager();
            return sceneManager;
        }

        // Allows registering a scene,
        // Scene.cs registers here. You don't have to do this again.
        public void RegisterScene(string key, Scene scene)
        {
            this.scenes.Add(key, scene);
        }
    }
}
