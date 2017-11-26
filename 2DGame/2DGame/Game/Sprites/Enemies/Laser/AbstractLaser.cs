using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Intro2DGame.Game.Sprites.Enemies.Orbs;

namespace Intro2DGame.Game.Sprites.Enemies.Laser
{
    public abstract class AbstractLaser : AbstractOrb
    {
        public Vector2 Displacement;
        public float Speed = 5f;
        public float Length = 2f;
        public AbstractLaser(string TextureKey, Vector2 Position, Vector2 ZielPosition):base(TextureKey, Position, ZielPosition)
        {
            //this.Rotation = (float)Math.Atan(ZielPosition.Y / ZielPosition.X);
            //if (ZielPosition.X > 0) this.Rotation += 3.14f;
            Displacement = new Vector2((float)Math.Cos(Rotation),(float)Math.Sin(Rotation));
            Displacement *= Speed;
            this.Scale = new Vector2(Length,1);
        }
        public override void Update(GameTime gameTime)
        {
            Position += Displacement;
        }
        /*
        public Vector2 Position;
        protected Texture2D Texture;
        public float Rotation = 0f;
        public virtual void Draw(SpriteBatch SpriteBatch)
        {

            if (Texture == null) return;
            SpriteBatch.Draw(
                Texture,//2D纹理
                Position,//纹理坐标
                null,
                Color.White,//颜色遮罩
                Rotation,
                new Vector2(Texture.Width / 2f, Texture.Height / 2f),
                new Vector2(1),//缩放参数
                SpriteEffects.None,//纹理左右上下映射
                0f);//纹理深度
        }
        */
    }
}
