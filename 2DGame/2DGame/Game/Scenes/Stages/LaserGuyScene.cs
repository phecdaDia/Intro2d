using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Intro2DGame.Game.Fonts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class LaserGuyScene : Scene
	{
        private LaserGuySprite LaserGuy;
		public LaserGuyScene() : base("laserguy")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 360)));
            this.LaserGuy=new LaserGuySprite(new Vector2(1180, 360));
            AddSprite(this.LaserGuy);

		}
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            FontManager.DrawString(spriteBatch, "example", new Vector2(200, 10), $"Gegner:{LaserGuy.Health}");
        }
    }

	internal class LaserGuySprite : AbstractAnimatedSprite
	{
		private double ShootDelay;
        private double DauerZeit;
        private int LaserShoot;
        private float Dreh;
		public LaserGuySprite(Vector2 position):base("tutorialplayer",position)
		{
            this.Enemy = true;
            this.Persistence = true;
            this.MaxHealth = 2500;
            this.Health = 2500;
            DauerZeit = 0f;
            LaserShoot = 0;
            ShootDelay = 200f;
		}

		protected override void AddFrames()
		{
			AddAnimation(new Point[] {new Point(), });
		}

		public override void Update(GameTime gameTime)
		{
            //var MoveMent = new Vector2();
            DauerZeit += gameTime.ElapsedGameTime.Milliseconds;
            var Area = Game.RenderSize;
            /*
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
            */
            var sprites = SceneManager.GetAllSprites();
            foreach (var k in sprites)
            {
                if (!k.Key.IsSubclassOf(typeof(PlayerOrb))) continue;
                foreach (AbstractOrb t in k.Value)
                {
                    t.Delete();
                    SpawnSprite(new PlayerOrb(t.GetPosition(), this.GetPosition()));
                }
            }
            if(DauerZeit>ShootDelay)
            {
                LaserShoot++;
                DauerZeit %= ShootDelay;
                if(LaserShoot>3)
                {
                    LaserShoot = 0;
                    SpawnSprite(new LaserOrb(Position, SceneManager.GetSprites<PlayerSprite>().First().GetPosition() - this.GetPosition()));
                }
                Dreh+= (float)Math.PI / 36f;
                if (Dreh > (float)Math.PI) Dreh = 0f;
                for (int i=0;i<12;++i)
                {
                    SpawnSprite(new LinearOrb(Position, new Vector2((float)Math.Sin(Math.PI/6*i+ Dreh), (float)Math.Cos(Math.PI / 6 * i+ Dreh)),3.0f));
                }
            }
		}
        public override bool DoesCollide(Vector2 position)
        {
            return (position - this.GetPosition()).Length() <= 16 ||
                   (position - this.GetPosition() - new Vector2(0, 16)).Length() <= 16 ||
                   (position - this.GetPosition() + new Vector2(0, 16)).Length() <= 16
            ;
        }
    }
}
