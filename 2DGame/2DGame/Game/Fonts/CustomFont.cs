using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Fonts
{
    /// <summary>
    /// Allows implementation of custom fonts.
    /// Not recommended but works if implemented properly
    /// </summary>
	public abstract class CustomFont
	{
        /// <summary>
        /// Dictionary where all symbols are positioned
        /// </summary>
		protected readonly Dictionary<char, Point> SymbolPosition;
        /// <summary>
        /// Key for the texture
        /// </summary>
		private readonly string TextureKey;

        /// <summary>
        /// Amount of pixels for every character
        /// </summary>
		private readonly int TotalPixelAmount;
        /// <summary>
        /// Current selecting rectangle.
        /// </summary>
		private Rectangle Rectangle;
        /// <summary>
        /// Size of the characters
        /// </summary>
		private Vector2 SymbolSize;

        /// <summary>
        /// Creates a new CustomFont
        /// </summary>
        /// <param name="fontName">Key</param>
        /// <param name="textureKey">Texture name in the production pipeline</param>
        /// <param name="symbolSize">Size of each character</param>
		public CustomFont(string fontName, string textureKey, Vector2 symbolSize)
		{
            // If no key has been supplied, throw a new exception
			if (fontName == null) throw new ArgumentNullException(nameof(fontName));

            if(textureKey==null) throw new ArgumentNullException(nameof(textureKey));
            TextureKey = textureKey;
			SymbolSize = symbolSize;

            // Creates the position dictionary
			SymbolPosition = new Dictionary<char, Point>();

            // Calculate the total pixels. 
			TotalPixelAmount = (int) (symbolSize.X * symbolSize.Y);
			Rectangle = new Rectangle(0, 0, (int) symbolSize.X, (int) symbolSize.Y);

			FontManager.GetInstance().RegisterFont(fontName, this);

			SetSymbolPositions();
		}

        /// <summary>
        /// Returns if the font ignores case
        /// </summary>
        /// <returns></returns>
		protected abstract bool IsIgnoreCase();

        /// <summary>
        /// Populating the SymbolPositions
        /// </summary>
		protected abstract void SetSymbolPositions();

        /// <summary>
        /// Creates texture from string
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
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