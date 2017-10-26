using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public MainMenuSprite() : base(new Vector2())
        {
            this.pressedKeys = new Dictionary<Keys, int>();
            this.selectedIndex = 0;
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

            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/test"), new Vector2(100, 50), Color.White);
            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/test"), new Vector2(100, 100), Color.White);
            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/test"), new Vector2(100, 150), Color.White);
            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/test"), new Vector2(100, 200), Color.White);
            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/test"), new Vector2(100, 250), Color.White);


            spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
        }
    }
}
