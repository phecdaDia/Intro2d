using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class DamagePattern : IPattern
	{
		private readonly int Amount;

		public DamagePattern(int amount)
		{
			Amount = amount;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			host.Health -= this.Amount;
			return true;
		}
	}
}
