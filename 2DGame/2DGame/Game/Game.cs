using System.Collections.Generic;
using System.Linq;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game
{
	/// <summary>
	///     This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		/// <summary>
		///     Static instance of the game
		/// </summary>
		private static Game GameInstance;

		/// <summary>
		///     Arial Font
		/// </summary>
		public static SpriteFont FontArial;

		/// <summary>
		///     Consolas Font
		/// </summary>
		public static SpriteFont FontConsolas;

		/// <summary>
		///     This is the size at which we render the game.
		/// </summary>
		public static readonly Point RenderSize = new Point(700, 900);

		/// <summary>
		///     Parsed arguments which were supplied by the commandline
		/// </summary>
		public static GameArguments GameArguments;

		public static Texture2D WhitePixel;

		private readonly FrameCounter FrameCounter;

		/// <summary>
		///     Our <see cref="GraphicsDeviceManager" />
		/// </summary>
		public readonly GraphicsDeviceManager Graphics;

		/// <summary>
		///     This allows us to change the size of the window without changing the render size.
		/// </summary>
		private RenderTarget2D NativeRenderTarget;

		/// <summary>
		///     <see cref="SpriteBatch" /> used for drawing
		/// </summary>
		private SpriteBatch SpriteBatch;

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

			//IsMouseVisible = true;

			IsFixedTimeStep = false;
			Graphics.SynchronizeWithVerticalRetrace = false; // testing with uncapped framerate

			FrameCounter = new FrameCounter();
		}

		public static Game GetInstance()
		{
			return GameInstance;
		}

		public static void ResetFrameCounter()
		{
			GetInstance().FrameCounter.Reset();
		}

		/// <summary>
		///     Closes the game.
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

			WhitePixel = new Texture2D(GraphicsDevice, 1, 1);
			WhitePixel.SetData(new[] {Color.White});
		}

		/// <summary>
		///     UnloadContent will be called once per game and is the place to unload
		///     game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here

			WhitePixel.Dispose();
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
				SceneManager.CloseScene(new TestTransition(1.0d));


			if (KeyboardManager.IsKeyDown(Keys.P)) SceneManager.AddScene("menu");
			if (SceneManager.GetCurrentScene() == null) Exit();

			FrameCounter.Update(gameTime);
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

			SpriteBatch.DrawString(FontConsolas, $"SceneKey : {SceneManager.GetCurrentScene().SceneKey}",
				new Vector2(20, RenderSize.Y - 20), Color.Black);
			SpriteBatch.DrawString(FontConsolas,
				$"Framerate: {FrameCounter.AverageFramerate:F2} ({FrameCounter.MinimumFramerate:F2})",
				new Vector2(20, RenderSize.Y - 40), Color.Black);
			SpriteBatch.DrawString(FontConsolas,
				$"Sprites  : {SceneManager.GetAllSprites().Sum(x => x.Value.Count)} ({SceneManager.GetTotalSpriteCount()})",
				new Vector2(20, RenderSize.Y - 60), Color.Black);

			var players = SceneManager.GetSprites<PlayerSprite>();
			var player = players.FirstOrDefault();
			if (player != null)
				SpriteBatch.DrawString(FontConsolas, $"Player   : {player.Position.X:F2} - {player.Position.Y:F2}",
					new Vector2(20, RenderSize.Y - 80), Color.Black);

			SpriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);

			SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.AnisotropicClamp);
			SpriteBatch.Draw(NativeRenderTarget,
				new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}

	internal class FrameCounter
	{
		private readonly int DELTA_BUFFER_SIZE = 100;

		private readonly Queue<double> DeltaBuffer;
		internal double MinimumFramerate;


		public FrameCounter()
		{
			DeltaBuffer = new Queue<double>();
			MinimumFramerate = 1000f;
		}

		internal double AverageFramerate
		{
			get { return GetAverageFramerate(); }
		}

		public void Update(GameTime gameTime)
		{
			var framerate = 1000d / gameTime.ElapsedGameTime.TotalMilliseconds;

			if (DeltaBuffer.Count > DELTA_BUFFER_SIZE) DeltaBuffer.Dequeue();
			DeltaBuffer.Enqueue(framerate);


			if (KeyboardManager.IsKeyDown(Keys.NumPad0)) Reset();
			if (AverageFramerate <= MinimumFramerate || MinimumFramerate < 0.0d) MinimumFramerate = AverageFramerate;
		}

		private double GetAverageFramerate()
		{
			var temp = DeltaBuffer.Count > 0 ? DeltaBuffer.Average() : -1d;
			return double.IsInfinity(temp) ? -1d : temp;
		}

		internal void Reset()
		{
			DeltaBuffer.Clear();
			MinimumFramerate = -1d;
		}
	}
}