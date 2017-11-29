using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	/// <summary>
	/// A normal orb that does nothing special.
	/// </summary>
	public class LinearOrb : AbstractOrb
	{
		public LinearOrb(Vector2 position, Vector2 direction, float speed) : this("orb3", position, direction, speed) {}

		public LinearOrb(string textureKey, Vector2 position, Vector2 direction, float speed) : base(textureKey, position, direction)
		{
			Direction *= speed;
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			return Direction;
		}
	}
}