using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Sprites
{
	public class PlayerSprite : AbstractSprite
	{
		private const double SHOOT_DELAY = 0.15d;

		private double Invulnerable;

		private Rectangle PlayArea = new Rectangle(0, 0, Game.RenderSize.X - 200, Game.RenderSize.Y);
		private double ShootDelay;

		private readonly Vector2 ShootDirection = new Vector2(0, -1);
		private double Shot;


		//private readonly DirectionMarker DirectionMarker;

		public PlayerSprite(Vector2 position, bool isMainMenu = false) : base("player", position)
		{
			LayerDepth = 0;

			if (isMainMenu)
			{
				PlayArea = new Rectangle(50, 100, 150, 500);
				ShootDirection = new Vector2(1, 0);
			}
			else
			{
				SceneManager.GetCurrentScene().AddSprite(new BannerSprite(this, new Vector2(500, 0))); // This adds the banner
				//SceneManager.GetCurrentScene().AddSprite(new BannerSprite(this, 620)); // This adds the banner
				SceneManager.GetCurrentScene().AddSprite(new ViginetteSprite(this)); // This adds the banner

				SceneManager.GetCurrentScene().AddSprite(new HealthBarSprite(this, new Vector2(610, 50), 50, 800));
			}

			//this.DirectionMarker = new DirectionMarker(this);

			MaxHealth = 1000;
			Health = 1000;


			Persistence = true;
		}

		public override void Update(GameTime gameTime)
		{
			if (Game.GameArguments.IsCheatsEnabled && KeyboardManager.IsKeyPressed(Keys.F4))
			{
				Health = MaxHealth;
				Invulnerable = 2;
			}


			var movement = new Vector2();

			if (Invulnerable > 0) Invulnerable -= gameTime.ElapsedGameTime.TotalSeconds;
			if (Shot > 0) Shot -= gameTime.ElapsedGameTime.TotalSeconds;
			if (ShootDelay > 0) ShootDelay -= gameTime.ElapsedGameTime.TotalSeconds;

			// Shoots bullets

			var ms = Mouse.GetState();


			if (ShootDelay <= 0.0d || KeyboardManager.IsKeyDown(Keys.Space))
				if (KeyboardManager.IsKeyPressed(Keys.Space) || ms.LeftButton == ButtonState.Pressed)

				{
					SpawnSprite(new PlayerOrb(Position, ShootDirection));
					ShootDelay = SHOOT_DELAY;

					Shot = SHOOT_DELAY + 0.25d;
				}

			// buffering movement
			if (KeyboardManager.IsKeyPressed(Keys.W)) movement += new Vector2(0, -1);
			if (KeyboardManager.IsKeyPressed(Keys.S)) movement += new Vector2(0, 1);
			if (KeyboardManager.IsKeyPressed(Keys.A)) movement += new Vector2(-1, 0);
			if (KeyboardManager.IsKeyPressed(Keys.D)) movement += new Vector2(1, 0);

			//if (KeyboardManager.IsKeyPressed(Keys.Q)) this.ShootDirection = ShootDirection.AddDegrees(-180 * gameTime.ElapsedGameTime.TotalSeconds);
			//if (KeyboardManager.IsKeyPressed(Keys.E)) this.ShootDirection = ShootDirection.AddDegrees(180 * gameTime.ElapsedGameTime.TotalSeconds);
			//if (KeyboardManager.IsKeyPressed(Keys.R)) this.ShootDirection = Vector2.UnitX;

			// normalizing movement
			if (movement.LengthSquared() > 0f)
			{
				movement.Normalize();
				movement *= new Vector2(1.0f, 1.1f);

				// now normalize it for uncapped frames
				movement *= 60.0f; // we aimed for 60 fps
				movement *= (float) gameTime.ElapsedGameTime.TotalSeconds;

				Position += movement * (Shot > 0 ? 2.75f : 4.25f);
			}


			// Prevents player from leaving the screen
			var halfTextureWidth = Texture.Width / 2;
			var halfTextureHeight = Texture.Height / 2;

			if (Position.X < halfTextureWidth + PlayArea.Location.X) Position.X = halfTextureWidth + +PlayArea.Location.X;
			if (Position.Y < halfTextureHeight + PlayArea.Location.Y) Position.Y = halfTextureHeight + PlayArea.Location.Y;

			if (Position.X + halfTextureWidth > PlayArea.Size.X) Position.X = PlayArea.Size.X - halfTextureWidth;
			if (Position.Y + halfTextureHeight > PlayArea.Size.Y) Position.Y = PlayArea.Size.Y - halfTextureHeight;


			if (Health >= MaxHealth) Health = MaxHealth;

			if (Health <= 0) SceneManager.CloseScene(new TestTransition(1.0d));

			//DirectionMarker.Update(gameTime);
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - Position).LengthSquared() < 16;
		}

		public bool DoesCollide(AbstractSprite sprite)
		{
			return (sprite.Position - Position).Length() < (sprite.TextureSize.X * 0.5f + 4);
		}

		public void Damage(int amount)
		{
			if (Invulnerable > 0) return;
			Health -= amount;
			Invulnerable = 0.25f;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.Draw(ImageManager.GetTexture2D("dot"), Position - new Vector2(4), Color.White);

			//this.DirectionMarker.Draw(spriteBatch);
		}
	}

	internal class BannerSprite : ImageSprite
	{
		private readonly PlayerSprite Player;

		public BannerSprite(PlayerSprite player, Vector2 position) : base("banner2", new Vector2())
		{
			Player = player;

			Persistence = true;

			Position = position;
			LayerDepth = 10;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Hue);

			spriteBatch.DrawString(Game.FontArial, $"Health: {Player.Health}", new Vector2(30, 30), Color.Black);
		}
	}

	internal class ViginetteSprite : ImageSprite
	{
		private readonly PlayerSprite Player;

		public ViginetteSprite(PlayerSprite player) : base("viginette", new Vector2())
		{
			Player = player;

			Persistence = true;
			LayerDepth = 11;

			Hue = Color.Transparent;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, new Rectangle(0, 0, Game.RenderSize.X, Game.RenderSize.Y), null, Hue);
		}

		public override void Update(GameTime gameTime)
		{
			var k = (Player.MaxHealth - Player.Health) / (float) Player.MaxHealth;
			var c = new Color((int) (255f * k), 0, 0, (int) (64f * k));

			Hue = c;
		}
	}

	public class PlayerOrb : LinearOrb
	{
		//private static readonly Vector2 Direction = new Vector2(0, -1);
		private const float SPEED = 7.5f;


		public PlayerOrb(Vector2 position, Vector2 direction) : base(position, direction, SPEED)
		{
			Hue = Color.Purple;
		}

		public override void Update(GameTime gameTime)
		{
			Position += UpdatePosition(gameTime) * 60.0f * (float) gameTime.ElapsedGameTime.TotalSeconds;

			var sprites = SceneManager.GetAllSprites();

			foreach (var t in sprites.Keys)
			foreach (AbstractSprite sprite in sprites[t])
			{
				if (!sprite.Enemy) continue;

				if (sprite.DoesCollide(Position))
				{
					sprite.Health -= 1;
					Delete();
				}
			}
		}
	}

	//internal class DirectionMarker : AbstractSprite
	//{
	//	private readonly PlayerSprite Player;

	//	internal DirectionMarker(PlayerSprite player) : base("orb3")
	//	{
	//		this.Player = player;
	//	}

	//	public override void Update(GameTime gameTime)
	//	{
	//		Vector2 FloatToVector2(float degrees) => new Vector2((float)Math.Cos(degrees), (float)Math.Sin(degrees));
	//		this.Rotation = (float) Player.ShootDirection.ToAngle();
	//		this.Position = Player.GetPosition() + FloatToVector2(this.Rotation) * 7.5f;
	//	}
	//}
}