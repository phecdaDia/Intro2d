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
		private String textureKey;
		private Vector2 symbolSize;

		protected Dictionary<char, Point> symbolPosition; // We'll see if we should change that later.

		private int totalPixelAmount;
		private Rectangle rectangle;

		public CustomFont(String fontName, String textureKey, Vector2 symbolSize)
		{
			FontManager.GetInstance().RegisterFont(fontName, this);

			this.textureKey = textureKey;
			this.symbolSize = symbolSize;

			this.symbolPosition = new Dictionary<char, Point>();

			this.totalPixelAmount = (int)(symbolSize.X * symbolSize.Y);
			this.rectangle = new Rectangle(0, 0, (int)symbolSize.X, (int)symbolSize.Y);

			SetSymbolPositions();
		}

		protected abstract Boolean IsIgnoreCase();
		protected abstract void SetSymbolPositions();

		public Texture2D CreateTexture(params String[] lines)
		{
			// Going through the lines to see how long the longest line is..
			int maxLength = 0;
			foreach (String s in lines)
			{
				int l = s.Length;
				if (l > maxLength) maxLength = l;
			}

			Texture2D result = new Texture2D(Game.GetInstance().GetGraphicsDeviceManager().GraphicsDevice, (int)symbolSize.X * maxLength, (int)symbolSize.Y * lines.Length);

			int line = 0;
			foreach (String s in lines)
			{

				int character = 0;
				foreach (Char c in (this.IsIgnoreCase() ? s.ToLower() : s))
				{
					if (symbolPosition.ContainsKey(c))
					{
						Console.WriteLine("symbols contains " + c);
						Color[] colorData = new Color[totalPixelAmount];
						// move the rectangle
						this.rectangle.Location = symbolPosition[c];

						ImageManager.GetTexture2D(textureKey).GetData<Color>(0, rectangle, colorData, 0, totalPixelAmount);

						this.rectangle.Location = new Point(character * ((int)symbolSize.X), line * ((int)symbolSize.Y));
						result.SetData<Color>(0, rectangle, colorData, 0, totalPixelAmount);
					}

					character++;
				}
				line++;
			}

			return result;
		}
	}
}
