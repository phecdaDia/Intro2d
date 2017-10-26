using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes
{
    // This is the individual level
    public abstract class Scene
    {
        private String sceneKey;


        public Scene(String key)
        {
            // Setting our sceneKey
            this.sceneKey = key;

            // Registering the scene in the SceneManager
            SceneManager.RegisterScene(key, this);

            CreateScene();
        }

        // This is used to spawn all objects
        protected abstract void CreateScene();
        public abstract void ResetScene();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
