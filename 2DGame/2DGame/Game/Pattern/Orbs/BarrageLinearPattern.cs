using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Orbs
{
	public class BarrageLinearPattern : IPattern
	{
		private readonly float Speed;
		private readonly Double Distance;
		private readonly Double Offset;

		public BarrageLinearPattern(float speed, double distance, double offset)
		{
			this.Distance = distance.ToDegrees();
			this.Offset = offset.ToDegrees();

			this.Speed = speed;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var curr = this.Offset;
			var limit = 2 * Math.PI + this.Distance + this.Offset;

			do
			{
				SceneManager.GetCurrentScene().BufferedAddSprite(new LinearOrb(host.Position, curr.ToVector2(), this.Speed));
				curr += this.Distance;
			} while (curr <= limit);

			return true;
		}
	}
}