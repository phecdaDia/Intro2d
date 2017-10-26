using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;

namespace Intro2DGame.Game.Sprites
{
    public class MainMenuSprite : AbstractSprite
    {
        // Dictionary used so we change the index only when the key is pressed down.
        // holding down the button shouldn't change the index.
        private Dictionary<Keys, int> pressedKeys;
        // the current selected Index. 
        private int selectedIndex;

        private readonly int timeoutDelay = 60;

		private List<Texture2D> menuEntries;

        public MainMenuSprite() : base(new Vector2())
        {
            this.pressedKeys = new Dictionary<Keys, int>();
            this.selectedIndex = 0;

			menuEntries = new List<Texture2D>();
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #1"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #2"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #3"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #4"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #5"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #6"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #7"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #8"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene! #9"));
		}

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Testing if the keys are in the dictionary to avoid any exceptions
            foreach (Keys k in ks.GetPressedKeys())
            {
                if (!pressedKeys.ContainsKey(k)) pressedKeys.Add(k, 0);
            }

            // decreasing the timeout by a frame

			// TODO: Currently broken. 

    //        foreach ()
    //        {
				//pressedKeys.
    //        }

			if (ks.IsKeyDown(Keys.Enter))
			{
				SceneManager.SetCurrentScene("example");
			}

            // Getting index down
            if (ks.IsKeyDown(Keys.W) && pressedKeys[Keys.W] == 0 || ks.IsKeyDown(Keys.Up) && pressedKeys[Keys.Up] == 0)
            {
                selectedIndex--;
                pressedKeys[Keys.W] = timeoutDelay;
                pressedKeys[Keys.Up] = timeoutDelay;
            }
            else if (ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.Up))
            {
                pressedKeys[Keys.W] = 0;
                pressedKeys[Keys.Up] = 0;
            }

            // Getting the index up
            if (ks.IsKeyDown(Keys.S) && pressedKeys[Keys.S] == 0 || ks.IsKeyDown(Keys.Down) && pressedKeys[Keys.Down] == 0)
            {
                selectedIndex++;
                pressedKeys[Keys.S] = timeoutDelay;
                pressedKeys[Keys.Down] = timeoutDelay;
            }
            else if (ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.Down))
            {
                pressedKeys[Keys.S] = 0;
                pressedKeys[Keys.Down] = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO:

            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"), new Vector2(50, 50 + selectedIndex * 50), Color.White);

			int idx = 0;
			foreach (Texture2D menuItem in menuEntries)
			{
				spriteBatch.Draw(menuItem, new Vector2(100, 50 + idx++ * 50), Color.White);
			}


            spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
        }
    }
}
