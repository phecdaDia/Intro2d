using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using Intro2DGame.Game.Fonts;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

	internal class MainMenuSprite : AbstractSprite
	{
		private const int MAX_MENU_ENTRIES = 10;

		private readonly List<MainMenuEntry> MenuEntries;

		private const int TIMEOUT_DELAY = 20;

        private const string FONT_NAME = "example";

		private int Timeout = 0;

		// the current selected Index. 
		private int SelectedIndex;

		private int UpShowIndex;

		public MainMenuSprite()
		{
			SelectedIndex = 0;
			UpShowIndex = 0;

            MenuEntries = new List<MainMenuEntry>
            {

                new MainMenuEntry("TEST0", FONT_NAME, "example"),
                new MainMenuEntry("A very very very very very long name", FONT_NAME, "example"),
                new MainMenuEntry("abcdefghijklmnopqrstuvwxyz", FONT_NAME, "example"),
                new MainMenuEntry("0123456789", FONT_NAME, "example"),
                new MainMenuEntry("TEST4", FONT_NAME, "example"),
                new MainMenuEntry("TEST5", FONT_NAME, "example"),
                new MainMenuEntry("TEST6", FONT_NAME, "example"),
                new MainMenuEntry("TEST7", FONT_NAME, "example"),
                new MainMenuEntry("TEST8", FONT_NAME, "example"),
                new MainMenuEntry("TEST9", FONT_NAME, "example"),
            //FontManager.CreateFontString("example", "Introductions"),
            //FontManager.CreateFontString("example", "Knockout Round"),
            //FontManager.CreateFontString("example", "Round 1"),
            //FontManager.CreateFontString("example", "Round 2"),
            //FontManager.CreateFontString("example", "Finals"),
            //FontManager.CreateFontString("example", "DIALOG TEST"),
            //FontManager.CreateFontString("example", "Go to Example Scene!"),
            //FontManager.CreateFontString("example", "Go to Example Scene2!"),
            //FontManager.CreateFontString("example", "Exit")
            };
		}

		public override void Update(GameTime gameTime)
		{

			if (KeyboardManager.IsKeyDown(Keys.Enter) || KeyboardManager.IsKeyDown(Keys.Space))
			{
                SceneManager.AddScene(MenuEntries[SelectedIndex].SceneKey);

				return;
			}

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

			spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"),
				new Vector2(50, 85 + (SelectedIndex - UpShowIndex) * 50), Color.White);

			var d = MenuEntries.Count > MAX_MENU_ENTRIES ? MAX_MENU_ENTRIES : MenuEntries.Count;

			var idx = 0;
			if (MenuEntries.Count <= MAX_MENU_ENTRIES)
				foreach (var menuItem in MenuEntries)
					spriteBatch.Draw(menuItem.Text, new Vector2(100, 85 + idx++ * 50), Color.White);
			else
				for (var i = 0; i < d; i++)
					spriteBatch.Draw(MenuEntries[i + UpShowIndex].Text, new Vector2(100, 85 + idx++ * 50), Color.White);
			//spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
			
			spriteBatch.DrawString(Game.FontArial, $"{this.Timeout}", new Vector2(1), Color.Black);
		}
	}

    internal struct MainMenuEntry
    {
        public Texture2D Text;
        public string SceneKey;

        public MainMenuEntry(string text, string font, string sceneKey)
        {
            this.Text = FontManager.CreateFontString(font, text);
            this.SceneKey = sceneKey;
        }
    }
}