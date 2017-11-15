using System;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Sprites
{
	public class PlayerSprite : AbstractSprite
	{
		private const int SHOOT_DELAY = 7;
		private int ShootDelay;

		private int Invulnerable;
		private int Shot;


		public PlayerSprite(Vector2 position) : base("player", position)
		{
			SetLayerDepth(1);
			SceneManager.GetCurrentScene().AddSprite(new BannerSprite(this)); // This adds the banner
			SceneManager.GetCurrentScene().AddSprite(new ViginetteSprite(this)); // This adds the banner

			MaxHealth = 1000;
			Health = 1000;
		}

		public override void Update(GameTime gameTime)
		{
			var movement = new Vector2();
			var area = Game.GraphicsArea;


			// Shoots bullets

			var ms = Mouse.GetState();
			Shot -= Shot > 0 ? 1 : 0;

			if (ShootDelay-- <= 0 || KeyboardManager.IsKeyDown(Keys.Space))
			{
				if (KeyboardManager.IsKeyPressed(Keys.Space))

				{
					SpawnSprite(new PlayerOrb(GetPosition(), Position + new Vector2(1, 0)));
					ShootDelay = SHOOT_DELAY;

					Shot = SHOOT_DELAY + 5;
				}
				else if (ms.LeftButton == ButtonState.Pressed)
				{
					SpawnSprite(new PlayerOrb(GetPosition(), ms.Position.ToVector2()));
					ShootDelay = SHOOT_DELAY;

					Shot = SHOOT_DELAY + 5;
				}
			}

			// buffering movement
			if (KeyboardManager.IsKeyPressed(Keys.W)) movement += new Vector2(0, -1);
			if (KeyboardManager.IsKeyPressed(Keys.S)) movement += new Vector2(0, 1);
			if (KeyboardManager.IsKeyPressed(Keys.A)) movement += new Vector2(-1, 0);
			if (KeyboardManager.IsKeyPressed(Keys.D)) movement += new Vector2(1, 0);

			// normalizing movement
			if (movement.LengthSquared() > 0f) movement.Normalize();

			movement *= new Vector2(1.1f, 1.0f);
			Position += movement * (Shot > 0 && GameConstants.IS_MOVEMENT_RESTRICTED ? 2.75f : 4.25f);

			// Prevents player from leaving the screen
			if (Position.X + Texture.Width / 2f > area.X) Position.X = area.X - Texture.Width / 2f;
			if (Position.Y + Texture.Height / 2f > area.Y) Position.Y = area.Y - Texture.Height / 2f;
			if (Position.X - Texture.Width / 2f < 0) Position.X = Texture.Width / 2f;
			if (Position.Y - Texture.Height / 2f < 100) Position.Y = 100 + Texture.Height / 2f;




			// Debug only!
			if (KeyboardManager.IsKeyPressed(Keys.F1)) Health += 1;
			if (KeyboardManager.IsKeyPressed(Keys.F2)) Health -= 1;
			if (KeyboardManager.IsKeyPressed(Keys.F4)) Health = MaxHealth;
			// end

			if (Invulnerable > 0) Invulnerable--;
			else
			{
				if (GameConstants.IS_AUTOREGEN_ENABLED)
				{

					if (GameConstants.IS_AUTOREGEN_RESTRICTED)
					{
						if (Shot == 0) Health += 1;
					}
					else Health += 1;
				}
			}

			if (Health >= MaxHealth) Health = MaxHealth;

			if (Health <= 0) SceneManager.CloseScene();

		}

		public override bool DoesCollide(AbstractOrb orb)
		{
			var tp1 = Position + new Vector2(0, 16) - orb.GetPosition();
			var tp2 = Position + new Vector2(0, -16) - orb.GetPosition();

			return tp1.LengthSquared() <= 256 || tp2.LengthSquared() <= 256;
		}

		public void Damage(int amount)
		{
			if (Invulnerable > 0) return;
			this.Health -= amount;
			this.Invulnerable = 15;
		}
	}

	internal class BannerSprite : ImageSprite
	{
		private readonly PlayerSprite Player;

		public BannerSprite(PlayerSprite player) : base("banner", new Vector2())
		{
			this.Player = player;

			Persistence = true;
			SetLayerDepth(10);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Hue);

			spriteBatch.DrawString(Game.FontArial, $"Health: {Player.Health}", new Vector2(30, 30), Color.Black);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}

	internal class ViginetteSprite : ImageSprite
	{
		private readonly PlayerSprite Player;

		public ViginetteSprite(PlayerSprite player) : base("viginette", new Vector2())
		{
			this.Player = player;

			Persistence = true;
			SetLayerDepth(11);

			this.Hue = Color.Transparent;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.Texture, new Rectangle(0, 0, (int)Game.GraphicsArea.X, (int)Game.GraphicsArea.Y), null, this.Hue);
		}

		public override void Update(GameTime gameTime)
		{
			var k = (this.Player.MaxHealth - this.Player.Health) / ((float) this.Player.MaxHealth);
			var c = new Color((int) (255f * k), 0, 0, (int) (64f * k));

			this.Hue = c;
		}
	}

	public class PlayerOrb : LinearOrb
	{
		//private static readonly Vector2 Direction = new Vector2(0, -1);
		private const float SPEED = 7.5f;


		public PlayerOrb(Vector2 position, Vector2 goal) : base(position, goal - position, SPEED)
		{
			Hue = Color.Purple;
		}

		public override void Update(GameTime gameTime)
		{
			Position += UpdatePosition(gameTime);

			var sprites = SceneManager.GetAllSprites();

			foreach (var t in sprites.Keys)
			foreach (AbstractSprite sprite in sprites[t])
			{
				if (!sprite.Enemy) continue;

				if (sprite.DoesCollide(this)) {
					sprite.Health -= 1;
					Delete();
				}
			}
		}
	}
}