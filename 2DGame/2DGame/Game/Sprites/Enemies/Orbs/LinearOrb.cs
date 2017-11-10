using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class LinearOrb : AbstractOrb
	{
		public LinearOrb(Vector2 position, Vector2 direction, float speed) : base("orb3", position, direction)
		{
			Direction *= speed;
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			return Direction;
		}
	}
}