using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class TandemPattern : IPattern
	{
		private IPattern[] Patterns;
		private bool[] FinishedPatterns;

		public TandemPattern(params IPattern[] patterns)
		{
			this.Patterns = patterns;
			this.FinishedPatterns = new bool[Patterns.Length];
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var finished = true;
			for (var i=0; i<Patterns.Length; i++)
			{
				if (FinishedPatterns[i]) continue;

				if (!Patterns[i].Execute(host, gameTime))
				{
					finished = false;
				}
				else
				{
					FinishedPatterns[i] = true;
				}
			}

			return finished;
		}
	}
}
