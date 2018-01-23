using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class SequencePattern : IPattern
	{
		private Queue<IPattern> Patterns;

		public SequencePattern(params IPattern[] patterns)
		{
			this.Patterns = new Queue<IPattern>();
			this.Patterns.EnqueueMany(patterns);
			
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var count = this.Patterns.Count;
			if (count == 0) return true;

			var finished = 0;
			while (finished < count && Patterns.Peek().Execute(host, gameTime))
			{
				Patterns.Dequeue();
				finished++;
			}

			return finished == count;
		}
	}
}
