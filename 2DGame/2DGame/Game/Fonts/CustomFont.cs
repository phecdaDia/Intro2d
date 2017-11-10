using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Fonts
{
	public abstract class CustomFont
	{
		protected readonly Dictionary<char, Point> SymbolPosition; // We'll see if we should change that later.
		private readonly string TextureKey;

		private readonly int TotalPixelAmount;
		private Rectangle Rectangle;
		private Vector2 SymbolSize;

		public CustomFont(string fontName, string textureKey, Vector2 symbolSize)
		{
			if (fontName == null) throw new ArgumentNullException(nameof(fontName));

			TextureKey = textureKey ?? throw new ArgumentNullException(nameof(textureKey));
			SymbolSize = symbolSize;

			SymbolPosition = new Dictionary<char, Point>();

			TotalPixelAmount = (int) (symbolSize.X * symbolSize.Y);
			Rectangle = new Rectangle(0, 0, (int) symbolSize.X, (int) symbolSize.Y);

			FontManager.GetInstance().RegisterFont(fontName, this);

			SetSymbolPositions();
		}

		protected abstract bool IsIgnoreCase();
		protected abstract void SetSymbolPositions();

		public Texture2D CreateTexture(params string[] lines)
		{
			// Going through the lines to see how long the longest line is..
			var maxLength = 0;
			foreach (var s in lines)
			{
				var l = s.Length;
				if (l > maxLength) maxLength = l;
			}

			var result = new Texture2D(Game.GetInstance().GetGraphicsDeviceManager().GraphicsDevice,
				(int) SymbolSize.X * maxLength, (int) SymbolSize.Y * lines.Length);

			var line = 0;
			foreach (var s in lines)
			{
				var character = 0;
				foreach (var c in IsIgnoreCase() ? s.ToLower() : s)
				{
					if (SymbolPosition.ContainsKey(c))
					{
						var colorData = new Color[TotalPixelAmount];
						// move the rectangle
						Rectangle.Location = SymbolPosition[c];

						ImageManager.GetTexture2D(TextureKey).GetData(0, Rectangle, colorData, 0, TotalPixelAmount);

						Rectangle.Location = new Point(character * (int) SymbolSize.X, line * (int) SymbolSize.Y);
						result.SetData(0, Rectangle, colorData, 0, TotalPixelAmount);
					}
					else
					{
						var colorData = new Color[TotalPixelAmount];
						Rectangle.Location = new Point(character * (int) SymbolSize.X, line * (int) SymbolSize.Y);
						result.SetData(0, Rectangle, colorData, 0, TotalPixelAmount);
					}

					character++;
				}
				line++;
			}

			return result;
		}
	}
}