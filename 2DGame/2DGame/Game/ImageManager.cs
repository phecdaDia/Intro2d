using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game
{
	public class ImageManager
	{
		// Making the ImageManager a singleton
		private static ImageManager Instance;

		private Dictionary<string, Texture2D> TextureDictionary;

		private ContentManager Content;

		public ImageManager(ContentManager content)
		{
			// Setting the singleton instance
			Instance = this;

			// Creating the TextureDirectory
			this.TextureDictionary = new Dictionary<string, Texture2D>();

			// Setting the ContentManager
			this.Content = content;
		}

		// Get the singleton instance
		public static ImageManager getInstance()
		{
			return Instance;
		}

		private Texture2D LoadTexture(String key)
		{
			try
			{
				// Loading the texture
				Texture2D texture = Content.Load<Texture2D>(key);

				// Adding texture to our TextureDictionary
				this.TextureDictionary.Add(key, texture);

				// return texture if the texture was loaded successfully. 
				return texture;
			}
			catch (Exception /*ex*/)
			{
				// Teture could not be loaded. Returning null. 
				return null;
			}
		}

		public Texture2D GetTexture2D(String key)
		{
			this.TextureDictionary.TryGetValue(key, out Texture2D output);

			// output is null when the image wasn't loaded yet.
			if (output == null)
			{
				// Loading the texture from the pipeline
				output = LoadTexture(key);
				if (output == null)
				{
					// Image was not found in content pipeline
					// Fallback image is used
					Console.WriteLine("[ImageManager] Texture2D could not be loaded: {0}", key);
					Console.WriteLine("\tUsing fallback texture");
					this.TextureDictionary.TryGetValue("fallback", out output);

					// returning fallback
					return output;
				}

				// returning newly loaded texture
				return output;
			}

			// return previously loaded texture
			return output;
		}


	}
}
