using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
		private static Game GameInstance;

        private readonly GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        public static SpriteFont FontArial;
		public static Vector2 GraphicsArea
		{
			get;
			private set;
		}

        public static Rectangle GraphicsAreaRectangle
        {
            get;
            private set;
        }

        public Game()
        {
			GameInstance = this;

            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Changing the window size
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.PreferredBackBufferWidth = 800;
            GraphicsArea = new Vector2(Graphics.PreferredBackBufferWidth,Graphics.PreferredBackBufferHeight);

            GraphicsAreaRectangle = new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

	        this.IsMouseVisible = true;

        }

		public static Game GetInstance()
		{
			return GameInstance;
		}

		public GraphicsDeviceManager GetGraphicsDeviceManager()
		{
			return Graphics;
		}

		public static void ExitGame()
		{
			GetInstance().Exit();
		}

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ImageManager.SetContentManager(Content);
			FontManager.SetContentManager(Content);

			FontManager.GetInstance();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            FontArial = Content.Load<SpriteFont>("Fonts/Arial");

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Images are loaded when needed. Don't load them here! We use the ImageManager.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				SceneManager.SetCurrentScene("mainmenu");

            // This updates the current scene.
            SceneManager.GetCurrentScene().Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 128, 255));

            // TODO: Add your drawing code here
            SpriteBatch.Begin();

            // Drawing the current Scene.
            SceneManager.GetCurrentScene().Draw(SpriteBatch);

            // Only add something here if it affects the game globally!

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
