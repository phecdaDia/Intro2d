using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Fonts
{
	public class FontManager
	{
		private static FontManager fontManager;

		private static ContentManager content;

		private Dictionary<String, CustomFont> fonts;

		private FontManager()
		{
			fontManager = this;

			this.fonts = new Dictionary<string, CustomFont>();

			new ExampleFont();
		}

		public static void SetContentManager(ContentManager contentManager)
		{
			content = contentManager;
		}

		public static FontManager GetInstance()
		{
			if (content == null) return null;
			if (fontManager == null) new FontManager();

			return fontManager;
		}

		public void RegisterFont(String key, CustomFont font)
		{
			this.fonts.Add(key, font);
		}

		public static Texture2D CreateFontString(String font, params String[] text)
		{
			if (!GetInstance().fonts.ContainsKey(font)) return null;

			CustomFont cf = GetInstance().fonts[font];
			return cf.CreateTexture(text);
		}
	}
}
