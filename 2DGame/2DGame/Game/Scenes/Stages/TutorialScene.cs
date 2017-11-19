using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes.Stages
{
    /// <summary>
    /// This scene is used for the tutorial
    /// </summary>
	public class TutorialScene : Scene
	{
		public TutorialScene() : base("tutorial")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 350)));
			AddSprite(new TutorialSprite(new Vector2(400,250)));
		}
	}

    /// <summary>
    /// Main control sprite for the tutorial
    /// </summary>
	internal class TutorialSprite : AbstractSprite
	{
        private int Shoot_Delay;
        private int ShootDelay;
        public TutorialSprite(Vector2 position):base("tutorialplayer",position)
        {
            ShootDelay = Shoot_Delay = 15;
        }
		public override void Update(GameTime gameTime)
		{
            var MoveMent = new Vector2();
            var Area = Game.GraphicsArea;
            if (KeyboardManager.IsKeyPressed(Keys.Up)) MoveMent += new Vector2(0, -1);
            if (KeyboardManager.IsKeyPressed(Keys.Down)) MoveMent += new Vector2(0, 1);
            if (KeyboardManager.IsKeyPressed(Keys.Left)) MoveMent += new Vector2(-1, 0);
            if (KeyboardManager.IsKeyPressed(Keys.Right)) MoveMent += new Vector2(1, 0);
            MoveMent *= new Vector2(1.1f, 1.0f);
            Position += MoveMent*4.25f;
            // Prevents player from leaving the screen
            if (Position.X + Texture.Width / 2f > Area.X) Position.X = Area.X - Texture.Width / 2f;
            if (Position.Y + Texture.Height / 2f > Area.Y) Position.Y = Area.Y - Texture.Height / 2f;
            if (Position.X - Texture.Width / 2f < 0) Position.X = Texture.Width / 2f;
            if (Position.Y - Texture.Height / 2f < 100) Position.Y = 100 + Texture.Height / 2f;

            if (ShootDelay--<=0 && KeyboardManager.IsKeyPressed(Keys.M))
            {
                ShootDelay = Shoot_Delay;
                SpawnSprite(new LinearIncreasingOrb(Position, new Vector2(-1, 0), 0.235f,1.01025f));
            }
        }
	}
}