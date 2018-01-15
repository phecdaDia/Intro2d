using System;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Movement
{
	public class RadialMovementPattern : IPattern
	{
		private readonly Vector2 CenterVector;
		private readonly double DeltaAngle;
		private readonly double InitialAngle;
		private readonly float Radius;
		private readonly double Timespan;

		private double ElapsedTime;

		private Vector2 LastResult;

		public RadialMovementPattern(Vector2 position, float radius, double initialAngle, double deltaAngle, double timespan)
		{
			Radius = radius;
			InitialAngle = initialAngle.ToRadiants();
			DeltaAngle = deltaAngle.ToRadiants();
			Timespan = timespan;

			var normalVector = InitialAngle.ToVector2();

			CenterVector = position - normalVector * radius;

			this.LastResult = normalVector;
		}

		public RadialMovementPattern(Vector2 position, Vector2 normal, double deltaAngle, double timespan)
		{
			CenterVector = position + normal;
			DeltaAngle = deltaAngle.ToRadiants();
			Timespan = timespan;

			Radius = normal.Length();
			InitialAngle = normal.ToAngle();

			this.LastResult = normal;

			//Console.WriteLine($"{CenterVector} - {DeltaAngle} - {Radius} - {InitialAngle}");
		}


		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			this.ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

			var isOver = ElapsedTime > this.Timespan;

			if (isOver)
			{
				host.Position += (this.InitialAngle + this.DeltaAngle).ToVector2() * Radius - LastResult;


				return true;
			}
			else
			{
				var result = (this.InitialAngle + (this.DeltaAngle * (this.ElapsedTime / this.Timespan))).ToVector2() * Radius;
				host.Position += result - LastResult;
				this.LastResult = result;

				return false;
			}



		}
	}
}