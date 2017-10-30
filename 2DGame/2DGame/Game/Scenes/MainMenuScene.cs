using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Fonts;

namespace Intro2DGame.Game.Scenes
{
    public class MainMenuScene : Scene
    {
        private MainMenuSprite mainMenuSprite;
		private ImageSprite titleTextSprite;

        public MainMenuScene() : base("mainmenu")
		{
		}


        public override void Draw(SpriteBatch spriteBatch)
        {
            this.mainMenuSprite.Draw(spriteBatch);
			this.titleTextSprite.Draw(spriteBatch);
			
        }

        public override void Update(GameTime gameTime)
        {
            this.mainMenuSprite.Update(gameTime);
        }

        protected override void CreateScene()
        {
            this.mainMenuSprite = new MainMenuSprite();
			this.titleTextSprite = new ImageSprite("title", new Vector2(400, 40));
        }

        public override void ResetScene()
        {
            
        }
    }
}
