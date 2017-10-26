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
        private Dictionary<Keys, Boolean> pressedKeys;
        // the current selected Index. 
        private int selectedIndex;

        public MainMenuSprite() : base(new Vector2())
        {
            this.pressedKeys = new Dictionary<Keys, bool>();
            this.selectedIndex = 0;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Testing if the keys are in the dictionary to avoid any exceptions
            foreach (Keys k in ks.GetPressedKeys())
            {
                if (!pressedKeys.ContainsKey(k)) pressedKeys.Add(k, false);
            }

            // Getting index down
            if (ks.IsKeyDown(Keys.W) && !pressedKeys[Keys.W] || ks.IsKeyDown(Keys.Up) && !pressedKeys[Keys.Up])
            {
                selectedIndex--;
                pressedKeys[Keys.W] = true;
                pressedKeys[Keys.Up] = true;
            }
            else if (ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.Up))
            {
                pressedKeys[Keys.W] = false;
                pressedKeys[Keys.Up] = false;
            }

            // Getting the index up
            if (ks.IsKeyDown(Keys.S) && !pressedKeys[Keys.S] || ks.IsKeyDown(Keys.Down) && !pressedKeys[Keys.Down])
            {
                selectedIndex++;
                pressedKeys[Keys.S] = true;
                pressedKeys[Keys.Down] = true;
            }
            else if (ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.Down))
            {
                pressedKeys[Keys.S] = false;
                pressedKeys[Keys.Down] = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO:
            spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 100), Color.Black);
        }
    }
}
