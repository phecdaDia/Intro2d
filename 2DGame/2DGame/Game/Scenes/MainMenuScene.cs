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
			AddSprite(new PlayerSprite(new Vector2(35, 200), false));
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
				new MainMenuEntry("Second Round", FONT_NAME, "starguy", new Vector2(x, y += spacing)),

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
					if (!(playerOrb.Position.X >= menuEntry.Position.X) || playerOrb.IsDeleted()) continue;

					if (!(playerOrb.Position.Y >= menuEntry.Position.Y) ||
					    !(playerOrb.Position.Y <= menuEntry.Position.Y + 32)) continue;

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
		private readonly string Font;
		private readonly string SceneKey;

		public MainMenuEntry(string text, string font, string sceneKey, Vector2 position)
		{
			this.Text = text;
			this.Font = font;
			this.SceneKey = sceneKey;
			this.Position = position;
		}

		public override void Update(GameTime gameTime)
		{
			SceneManager.AddScene(this.SceneKey, new TestTransition(1.0d));
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
}