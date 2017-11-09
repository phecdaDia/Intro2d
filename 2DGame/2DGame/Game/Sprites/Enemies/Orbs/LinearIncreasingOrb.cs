using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
    public class LinearIncreasingOrb : AbstractOrb
    {

        private readonly float Speed2;

        public LinearIncreasingOrb(Vector2 position, Vector2 direction, float speed, float speed2) : base("orb3", position, direction)
		{
            this.Direction *= speed;

            this.Speed2 = speed2;
        }

        protected override Vector2 UpdatePosition(GameTime gameTime)
        {
            Direction *= this.Speed2;
            return Direction;
        }
    }
}
