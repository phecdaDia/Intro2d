using System;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Intro2DGame.Game.Sprites.Enemies.Laser;
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
        public static PlayerSprite Player1
        {
            get;
            protected set;
        }
        protected override void CreateScene()
        {
            Player1 = new PlayerSprite(new Vector2(100, 350));
            AddSprite(Player1);
            AddSprite(new TutorialSprite(new Vector2(400, 250)));
        }
    }
    /// <summary>
    /// Main control sprite for the tutorial
    /// </summary>
    /// 

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
            Position += MoveMent * 4.25f;
            // Prevents player from leaving the screen
            if (Position.X + Texture.Width / 2f > Area.X) Position.X = Area.X - Texture.Width / 2f;
            if (Position.Y + Texture.Height / 2f > Area.Y) Position.Y = Area.Y - Texture.Height / 2f;
            if (Position.X - Texture.Width / 2f < 0) Position.X = Texture.Width / 2f;
            if (Position.Y - Texture.Height / 2f < 100) Position.Y = 100 + Texture.Height / 2f;

            if (ShootDelay-- <= 0 && KeyboardManager.IsKeyPressed(Keys.M))
            {
                ShootDelay = Shoot_Delay;
                SpawnSprite(new TutorialLaser(Position, TutorialScene.Player1.GetPosition() - this.GetPosition()));
            }
            //var sprites = SceneManager.GetAllSprites();

           

        }
	}
    internal class TutorialLaser:AbstractLaser
    {
        public TutorialLaser(Vector2 Position,Vector2 ZielPosition): base("OrbLaser", Position, ZielPosition)
        {
            //TutorialScene.Player2.GetPosition() - TutorialScene.Player1.GetPosition()
            //this.Rotation = (float)Math.Acos(TutorialScene.Player1.GetPosition().X / TutorialScene.Player1.GetPosition().Y);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (TutorialScene.Player1.DoesCollide(this))
            {
                TutorialScene.Player1.Health -= 200;
                this.Delete();
            }
        }
        protected override Vector2 UpdatePosition(GameTime gameTime)
        {
            return Direction;
        }
    }
}