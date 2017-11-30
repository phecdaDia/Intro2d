using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using Intro2DGame.Game.Fonts;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Intro2DGame.Game.Scenes.Transition;

namespace Intro2DGame.Game.Scenes
{
	/// <summary>
	/// Scene containing the main menu when the game is startet.
	/// </summary>
	public class MainMenuScene : Scene
	{
		public MainMenuScene() : base("mainmenu")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new MainMenuSprite());
			AddSprite(new ImageSprite("title", new Vector2(400, 40)));
			AddSprite(new MainMenuPlayer());
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (SceneManager.GetCurrentScene().SceneKey != this.SceneKey) return;
				base.Draw(spriteBatch);
		}
	}

	/// <summary>
	/// Main menu control sprite
	/// Controls the logic of the main menu
	/// </summary>
	internal class MainMenuSprite : AbstractSprite
	{
		/// <summary>
		/// Number of menu entries to be displayed at the same time
		/// </summary>
		private const int MAX_MENU_ENTRIES = 10;

		/// <summary>
		/// List of menu entries
		/// </summary>
		private readonly List<MainMenuEntry> MenuEntries;

		/// <summary>
		/// Font used to autogenerate the textures
		/// </summary>
		private const string FONT_NAME = "example";

		/// <summary>
		/// Creates the main menu and initialized all fields
		/// </summary>
		public MainMenuSprite()
		{

			var x = 500;
			var y = 85;
			var spacing = 45;

			// Adding all entries to the list
			MenuEntries = new List<MainMenuEntry>
			{

				//
				new MainMenuEntry("Laser guy Round", FONT_NAME, "laserguy", new Vector2(x, y += spacing)),

				//
				new MainMenuEntry("Second Round", FONT_NAME, "mainmenu", new Vector2(x, y += spacing)),

				//
				new MainMenuEntry("Final Round", FONT_NAME, "mainmenu", new Vector2(x, y += spacing)),

				// entry for the first debug scene
				new MainMenuEntry("example fight HARD", FONT_NAME, "example", new Vector2(x, y += spacing)),

				// entry for the second debug scene
				new MainMenuEntry("example fight 2", FONT_NAME, "example2", new Vector2(x, y += spacing)),
				
				// Lambda expression for dialog generation
				new LambdaMainMenuEntry("dialog test", FONT_NAME, () => {
					var random = new Random();
					for (var i = 0; i < 10; i++)
					{
						SceneManager.AddScene(new DialogScene($"Example Dialog Box #{i}\r\n{random.Next(0x7fffffff):X08}-{random.Next(0x7fffffff):X08}-{random.Next(0x7fffffff):X08}-{random.Next(0x7fffffff):X08}"));
					}
				}, new Vector2(x, y += spacing)),

				// difficulty
				new DifficultyMainMenuEntry("Difficulty: Normal", FONT_NAME, new Vector2(x, y += spacing)),
			};
		}

		public override void Update(GameTime gameTime)
		{

			var playerOrbs = SceneManager.GetSprites<PlayerOrb>();

			foreach (var playerOrb in playerOrbs)
			{
				foreach (var menuEntry in MenuEntries)
				{
					if (!(playerOrb.GetPosition().X >= menuEntry.GetPosition().X) || playerOrb.IsDeleted()) continue;

					if (!(playerOrb.GetPosition().Y >= menuEntry.GetPosition().Y) ||
					    !(playerOrb.GetPosition().Y <= menuEntry.GetPosition().Y + 32)) continue;

					foreach (var po in playerOrbs) po.Delete();

					menuEntry.Update(gameTime);
					return;
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			var d = MenuEntries.Count > MAX_MENU_ENTRIES ? MAX_MENU_ENTRIES : MenuEntries.Count;
			
			if (MenuEntries.Count <= MAX_MENU_ENTRIES)
			{
				foreach (var menuItem in MenuEntries)
				{
					menuItem.Draw(spriteBatch);
				}
			}
		}
	}

	internal class MainMenuEntry : AbstractSprite
	{
		protected string Text;
		protected readonly string Font;
		protected readonly string SceneKey;

		public MainMenuEntry(string text, string font, string sceneKey, Vector2 position)
		{
			this.Text = text;
			this.Font = font;
			this.SceneKey = sceneKey;
			this.Position = position;
		}

		public override void Update(GameTime gameTime)
		{
			SceneManager.AddScene(this.SceneKey, new TestTransition(1000));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			FontManager.DrawString(spriteBatch, Font, Position, Text);
		}
	}

	internal class LambdaMainMenuEntry : MainMenuEntry
	{
		private readonly Action Lambda;

		public LambdaMainMenuEntry(string text, string font, Action lambda, Vector2 position) : base(text, font, "", position)
		{
			this.Lambda = lambda;
		}

		public override void Update(GameTime gameTime)
		{
			this.Lambda.Invoke();
		}
	}

	internal class DifficultyMainMenuEntry : MainMenuEntry
	{

		public DifficultyMainMenuEntry(string text, string font, Vector2 position) : base(text, font, "", position)
		{
		}

		public override void Update(GameTime gameTime)
		{
			switch (GameConstants.Difficulty)
			{
				case Difficulty.Easy:
					GameConstants.Difficulty = Difficulty.Normal;
					break;
				case Difficulty.Normal:
					GameConstants.Difficulty = Difficulty.Difficult;
					break;
				case Difficulty.Difficult:
					GameConstants.Difficulty = Difficulty.Lunatic;
					break;
				case Difficulty.Lunatic:
					GameConstants.Difficulty = Difficulty.Easy;
					break;
			}

			this.Text = $"Difficulty: {GameConstants.Difficulty.ToString()}";
		}
	}

	internal class MainMenuPlayer : AbstractAnimatedSprite
	{

		//public MainMenuPlayer(string key, Vector2 position, Point size, int delay) : base(key, position, size, delay)
		//{
		//}

		private const int SHOOT_DELAY = 7;
		private int ShootDelay;
		
		private int Shot;

		private Rectangle PlayerArea;

		// Temporary texture for debug
		private Texture2D temp;

		private Boolean IsShootingEnabled = false;

		public MainMenuPlayer() : base("player", new Vector2())
		{

			this.PlayerArea = new Rectangle(50, 100, 350, 500);

			this.Position = new Vector2(PlayerArea.X + PlayerArea.Width / 2, PlayerArea.Y + PlayerArea.Height / 2);
		}


		protected override void AddFrames()
		{
			throw new NotImplementedException();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var movement = new Vector2();


			// Shoots bullets

			var ms = Mouse.GetState();
			Shot -= Shot > 0 ? 1 : 0;

			if (ms.LeftButton == ButtonState.Released) IsShootingEnabled = true;

			if (ShootDelay-- <= 0 && IsShootingEnabled)
			{
				if (KeyboardManager.IsKeyPressed(Keys.Space) || ms.LeftButton == ButtonState.Pressed)

				{
					SpawnSprite(new PlayerOrb(GetPosition(), Position + new Vector2(1, 0)));
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

			movement *= new Vector2(1.0f, 1.1f);
			Position += movement * (Shot > 0 ? 2.75f : 4.25f);

			
			// Prevents player from leaving the screen
			var halfTextureWidth = this.Texture.Width / 2;
			var halfTextureHeight = this.Texture.Height / 2;

			if (Position.X - halfTextureWidth < PlayerArea.Location.X) Position.X = PlayerArea.Location.X + halfTextureWidth;
			if (Position.Y - halfTextureHeight < PlayerArea.Location.Y) Position.Y = PlayerArea.Location.Y + halfTextureHeight;

			if (Position.X + halfTextureWidth > PlayerArea.Location.X + PlayerArea.Width) Position.X = PlayerArea.Location.X + PlayerArea.Width - halfTextureWidth;
			if (Position.Y + halfTextureHeight > PlayerArea.Location.Y + PlayerArea.Height) Position.Y = PlayerArea.Location.Y + PlayerArea.Height - halfTextureHeight;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(temp, PlayerArea, Color.White);
			base.Draw(spriteBatch);

		}

		public override void LoadContent()
		{
			temp = new Texture2D(Game.GetInstance().GraphicsDevice, 1, 1);
			temp.SetData(new [] { new Color(0x20202020u), });
		}

		public override void UnloadContent()
		{
			temp.Dispose();
		}
	}
}