using System;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Orbs
{
	public class BarrageLinearPattern : IPattern
	{
		private readonly double Distance;
		private readonly double Offset;
		private readonly float Speed;

		public BarrageLinearPattern(float speed, double distance, double offset)
		{
			Distance = distance.ToDegrees();
			Offset = offset.ToDegrees();

			Speed = speed;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var curr = Offset;
			var limit = 2 * Math.PI + Distance + Offset;

			do
			{
				SceneManager.GetCurrentScene().BufferedAddSprite(new LinearOrb(host.Position, curr.ToVector2(), Speed));
				curr += Distance;
			} while (curr <= limit);

			return true;
		}
	}
}