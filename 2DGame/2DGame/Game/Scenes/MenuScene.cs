using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
	public class MenuScene : Scene
	{
		private MenuSprite MenuSprite;

		public MenuScene() : base("menu")
		{
			
		}

		protected override void CreateScene()
		{
			// this has to be implemented differently, sadly
			this.MenuSprite = new MenuSprite();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			this.MenuSprite.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			this.MenuSprite.Update(gameTime);
		}
	}

	class MenuSprite : AbstractAnimatedSprite
	{
		public MenuSprite() : base("test/coin", new Vector2(400, 300), 24, 10)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			if (KeyboardManager.IsKeyDown(Keys.Pause)) SceneManager.CloseScene();
		}
	}
}
