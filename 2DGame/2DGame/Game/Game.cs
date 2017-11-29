using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace Intro2DGame.Game
{
	/// <summary>
	///     This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		/// <summary>
		/// Static instance of the game
		/// </summary>
		private static Game GameInstance;

		/// <summary>
		/// Arial Font
		/// </summary>
		public static SpriteFont FontArial;

		/// <summary>
		/// Consolas Font
		/// </summary>
		public static SpriteFont FontConsolas;

		/// <summary>
		/// Our <see cref="GraphicsDeviceManager"/>
		/// </summary>
		public readonly GraphicsDeviceManager Graphics;

		/// <summary>
		/// <see cref="SpriteBatch"/> used for drawing
		/// </summary>
		private SpriteBatch SpriteBatch;

		/// <summary>
		/// This is the size at which we render the game.
		/// </summary>
		public static readonly Point RenderSize = new Point(800, 600);//(1280, 720);

		/// <summary>
		/// This allows us to change the size of the window without changing the render size.
		/// </summary>
		private RenderTarget2D NativeRenderTarget;

		/// <summary>
		/// Parsed arguments which were supplied by the commandline
		/// </summary>
		public static GameArguments GameArguments;

		private float Framerate, MinimumFramerate;

		public Game(params string[] args)
		{
			GameArguments = new GameArguments(args);

			GameInstance = this;

			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			// Changing the window size
			Graphics.PreferredBackBufferWidth = GameArguments.BackbufferWidth;
			Graphics.PreferredBackBufferHeight = GameArguments.BackbufferHeight;
			

			if (GameArguments.IsFullScreen) Graphics.ToggleFullScreen();

			IsMouseVisible = true;
		}

		public static Game GetInstance()
		{
			return GameInstance;
		}

		/// <summary>
		/// Closes the game.
		/// </summary>
		public static void ExitGame()
		{
			GetInstance().Exit();
		}

		/// <summary>
		///     Allows the game to perform any initialization it needs to before starting to run.
		///     This is where it can query for any required services and load any non-graphic
		///     related content.  Calling base.Initialize will enumerate through any components
		///     and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			ImageManager.SetContentManager(Content);
			FontManager.SetContentManager(Content);

			FontManager.GetInstance();


			NativeRenderTarget = new RenderTarget2D(GraphicsDevice, RenderSize.X, RenderSize.Y);

			base.Initialize();
		}

		/// <summary>
		///     LoadContent will be called once per game and is the place to load
		///     all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			FontArial = Content.Load<SpriteFont>("Fonts/Arial");
			FontConsolas = Content.Load<SpriteFont>("Fonts/Consolas");

			// Create a new SpriteBatch, which can be used to draw textures.
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			// Images are loaded when needed. Don't load them here! We use the ImageManager.
		}

		/// <summary>
		///     UnloadContent will be called once per game and is the place to unload
		///     game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		///     Allows the game to run logic such as updating the world,
		///     checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			KeyboardManager.Update();

			if (KeyboardManager.IsKeyDown(Keys.Escape))
				SceneManager.CloseScene();



			if (KeyboardManager.IsKeyDown(Keys.P)) SceneManager.AddScene("menu");
			if (SceneManager.GetCurrentScene() == null) Exit();

			this.Framerate = 1000f / gameTime.ElapsedGameTime.Milliseconds;
			// This updates the current scene.

			SceneManager.Update(gameTime);
			//SceneManager.GetCurrentScene().Update(gameTime);


			base.Update(gameTime);
		}

		/// <summary>
		///     This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (SceneManager.GetCurrentScene() == null) return;


			GraphicsDevice.SetRenderTarget(NativeRenderTarget);
			GraphicsDevice.Clear(new Color(0, 128, 255));

			// TODO: Add your drawing code here
			SpriteBatch.Begin();

			// Drawing the current Scene.
			SceneManager.Draw(SpriteBatch);
			//SceneManager.GetCurrentScene().Draw(SpriteBatch);

			// Only add something here if it affects the game globally!

			SpriteBatch.DrawString(FontConsolas, $"SceneKey : {SceneManager.GetCurrentScene().SceneKey}", new Vector2(10, RenderSize.Y - 20), Color.Black);
			SpriteBatch.DrawString(FontConsolas, $"Framerate: {this.Framerate}", new Vector2(10, RenderSize.Y - 40), Color.Black);
			SpriteBatch.DrawString(FontConsolas, $"Sprites  : {SceneManager.GetAllSprites().Sum(x => x.Value.Count)}", new Vector2(10, RenderSize.Y - 60), Color.Black);

			SpriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);

			SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.AnisotropicClamp);
			SpriteBatch.Draw(NativeRenderTarget, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}