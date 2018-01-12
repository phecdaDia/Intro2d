using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class SleepPattern : IPattern
	{
		private readonly double Timeout;
		private double ElapsedSeconds;

		public SleepPattern(double timeout)
		{
			Timeout = timeout;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
			return ElapsedSeconds >= Timeout;
		}
	}
}