using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Sprites
{
    public class PlayerSprite : AbstractSprite
    {
        public PlayerSprite(Vector2 Position) : base("player", Position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Vector2 Movement = new Vector2();
            Vector2 Area= Game.GraphicsArea;

			// buffering movement
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W)) Movement += new Vector2(0, -1);
            if (ks.IsKeyDown(Keys.S)) Movement += new Vector2(0, 1);
            if (ks.IsKeyDown(Keys.A)) Movement += new Vector2(-1, 0);
            if (ks.IsKeyDown(Keys.D)) Movement += new Vector2(1, 0);
            
			// normalizing movement
            if (Movement.LengthSquared() > 0f) Movement.Normalize();
            this.position += Movement * 5f;

			// Prevents player from leaving the screen
            if ((this.position.X + this.Texture.Width / 2) > Area.X) this.position.X = Area.X - this.Texture.Width / 2;
            if ((this.position.Y + this.Texture.Height / 2) > Area.Y) this.position.Y = Area.Y - this.Texture.Height / 2;
            if ((this.position.X - this.Texture.Width / 2) < 0) this.position.X = this.Texture.Width / 2;
            if ((this.position.Y - this.Texture.Height / 2) < 0) this.position.Y = this.Texture.Height / 2;
        }
    }
}
