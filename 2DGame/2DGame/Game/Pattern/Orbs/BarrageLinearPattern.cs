using System;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Orbs
{
	public class BarragePattern : IPattern
	{
		private readonly int Amount;
		private readonly double Angle;
		private readonly double Offset;
		private readonly float Speed;

		public BarragePattern(float speed, int amount, double angle, double offset)
		{
			Amount = amount;
			Angle = angle.ToRadiants();
			Offset = offset.ToRadiants();

			Speed = speed;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var curr = Angle;
			var delta = Offset / Amount;

			for (var i = 0; i <= Amount; i++)
			{
				SceneManager.GetCurrentScene().BufferedAddSprite(new LinearOrb(host.Position, curr.ToVector2(), this.Speed));
				curr += delta;
			}

			return true;
		}
	}
}