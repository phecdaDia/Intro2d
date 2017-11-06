using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Sprites
{
    public class PlayerSprite : AbstractSprite
    {
	    private const int SHOOT_DELAY = 7;
	    private int ShootDelay;

        public PlayerSprite(Vector2 position) : base("player", position)
        {
            this.SetLayerDepth(1);
            
        }

	    public override void Update(GameTime gameTime)
	    {

		    Vector2 movement = new Vector2();
		    Vector2 area = Game.GraphicsArea;

		    // buffering movement
		    KeyboardState ks = Keyboard.GetState();
		    if (ks.IsKeyDown(Keys.W)) movement += new Vector2(0, -1);
		    if (ks.IsKeyDown(Keys.S)) movement += new Vector2(0, 1);
		    if (ks.IsKeyDown(Keys.A)) movement += new Vector2(-1, 0);
		    if (ks.IsKeyDown(Keys.D)) movement += new Vector2(1, 0);

		    // normalizing movement
		    if (movement.LengthSquared() > 0f) movement.Normalize();
		    this.Position += movement * 3f;

		    // Prevents player from leaving the screen
		    if (this.Position.X + this.Texture.Width / 2f > area.X) this.Position.X = area.X - this.Texture.Width / 2f;
		    if (this.Position.Y + this.Texture.Height / 2f > area.Y) this.Position.Y = area.Y - this.Texture.Height / 2f;
		    if (this.Position.X - this.Texture.Width / 2f < 0) this.Position.X = this.Texture.Width / 2f;
		    if (this.Position.Y - this.Texture.Height / 2f < 0) this.Position.Y = this.Texture.Height / 2f;


		    // Shoots bullets

		    MouseState ms = Mouse.GetState();
		    if (ShootDelay-- <= 0)
			{
				if (ks.IsKeyDown(Keys.Space))
	
				{
					ShootOrb<PlayerOrb>(this.GetPosition(), this.Position + new Vector2(0, -1));
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
}
