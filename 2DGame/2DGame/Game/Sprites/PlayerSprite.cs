using System;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites.Orbs;
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

		private Vector2 ShootDirection = new Vector2(1, 0);

		private Rectangle PlayArea = new Rectangle(0, 100, Game.RenderSize.X, Game.RenderSize.Y - 100);


		//private readonly DirectionMarker DirectionMarker;

		public PlayerSprite(Vector2 position, bool addChilds = true) : base("player", position)
		{
			LayerDepth = 0;

			if (addChilds)
			{
				SceneManager.GetCurrentScene().AddSprite(new BannerSprite(this)); // This adds the banner
				SceneManager.GetCurrentScene().AddSprite(new BannerSprite(this, 620)); // This adds the banner
				SceneManager.GetCurrentScene().AddSprite(new ViginetteSprite(this)); // This adds the banner
			}

			//this.DirectionMarker = new DirectionMarker(this);

			MaxHealth = 1000;
			Health = 1000;


			this.Persistence = true;
		}
		
		public override void Update(GameTime gameTime)
		{

			if (Game.GameArguments.IsCheatsEnabled && KeyboardManager.IsKeyPressed(Keys.F4))
			{
				Health = MaxHealth;
				Invulnerable = 2;
			}


			var movement = new Vector2();


			// Shoots bullets

			var ms = Mouse.GetState();
			Shot -= Shot > 0 ? 1 : 0;
			ShootDelay -= ShootDelay > 0 ? 1 : 0;


			if (ShootDelay <= 0 || KeyboardManager.IsKeyDown(Keys.Space))
			{
				if (KeyboardManager.IsKeyPressed(Keys.Space) || ms.LeftButton == ButtonState.Pressed)

				{
					SpawnSprite(new PlayerOrb(this.Position, ShootDirection));
					ShootDelay = SHOOT_DELAY;

					Shot = SHOOT_DELAY + 5;
				}
			}

			// buffering movement
			if (KeyboardManager.IsKeyPressed(Keys.W)) movement += new Vector2(0, -1);
			if (KeyboardManager.IsKeyPressed(Keys.S)) movement += new Vector2(0, 1);
			if (KeyboardManager.IsKeyPressed(Keys.A)) movement += new Vector2(-1, 0);
			if (KeyboardManager.IsKeyPressed(Keys.D)) movement += new Vector2(1, 0);

			if (KeyboardManager.IsKeyPressed(Keys.Q)) this.ShootDirection = ShootDirection.AddDegrees(-180 * gameTime.ElapsedGameTime.TotalSeconds);
			//if (KeyboardManager.IsKeyPressed(Keys.E)) this.ShootDirection = ShootDirection.AddDegrees(180 * gameTime.ElapsedGameTime.TotalSeconds);
			//if (KeyboardManager.IsKeyPressed(Keys.R)) this.ShootDirection = Vector2.UnitX;

			// normalizing movement
			if (movement.LengthSquared() > 0f) movement.Normalize();

			movement *= new Vector2(1.0f, 1.1f);
			Position += movement * (Shot > 0 ? 2.75f : 4.25f);

			// Prevents player from leaving the screen
			var halfTextureWidth = this.Texture.Width / 2;
			var halfTextureHeight = this.Texture.Height / 2;

			if (Position.X < halfTextureWidth + PlayArea.Location.X) Position.X = halfTextureWidth + +PlayArea.Location.X;
			if (Position.Y < halfTextureHeight + PlayArea.Location.Y) Position.Y = halfTextureHeight + PlayArea.Location.Y;

			if (Position.X + halfTextureWidth > PlayArea.Size.X) Position.X = PlayArea.Size.X - halfTextureWidth;
			if (Position.Y + halfTextureHeight > PlayArea.Size.Y) Position.Y = PlayArea.Size.Y - halfTextureHeight;



			if (Invulnerable > 0) Invulnerable--;
			

			if (Health >= MaxHealth) Health = MaxHealth;

			if (Health <= 0) SceneManager.CloseScene(new TestTransition(1000));

			//DirectionMarker.Update(gameTime);

		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - this.Position).LengthSquared() < 16;
		}

		public bool DoesCollide(AbstractSprite sprite)
		{
			return (sprite.Position - this.Position).LengthSquared() < (16 + sprite.TextureSize.LengthSquared() * 0.5f);
		}

		public void Damage(int amount)
		{
			if (Invulnerable > 0) return;
			this.Health -= amount;
			this.Invulnerable = 15;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.Draw(ImageManager.GetTexture2D("dot"), this.Position - new Vector2(4), Color.White);

			//this.DirectionMarker.Draw(spriteBatch);

		}
	}

	internal class BannerSprite : ImageSprite
	{
		private readonly PlayerSprite Player;

		public BannerSprite(PlayerSprite player) : base("banner", new Vector2())
		{
			this.Player = player;

			Persistence = true;
			
			this.Position = new Vector2(0, 0);
			LayerDepth = 10;
		}

		public BannerSprite(PlayerSprite player, int w) : this(player)
		{
			this.Position = new Vector2(0, w);
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
			this.Player = player;

			Persistence = true;
			LayerDepth = 11;

			this.Hue = Color.Transparent;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.Texture, new Rectangle(0, 0, Game.RenderSize.X, Game.RenderSize.Y), null, this.Hue);
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


		public PlayerOrb(Vector2 position, Vector2 direction) : base(position, direction, SPEED)
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

				if (sprite.DoesCollide(this.Position)) {
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