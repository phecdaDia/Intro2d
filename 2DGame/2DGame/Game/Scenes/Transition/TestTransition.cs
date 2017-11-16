using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes.Transition
{
    /// <summary>
    /// Example transition
    /// This transition displays a black screen for the duration specified in the constructor
    /// </summary>
    public class TestTransition : Scene
    {
        private int Milliseconds, Elapsed = 0;
        private Texture2D Texture;

        public TestTransition(int milliseconds) : base("transition-test")
        {
            this.Milliseconds = milliseconds;
        }

        protected override void CreateScene()
        {}

        public override void Update(GameTime gameTime)
        {
            this.Elapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (Elapsed >= Milliseconds) SceneManager.CloseScene();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Drawing the texture over the entire graphics area. 
            // Recoloring it to be black with 255 alpha.
            spriteBatch.Draw(this.Texture, Game.GraphicsAreaRectangle, new Color(0xff000000u));
        }

        /// <summary>
        /// Loading custom content to display the black rectangle
        /// </summary>
        public override void LoadContent()
        {
            // Creating a 1x1 pixel texture that is white so we can recolor it in the spriteBatch.Draw();
            Texture = new Texture2D(Game.GetInstance().GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Disposing any loading content.
        /// </summary>
        public override void UnloadContent()
        {
            Texture.Dispose();
        }
    }
}
