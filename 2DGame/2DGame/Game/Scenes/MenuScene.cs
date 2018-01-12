using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
	/// <inheritdoc />
	/// <summary>
	///     Scene shown when the player pauses the game
	/// </summary>
	public class MenuScene : Scene
	{
		public MenuScene() : base("menu")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new MenuSprite());
		}
	}

	internal class MenuSprite : AbstractAnimatedSprite
	{
		public MenuSprite() : base("test/coin", new Vector2(400, 300), new Point(24), 30)
		{
			Rotation = 45f;
			Scale = new Vector2(2);
		}

		public override void Update(GameTime gameTime)
		{
			if (KeyboardManager.IsKeyDown(Keys.Pause)) SceneManager.CloseScene();

			Rotation += 3.6f * gameTime.ElapsedGameTime.Milliseconds / 1000f;

			Rotation %= 360f;


			base.Update(gameTime);
		}

		protected override void AddFrames()
		{
			AddAnimation(new[]
			{
				new Point(0, 0),
				new Point(24, 0),
				new Point(48, 0),
				new Point(72, 0),
				new Point(96, 0),
				new Point(120, 0)
			});
		}
	}
}