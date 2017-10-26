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
		private static ImageManager instance;

		// Dictionary for the Textures, so we only have to load them once.
		// Loading will be done via LoadTexture(String): Texture2D in the class that needs the texture
		private Dictionary<string, Texture2D> textureDictionary;

		// Instance of the ContentPipeline
		private static ContentManager content;

		// Creating the ImageManager
        private ImageManager()
        {
            // ImageManager is a singleton. If there already is an instance, we don't want to create another one.
            if (instance != null) return;

            // Setting the singleton instance
            instance = this;

            // Creating the TextureDirectory
            this.textureDictionary = new Dictionary<string, Texture2D>();
        }

        public static void SetContentManager(ContentManager contentManager)
        {
            content = contentManager;
        }

		// Get the singleton instance
		public static ImageManager GetInstance()
		{
            if (content == null) return null;

            if (instance == null) instance = new ImageManager();
			return instance;
		}

		private Texture2D LoadTexture(String key)
		{
			try
			{
				// Loading the texture
				Texture2D texture = content.Load<Texture2D>(key);

				// Adding texture to our TextureDictionary
				this.textureDictionary.Add(key, texture);

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

            // We have to create "output" here. CSharp 2015 doesn't like it in the out.
            Texture2D output;
			this.textureDictionary.TryGetValue(key, out output);

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
					this.textureDictionary.TryGetValue("fallback", out output);

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
