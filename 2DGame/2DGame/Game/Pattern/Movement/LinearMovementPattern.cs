using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Movement
{
	public class LinearMovementPattern : IPattern
	{
		private readonly Vector2 DeltaMovement;
		private double Timespan;

		public LinearMovementPattern(Vector2 delta, double timespan)
		{
			DeltaMovement = delta / (float) timespan;
			Timespan = timespan;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			Timespan -= gameTime.ElapsedGameTime.TotalSeconds;

			var delta = gameTime.ElapsedGameTime.TotalSeconds;

			if (Timespan < 0.0d)
			{
				delta += Timespan;
				host.Position += DeltaMovement * (float) delta;
				return true;
			}

			host.Position += DeltaMovement * (float) delta;
			return false;
		}

		public static LinearMovementPattern GenerateFromVector2(Vector2 delta, float units)
		{
			return new LinearMovementPattern(delta, delta.Length() / units);
		}
	}
}