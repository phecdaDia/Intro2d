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
		/// Timeout for holding down a button
		/// </summary>
		private const int TIMEOUT_DELAY = 20;

		/// <summary>
		/// Font used to autogenerate the textures
		/// </summary>
		private const string FONT_NAME = "example";

		/// <summary>
		/// Current timeout.
		/// </summary>
		private int Timeout = 0;

		/// <summary>
		/// The currently selected index
		/// </summary>
		private int SelectedIndex;

		/// <summary>
		/// Index of the first displayed item
		/// </summary>
		private int UpShowIndex;

		/// <summary>
		/// Creates the main menu and initialized all fields
		/// </summary>
		public MainMenuSprite()
		{

			var x = 70;
			var y = 85;
			var spacing = 45;

			// Adding all entries to the list
			MenuEntries = new List<MainMenuEntry>
			{
				// Entry for the tutorial
				new MainMenuEntry("Tutorial", FONT_NAME, "tutorial", new Vector2(x, y += spacing)),

				//
				new MainMenuEntry("TODO", FONT_NAME, "round1", new Vector2(x, y += spacing)),

				//
				new MainMenuEntry("TODO Round 1", FONT_NAME, "mainmenu", new Vector2(x, y += spacing)),

				//
				new MainMenuEntry("TODO Round 2", FONT_NAME, "mainmenu", new Vector2(x, y += spacing)),

				//
				new MainMenuEntry("TODO Finals", FONT_NAME, "mainmenu", new Vector2(x, y += spacing)),

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
			};
		}

		public override void Update(GameTime gameTime)
		{

			MenuEntries[SelectedIndex].Update(gameTime);

			// it works, don't touch this.

			Timeout -= Timeout > 0 ? 1 : 0;

			// Getting index down
			if (KeyboardManager.IsKeyDown(Keys.W) || KeyboardManager.IsKeyDown(Keys.Up) || Timeout == 0 && (KeyboardManager.IsKeyPressed(Keys.W) || KeyboardManager.IsKeyPressed(Keys.Up)))
			{
				SelectedIndex--;
				if (UpShowIndex > 0) UpShowIndex--;
				if (SelectedIndex < 0)
				{
					UpShowIndex = MenuEntries.Count - MAX_MENU_ENTRIES;
					if (UpShowIndex < 0) UpShowIndex = 0;
					SelectedIndex += MenuEntries.Count;
				}

				Timeout = TIMEOUT_DELAY;
			}

			// Getting the index up
			if (KeyboardManager.IsKeyDown(Keys.S) || KeyboardManager.IsKeyDown(Keys.Down) || Timeout == 0 && (KeyboardManager.IsKeyPressed(Keys.S) || KeyboardManager.IsKeyPressed(Keys.Down)))
			{
				SelectedIndex++;
				if (SelectedIndex >= MenuEntries.Count)
				{
					SelectedIndex = 0;
					UpShowIndex = 0;
				}

				if (SelectedIndex >= UpShowIndex + MAX_MENU_ENTRIES)
					UpShowIndex++;

				Timeout = TIMEOUT_DELAY;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			//spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"),
			//	new Vector2(50, 85 + (SelectedIndex - UpShowIndex) * 50), Color.White);

			var d = MenuEntries.Count > MAX_MENU_ENTRIES ? MAX_MENU_ENTRIES : MenuEntries.Count;
			
			if (MenuEntries.Count <= MAX_MENU_ENTRIES)
			{
				int z = 0;
				foreach (var menuItem in MenuEntries)
				{
					if (z == SelectedIndex - UpShowIndex)
					{
						spriteBatch.Draw(
							ImageManager.GetTexture2D("MenuItem/arrow"),
							menuItem.GetPosition() - new Vector2(50, 0),
							Color.White
						);
					}
					menuItem.Draw(spriteBatch);
					z++;
				}
			}
			else
			{ 
				for (var i = 0; i < d; i++)
				{
					if (i == SelectedIndex - UpShowIndex)
					{
						spriteBatch.Draw(
							ImageManager.GetTexture2D("MenuItem/arrow"),
							MenuEntries[i + UpShowIndex].GetPosition() - new Vector2(50, 0),
							Color.White
						);
					}
					MenuEntries[i + UpShowIndex].Draw(spriteBatch);
				}
			}
			//spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);

			spriteBatch.DrawString(Game.FontArial, $"{this.Timeout} - {this.SelectedIndex}", new Vector2(1), Color.Black);
		}
	}

	internal class MainMenuEntry : AbstractSprite
	{
		public readonly string Text, Font, SceneKey;

		public MainMenuEntry(string text, string font, string sceneKey, Vector2 position)
		{
			this.Text = text;
			this.Font = font;
			this.SceneKey = sceneKey;
			this.Position = position;
		}

		public override void Update(GameTime gameTime)
		{
			if (KeyboardManager.IsKeyDown(Keys.Enter))
			{
				SceneManager.AddScene(this.SceneKey, new TestTransition(1000));
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			FontManager.DrawString(spriteBatch, Font, Position, Text);
		}
	}

	internal class LambdaMainMenuEntry : MainMenuEntry
	{
		public readonly Action Lambda;

		public LambdaMainMenuEntry(string text, string font, Action lambda, Vector2 position) : base(text, font, "", position)
		{
			this.Lambda = lambda;
		}
		
		public override void Update(GameTime gameTime)
		{
			if (KeyboardManager.IsKeyDown(Keys.Enter))
			{
				this.Lambda.Invoke();
			}
		}
	}
}