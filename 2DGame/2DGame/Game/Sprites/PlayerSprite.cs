using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Intro2DGame.Game.Sprites
{
    public class PlayerSprite : AbstractSprite
    {
	    private const int SHOOT_DELAY = 7;
	    private int ShootDelay;

        public PlayerSprite(Vector2 position) : base("player", position)
        {
            this.SetLayerDepth(1);
            SceneManager.GetCurrentScene().AddSprite(new BannerSprite()); // This adds the banner
        }

	    public override void Update(GameTime gameTime)
	    {

		    Vector2 movement = new Vector2();
		    Vector2 area = Game.GraphicsArea;

		    // buffering movement
		    KeyboardState ks = Keyboard.GetState();
		    if (KeyboardManager.IsKeyPressed(Keys.W)) movement += new Vector2(0, -1);
		    if (KeyboardManager.IsKeyPressed(Keys.S)) movement += new Vector2(0, 1);
		    if (KeyboardManager.IsKeyPressed(Keys.A)) movement += new Vector2(-1, 0);
		    if (KeyboardManager.IsKeyPressed(Keys.D)) movement += new Vector2(1, 0);

		    // normalizing movement
		    if (movement.LengthSquared() > 0f) movement.Normalize();
		    this.Position += movement * 3f;

		    // Prevents player from leaving the screen
		    if (this.Position.X + this.Texture.Width / 2f > area.X) this.Position.X = area.X - this.Texture.Width / 2f;
		    if (this.Position.Y + this.Texture.Height / 2f > area.Y) this.Position.Y = area.Y - this.Texture.Height / 2f;
		    if (this.Position.X - this.Texture.Width / 2f < 0) this.Position.X = this.Texture.Width / 2f;
		    if (this.Position.Y - this.Texture.Height / 2f < 100) this.Position.Y = 100 + this.Texture.Height / 2f;


		    // Shoots bullets

		    MouseState ms = Mouse.GetState();
		    if (ShootDelay-- <= 0)
			{
				if (KeyboardManager.IsKeyPressed(Keys.Space))
	
				{
					ShootOrb<PlayerOrb>(this.GetPosition(), this.Position + new Vector2(1, 0));
					ShootDelay = SHOOT_DELAY;
				}
				else if (ms.LeftButton == ButtonState.Pressed)
				{
					ShootOrb<PlayerOrb>(this.GetPosition(), ms.Position.ToVector2());
					ShootDelay = SHOOT_DELAY;
				}
			}

		}

	    public override bool DoesCollide(AbstractOrb orb)
        {
            Vector2 tp1 = this.Position + new Vector2(0, 16) - orb.GetPosition();
            Vector2 tp2 = this.Position + new Vector2(0, -16) - orb.GetPosition();

            return tp1.LengthSquared() <= 256 || tp2.LengthSquared() <= 256;
        }
    }

    class BannerSprite : ImageSprite
    {
        public BannerSprite () : base("banner", new Vector2())
        {
            this.Persistence = true;
            this.SetLayerDepth(10);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Hue);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    public class PlayerOrb : LinearOrb
    {
        //private static readonly Vector2 Direction = new Vector2(0, -1);
        private const float SPEED = 7.5f;


        public PlayerOrb(Vector2 position, Vector2 goal) : base(position, goal - position, SPEED)
        {
            this.Hue = Color.Purple;
        }

        public override void Update(GameTime gameTime)
        {
            this.Position += UpdatePosition(gameTime);

            Dictionary<Type, IList> sprites = SceneManager.GetAllSprites();

            foreach (Type t in sprites.Keys)
            {
                foreach (AbstractSprite sprite in sprites[t])
                {
                    if (!sprite.Enemy) break;

                    if (sprite.DoesCollide(this))
                    {
                        sprite.Damage(1);
                    }

                }
            }
        }
    }
}
