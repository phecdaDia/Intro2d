using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

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
