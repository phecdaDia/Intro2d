using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Movement
{
	public class LinearMovePattern : IPattern
	{
		private readonly Vector2 DeltaMovement;
		private double Timespan;

		public LinearMovePattern(Vector2 delta, double timespan)
		{
			this.DeltaMovement = delta / (float)timespan;
			this.Timespan = timespan;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			this.Timespan -= gameTime.ElapsedGameTime.TotalSeconds;

			var delta = gameTime.ElapsedGameTime.TotalSeconds;

			if (this.Timespan < 0.0d)
			{
				delta += this.Timespan;
				host.Position += DeltaMovement * (float)delta;
				return true;
			}
			else
			{

				host.Position += DeltaMovement * (float)delta;
				return false;
			}


		}
	}
}
