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

		public TandemPattern(params IPattern[] patterns)
		{
			this.Patterns = patterns;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var finished = true;
			foreach (var pattern in this.Patterns)
				if (!pattern.Execute(host, gameTime))
					finished = false;

			return finished;
		}
	}
}
