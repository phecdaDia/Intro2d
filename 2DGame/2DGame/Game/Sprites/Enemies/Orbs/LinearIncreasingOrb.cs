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

        private readonly float Speed2;

        public LinearIncreasingOrb(Vector2 position, Vector2 direction, float speed, float speed2) : base("orb", position)
		{
            this.Direction = direction;
            this.Direction.Normalize();
            this.Direction *= speed;

            this.Speed2 = speed2;
        }

        protected override void UpdatePosition(GameTime gameTime)
        {
            Direction *= this.Speed2;
            this.Position += Direction;
        }
    }
}
