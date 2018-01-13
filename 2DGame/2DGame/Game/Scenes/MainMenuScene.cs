using System;
using System.Collections.Generic;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Scenes
{
	/// <summary>
	///     Scene containing the main menu when the game is startet.
	/// </summary>
	public class MainMenuScene : Scene
	{
		public MainMenuScene() : base("mainmenu")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new MainMenuSprite());
			AddSprite(new ImageSprite("title", new Vector2(350, 40)));
			AddSprite(new PlayerSprite(new Vector2(35, 200), true));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (SceneManager.GetCurrentScene().SceneKey != SceneKey) return;
			base.Draw(spriteBatch);
		}
	}

	/// <summary>
	///     Main menu control sprite
	///     Controls the logic of the main menu
	/// </summary>
	internal class MainMenuSprite : AbstractSprite
	{
		/// <summary>
		///     Number of menu entries to be displayed at the same time
		/// </summary>
		private const int MAX_MENU_ENTRIES = 10;

		/// <summary>
		///     Font used to autogenerate the textures
		/// </summary>
		private const string FONT_NAME = "example";

		/// <summary>
		///     List of menu entries
		/// </summary>
		private readonly List<MainMenuEntry> MenuEntries;

		/// <summary>
		///     Creates the main menu and initialized all fields
		/// </summary>
		public MainMenuSprite()
		{
			var x = 200;
			var y = 85;
			var spacing = 45;

			// Adding all entries to the list
			MenuEntries = new List<MainMenuEntry>
			{
				//
				new MainMenuEntry("Enter the arena", FONT_NAME, "laserguy", new Vector2(x, y += spacing)),

				//// Lambda expression for dialog generation
				//new LambdaMainMenuEntry("dialog test", FONT_NAME, () =>
				//{
				//	var random = new Random();
				//	for (var i = 0; i < 10; i++)
				//		SceneManager.AddScene(new DialogScene(
				//			$"Example Dialog Box #{i}\r\n{random.Next(0x7fffffff):X08}-{random.Next(0x7fffffff):X08}-{random.Next(0x7fffffff):X08}-{random.Next(0x7fffffff):X08}"));
				//}, new Vector2(x, y += spacing)),

				
				// difficulty
				new DifficultyMainMenuEntry("Difficulty: Normal", FONT_NAME, new Vector2(x, y += spacing)),

				new MainMenuEntry("Credits", FONT_NAME, "mainmenu", new Vector2(x, y += 2 * spacing)),

				new MainMenuEntry("[Move with WASD]", FONT_NAME, "mainmenu", new Vector2(x, y += 2 * spacing)),
				new MainMenuEntry("[Shoot with SPACE]", FONT_NAME, "mainmenu", new Vector2(x, y += spacing)),
				new MainMenuEntry("", FONT_NAME, "example", new Vector2(x, y += spacing)), // easteregg?
			};
		}

		public override void Update(GameTime gameTime)
		{
			var playerOrbs = SceneManager.GetSprites<PlayerOrb>();

			foreach (var playerOrb in playerOrbs)
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

		public override void Draw(SpriteBatch spriteBatch)
		{
			var d = MenuEntries.Count > MAX_MENU_ENTRIES ? MAX_MENU_ENTRIES : MenuEntries.Count;

			if (MenuEntries.Count <= MAX_MENU_ENTRIES)
				foreach (var menuItem in MenuEntries)
					menuItem.Draw(spriteBatch);
		}
	}

	internal class MainMenuEntry : AbstractSprite
	{
		private readonly string Font;
		private readonly string SceneKey;
		protected string Text;

		public MainMenuEntry(string text, string font, string sceneKey, Vector2 position)
		{
			Text = text;
			Font = font;
			SceneKey = sceneKey;
			Position = position;
		}

		public override void Update(GameTime gameTime)
		{
			SceneManager.AddScene(SceneKey, new TestTransition(1.0d));
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
			Lambda = lambda;
		}

		public override void Update(GameTime gameTime)
		{
			Lambda.Invoke();
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

			Text = $"Difficulty: {GameConstants.Difficulty.ToString()}";
		}
	}
}