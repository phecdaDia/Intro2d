using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
    public class LinearIncreasingOrb : AbstractOrb
    {
        private Vector2 Direction;

        private float Speed2;

        public LinearIncreasingOrb(Vector2 Position, Vector2 Direction, float Speed, float Speed2) : base("orb", Position)
		{
            this.Direction = Direction;
            this.Direction.Normalize();
            this.Direction *= Speed;

            this.Speed2 = Speed2;
        }

        protected override void UpdatePosition(GameTime gameTime)
        {
            Direction *= this.Speed2;
            this.Position += Direction;
        }
    }
}
