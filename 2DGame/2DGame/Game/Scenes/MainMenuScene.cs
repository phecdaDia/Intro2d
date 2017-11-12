﻿using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Intro2DGame.Game.Fonts;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
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

	public class MainMenuSprite : AbstractSprite
	{
		private const int MAX_MENU_ENTRIES = 10;

		private readonly List<Texture2D> MenuEntries;

		private readonly int TimeoutDelay = 60;

		// the current selected Index. 
		private int SelectedIndex;

		private int UpShowIndex;

		public MainMenuSprite()
		{
			SelectedIndex = 0;
			UpShowIndex = 0;

			MenuEntries = new List<Texture2D>
			{
				FontManager.CreateFontString("example", "Introductions"),
				FontManager.CreateFontString("example", "Knockout Round"),
				FontManager.CreateFontString("example", "Round 1"),
				FontManager.CreateFontString("example", "Round 2"),
				FontManager.CreateFontString("example", "Finals"),
				FontManager.CreateFontString("example", "Go to Scene #6"),
				FontManager.CreateFontString("example", "Go to Example Scene!"),
				FontManager.CreateFontString("example", "Exit")
			};
		}

		public override void Update(GameTime gameTime)
		{

			if (KeyboardManager.IsKeyDown(Keys.Enter))
			{
				switch (SelectedIndex)
				{
					case 0:
						SceneManager.AddScene("tutorial");
						break;
					case 1:
						SceneManager.AddScene("mainmenu");
						break;
					case 2:
						SceneManager.AddScene("mainmenu");
						break;
					case 3:
						SceneManager.AddScene("mainmenu");
						break;
					case 4:
						SceneManager.AddScene("mainmenu");
						break;
					case 5:
						// Creating some dialog boxes
						for (var i = 0; i < 10; i++)
						{
							SceneManager.AddScene(new DialogScene($"Example Dialog Box #{i}"));
						}
						break;
					case 6:
						SceneManager.AddScene("example");
						break;
					case 7:
						Game.ExitGame();
						break;
					default:
						Game.ExitGame();
						break;
				}
				return;
			}

			// it works, don't touch this.

			// Getting index down
			if (KeyboardManager.IsKeyDown(Keys.W) || KeyboardManager.IsKeyDown(Keys.Up))
			{
				SelectedIndex--;
				if (UpShowIndex > 0) UpShowIndex--;
				if (SelectedIndex < 0)
				{
					UpShowIndex = MenuEntries.Count - MAX_MENU_ENTRIES;
					if (UpShowIndex < 0) UpShowIndex = 0;
					SelectedIndex += MenuEntries.Count;
				}
			}

			// Getting the index up
			if (KeyboardManager.IsKeyDown(Keys.S) || KeyboardManager.IsKeyDown(Keys.Down))
			{
				SelectedIndex++;
				if (SelectedIndex >= MenuEntries.Count)
				{
					SelectedIndex = 0;
					UpShowIndex = 0;
				}

				if (SelectedIndex >= UpShowIndex + MAX_MENU_ENTRIES)
					UpShowIndex++;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"),
				new Vector2(50, 85 + (SelectedIndex - UpShowIndex) * 50), Color.White);

			var d = MenuEntries.Count > MAX_MENU_ENTRIES ? MAX_MENU_ENTRIES : MenuEntries.Count;

			var idx = 0;
			if (MenuEntries.Count <= MAX_MENU_ENTRIES)
				foreach (var menuItem in MenuEntries)
					spriteBatch.Draw(menuItem, new Vector2(100, 85 + idx++ * 50), Color.White);
			else
				for (var i = 0; i < d; i++)
					spriteBatch.Draw(MenuEntries[i + UpShowIndex], new Vector2(100, 85 + idx++ * 50), Color.White);
			//spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
		}
	}
}