using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class FuncPattern : IPattern
	{
		private readonly Func<AbstractSprite, GameTime, Boolean> LambdaFunc;

		public FuncPattern(Func<AbstractSprite, GameTime, Boolean> lambdaFunc)
		{
			this.LambdaFunc = lambdaFunc;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime) => LambdaFunc(host, gameTime);
	}
}
