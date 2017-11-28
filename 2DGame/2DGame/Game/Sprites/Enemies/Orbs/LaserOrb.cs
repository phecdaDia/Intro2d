using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
    public class LaserOrb : AbstractOrb
    {
        protected int Time=0;
        protected static Texture2D FirstTexture2D;
        protected static Texture2D SecondTexture2D;
        //protected Vector2 LaserPosition1;
        //protected Vector2 LaserPosition2;
        public Boolean AfterDamage;
        public LaserOrb(Vector2 Position, Vector2 Direction) : base("OrbLaser2", Position, Direction)
        {
            Scale.X = 50;
            Scale.Y = 1;
            AfterDamage = false;
            Hue = Color.Blue;
            FirstTexture2D = ImageManager.GetTexture2D("OrbLaser2");
            SecondTexture2D = ImageManager.GetTexture2D("OrbLaser");
            //if (TextureDictionary.ContainsKey(FirstTexture2D.GetType()))
            //    TextureDictionary[FirstTexture2D.GetType()] = FirstTexture2D;
            //if (TextureDictionary.ContainsKey(SecondTexture2D.GetType()))
            //    TextureDictionary[SecondTexture2D.GetType()] = SecondTexture2D;
        }
        protected override Vector2 UpdatePosition(GameTime gameTime)
        {
            return Direction;
        }
        public override void Update(GameTime gameTime)
        {
            Time++;
            if (Time > 90) this.Delete();
            if (Time>60&& !AfterDamage)
            {
                var Players = SceneManager.GetSprites<PlayerSprite>();
                foreach(var Player in Players)
                {
                    if (!Player.DoesCollide(this)) continue;
                    Player.Damage(GameConstants.PLAYER_DAMAGE);
                    AfterDamage = true;
                }
                return;
            }
            
        }
        public override void Draw(SpriteBatch SpriteBatch)
        {
            if (FirstTexture2D ==null|| SecondTexture2D == null) return;
            if (Time <= 60)
            {
                SpriteBatch.Draw(FirstTexture2D,
                    Position,
                    null,
                    Hue,
                    Rotation,
                    new Vector2(FirstTexture2D.Width / 2f, FirstTexture2D.Height / 2f),
                    Scale,
                    SpriteEffects.None,
                    0f);
            }
            if (Time>60&& Time<=90)
            {
                SpriteBatch.Draw(SecondTexture2D,
                    Position,
                    null,
                    Hue,
                    Rotation,
                    new Vector2(SecondTexture2D.Width / 2f, SecondTexture2D.Height / 2f),
                    Scale,
                    SpriteEffects.None,
                    0f);
                return;
            }
        }
    }
}
