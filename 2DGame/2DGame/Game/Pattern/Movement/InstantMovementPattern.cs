using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Movement
{
	public class InstantMovementPattern : IPattern
	{
		private readonly Vector2 Delta;

		public InstantMovementPattern(Vector2 delta)
		{
			Delta = delta;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			host.Position += Delta;
			return true;
		}
	}
}