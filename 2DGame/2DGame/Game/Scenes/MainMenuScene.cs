using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Intro2DGame.Game.Sprites;

namespace Intro2DGame.Game.Scenes
{
    public class MainMenuScene : Scene
    {
        private MainMenuSprite mainMenuSprite;

        public MainMenuScene() : base("mainmenu") { }


        public override void Draw(SpriteBatch spriteBatch)
        {
            this.mainMenuSprite.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            this.mainMenuSprite.Update(gameTime);
        }

        protected override void CreateScene()
        {
            this.mainMenuSprite = new MainMenuSprite();
        }

        protected override void ResetScene()
        {
            
        }
    }
}
