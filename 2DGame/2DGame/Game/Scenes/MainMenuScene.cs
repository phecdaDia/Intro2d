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

		public MainMenuScene() : base("mainmenu")
		{
		}

        protected override void CreateScene()
        {
            this.AddSprite(new MainMenuSprite());
			this.AddSprite(new ImageSprite("title", new Vector2(400, 40)));
        }
    }
}
