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
        private int SelectedIndex;
        private int UpShowIndex;

        private readonly int timeoutDelay = 60;

		private List<Texture2D> menuEntries;

		private static readonly int MAX_MENU_ENTRIES = 10;

        public MainMenuSprite() : base()
        {
            this.pressedKeys = new Dictionary<Keys, int>();
            this.SelectedIndex = 0;
            this.UpShowIndex = 0;

			menuEntries = new List<Texture2D>();
			menuEntries.Add(FontManager.CreateFontString("example", "Introductions"));
			menuEntries.Add(FontManager.CreateFontString("example", "Knockout Round"));
			menuEntries.Add(FontManager.CreateFontString("example", "Round 1"));
			menuEntries.Add(FontManager.CreateFontString("example", "Round 2"));
			menuEntries.Add(FontManager.CreateFontString("example", "Finals"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #6"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene!"));
            menuEntries.Add(FontManager.CreateFontString("example", "Exit"));

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Testing if the keys are in the dictionary to avoid any exceptions
            foreach (Keys k in ks.GetPressedKeys())
            {
                if (!pressedKeys.ContainsKey(k)) pressedKeys.Add(k, 0);
            }

			if (ks.IsKeyDown(Keys.Enter))
			{
				switch (SelectedIndex)
				{
					case 0: SceneManager.SetCurrentScene("mainmenu"); break;
					case 1: SceneManager.SetCurrentScene("mainmenu"); break;
					case 2: SceneManager.SetCurrentScene("mainmenu"); break;
					case 3: SceneManager.SetCurrentScene("mainmenu"); break;
					case 4: SceneManager.SetCurrentScene("mainmenu"); break;
					case 5: SceneManager.SetCurrentScene("mainmenu"); break;
					case 6: SceneManager.SetCurrentScene("example"); break;
					case 7: Game.ExitGame(); break;
				}
				
			}

            // Getting index down
            if (ks.IsKeyDown(Keys.W) && pressedKeys[Keys.W] == 0 || ks.IsKeyDown(Keys.Up) && pressedKeys[Keys.Up] == 0)
            {
                SelectedIndex--;
                if (UpShowIndex > 0) UpShowIndex--;
                if (SelectedIndex < 0)
                {
                    UpShowIndex = menuEntries.Count - MAX_MENU_ENTRIES;
					if (UpShowIndex < 0) UpShowIndex = 0;
                    SelectedIndex += menuEntries.Count;
                }
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
                SelectedIndex++;
                if (SelectedIndex >= menuEntries.Count)
                {
                    SelectedIndex = 0;
                    UpShowIndex = 0;
                }

                if (SelectedIndex >= UpShowIndex + MAX_MENU_ENTRIES)
					UpShowIndex++;
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

            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"), new Vector2(50, 85 + (SelectedIndex - UpShowIndex) * 50), Color.White);

			int d = (this.menuEntries.Count > MAX_MENU_ENTRIES) ? MAX_MENU_ENTRIES : this.menuEntries.Count;

			int idx = 0;
            if(menuEntries.Count <= MAX_MENU_ENTRIES)
            {
                foreach (Texture2D menuItem in menuEntries)
                {
                    spriteBatch.Draw(menuItem, new Vector2(100, 85 + idx++ * 50), Color.White);
                }
            }
            else
            {
                for(int i=0;i<d;i++)
                    spriteBatch.Draw(menuEntries[i + UpShowIndex], new Vector2(100, 85 + idx++ * 50), Color.White);
            }
            //spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
        }
    }
}
