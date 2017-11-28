using System;
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
        internal static TutorialPlayerSprite Player1{get;set;}
        internal static TutorialSprite Player2 { get; set; }
        protected override void CreateScene()
        {
            Player1 = new TutorialPlayerSprite(new Vector2(100, 350));
            AddSprite(Player1);
            Player2 = new TutorialSprite(new Vector2(400, 250));
            AddSprite(Player2);
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
            ShootDelay = Shoot_Delay = 40;
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
                SpawnSprite(new TutorialSpriteLaserOrb(Position, TutorialScene.Player1.GetPosition() - this.GetPosition()));
            }
            //var sprites = SceneManager.GetAllSprites();
        }
	}
    internal class TutorialSpriteLaserOrb: LaserOrb
    {
        public TutorialSpriteLaserOrb(Vector2 Position, Vector2 Direction) : base(Position, Direction) { }
        public override void Update(GameTime gameTime)
        {
            Time++;
            if (Time > 90) this.Delete();
            if (!AfterDamage && Time > 60)
            {
                if (TutorialScene.Player1.DoesCollide(this))
                {
                    TutorialScene.Player1.Damage(GameConstants.PLAYER_DAMAGE);
                    AfterDamage = true;
                }
                return;
            }
        }
    }
    internal class TutorialPlayerSprite:PlayerSprite
    {
        public TutorialPlayerSprite(Vector2 Position) : base(Position){}
        public override bool DoesCollide(AbstractOrb Orb)
        {
            double Steigung = Orb.Direction.Y / Orb.Direction.X;
            float ObenLinkX = this.Position.X - Texture.Width / 2f;
            float ObenLinkY = this.Position.Y - 100 - Texture.Height / 2f;//-100
            float LinearPointerY =(float)( Orb.GetPosition().Y-100-((Orb.GetPosition().X-ObenLinkX) * Steigung));//-100
            if (LinearPointerY < ObenLinkY)
            {
                LinearPointerY -= (float)(Steigung * Texture.Width);
                if (LinearPointerY > ObenLinkY)
                    return true;
                else
                    return false;
            }
            else
            {

                LinearPointerY += (float)(Steigung * Texture.Width);
                ObenLinkY += Texture.Height;
                if (LinearPointerY < ObenLinkY)
                    return true;
                else
                    return false;
            }
        }
    }
}