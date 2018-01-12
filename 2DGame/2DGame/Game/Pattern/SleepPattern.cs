using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class SleepPattern : IPattern
	{
		private double ElapsedSeconds;
		private readonly double Timeout;

		public SleepPattern(double timeout)
		{
			this.Timeout = timeout;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			this.ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
			return this.ElapsedSeconds >= this.Timeout;
		}
	}
}