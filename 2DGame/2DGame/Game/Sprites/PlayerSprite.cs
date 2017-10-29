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
            Vector2 Area= Intro2DGame.Game.Game.GraphicsArea;

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W)) Movement += new Vector2(0, -1);
            if (ks.IsKeyDown(Keys.S)) Movement += new Vector2(0, 1);
            if (ks.IsKeyDown(Keys.A)) Movement += new Vector2(-1, 0);
            if (ks.IsKeyDown(Keys.D)) Movement += new Vector2(1, 0);
            
            if (Movement.LengthSquared() > 0f) Movement.Normalize();
            this.position += Movement * 5f;

            //if ((this.position.X - this.texture.Width) > Area.X) this.position.X = Area.X - this.texture.Width;
            //if ((this.position.Y - this.texture.Height) > Area.Y) this.position.Y = Area.Y - this.texture.Height;
            if (this.position.X < 0) this.position.X = 0;
            if (this.position.Y < 0) this.position.Y = 0;
        }
    }
}
