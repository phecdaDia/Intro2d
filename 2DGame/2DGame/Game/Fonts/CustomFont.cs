using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Fonts
{
	public abstract class CustomFont
	{
		private readonly string TextureKey;
		private Vector2 SymbolSize;

		protected Dictionary<char, Point> SymbolPosition; // We'll see if we should change that later.

		private readonly int TotalPixelAmount;
		private Rectangle Rectangle;

		public CustomFont(string fontName, string textureKey, Vector2 symbolSize)
		{
			if (fontName == null) throw new ArgumentNullException(nameof(fontName));

			this.TextureKey = textureKey ?? throw new ArgumentNullException(nameof(textureKey));
			this.SymbolSize = symbolSize;

			this.SymbolPosition = new Dictionary<char, Point>();

			this.TotalPixelAmount = (int)(symbolSize.X * symbolSize.Y);
			this.Rectangle = new Rectangle(0, 0, (int)symbolSize.X, (int)symbolSize.Y);

			FontManager.GetInstance().RegisterFont(fontName, this);

			SetSymbolPositions();
		}

		protected abstract bool IsIgnoreCase();
		protected abstract void SetSymbolPositions();

		public Texture2D CreateTexture(params string[] lines)
		{
			// Going through the lines to see how long the longest line is..
			int maxLength = 0;
			foreach (var s in lines)
			{
				int l = s.Length;
				if (l > maxLength) maxLength = l;
			}

			Texture2D result = new Texture2D(Game.GetInstance().GetGraphicsDeviceManager().GraphicsDevice, (int)SymbolSize.X * maxLength, (int)SymbolSize.Y * lines.Length);

			int line = 0;
			foreach (string s in lines)
			{

				int character = 0;
				foreach (char c in (this.IsIgnoreCase() ? s.ToLower() : s))
				{
					if (SymbolPosition.ContainsKey(c))
					{
						Color[] colorData = new Color[TotalPixelAmount];
						// move the rectangle
						this.Rectangle.Location = SymbolPosition[c];

						ImageManager.GetTexture2D(TextureKey).GetData<Color>(0, Rectangle, colorData, 0, TotalPixelAmount);

						this.Rectangle.Location = new Point(character * ((int)SymbolSize.X), line * ((int)SymbolSize.Y));
						result.SetData<Color>(0, Rectangle, colorData, 0, TotalPixelAmount);
					}
					else
					{
						Color[] colorData = new Color[TotalPixelAmount];
						this.Rectangle.Location = new Point(character * ((int)SymbolSize.X), line * ((int)SymbolSize.Y));
						result.SetData<Color>(0, Rectangle, colorData, 0, TotalPixelAmount);
					}

					character++;
				}
				line++;
			}

			return result;
		}
	}
}
