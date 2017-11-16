using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes.Transition
{
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
            spriteBatch.Draw(this.Texture, Game.GraphicsAreaRectangle, new Color(0xff000000u));
        }

        public override void LoadContent()
        {
            Texture = new Texture2D(Game.GetInstance().GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[] { Color.White });
        }

        public override void UnloadContent()
        {
            Texture.Dispose();
        }
    }
}
