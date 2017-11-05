using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game
{
	public class ImageManager
	{
		// Making the ImageManager a singleton
		private static ImageManager Instance;

		// Dictionary for the Textures, so we only have to load them once.
		// Loading will be done via LoadTexture(String): Texture2D in the class that needs the texture
		private readonly Dictionary<string, Texture2D> TextureDictionary;

		// Instance of the ContentPipeline
		private static ContentManager Content;

		// Creating the ImageManager
        private ImageManager()
        {
            // ImageManager is a singleton. If there already is an Instance, we don't want to create another one.
            if (Instance != null) return;

            // Setting the singleton Instance
            Instance = this;

            // Creating the TextureDirectory
            this.TextureDictionary = new Dictionary<string, Texture2D>();
        }

        public static void SetContentManager(ContentManager contentManager)
        {
            Content = contentManager;
        }

		// Get the singleton Instance
		private static ImageManager GetInstance()
		{
			if (Content == null) return null;

			return Instance ?? (Instance = new ImageManager());
		}

		private Texture2D LoadTexture(string key)
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

		public static Texture2D GetTexture2D(string key)
		{

			// TODO: Probably redo this function

            // We have to create "output" here. CSharp 2015 doesn't like it in the out.
            Texture2D output;

			GetInstance().TextureDictionary.TryGetValue(key, out output);

			// output is null when the image wasn't loaded yet.
			if (output != null) return output;

			// Loading the texture from the pipeline
			output = GetInstance().LoadTexture(key);

			if (output != null) return output;


			// Image was not found in content pipeline
			// Fallback image is used
			Console.WriteLine("[ImageManager] Texture2D could not be loaded: {0}", key);
			Console.WriteLine("\tUsing fallback texture");
			GetInstance().TextureDictionary.TryGetValue("fallback", out output);

			// returning fallback
			return output;

			// returning newly loaded texture

			// return previously loaded texture
		}


	}
}
