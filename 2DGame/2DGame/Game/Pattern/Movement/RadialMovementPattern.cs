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

		public RadialMovementPattern(Vector2 position, float radius, double initialAngle, double deltaAngle, double timespan)
		{
			Radius = radius;
			InitialAngle = initialAngle.ToDegrees();
			DeltaAngle = deltaAngle.ToDegrees();
			Timespan = timespan;

			var normalVector = InitialAngle.ToVector2();

			CenterVector = position - normalVector * radius;
		}

		public RadialMovementPattern(Vector2 position, Vector2 normal, double deltaAngle, double timespan)
		{
			CenterVector = position + normal;
			DeltaAngle = deltaAngle.ToDegrees();
			Timespan = timespan;

			Radius = normal.Length();
			InitialAngle = normal.ToAngle() + Math.PI;

			Console.WriteLine($"{CenterVector} - {DeltaAngle} - {Radius} - {InitialAngle}");
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

			var isOver = ElapsedTime > Timespan;

			var delta = ElapsedTime / Timespan;

			var tempAngle = !isOver ? DeltaAngle * delta : DeltaAngle;
			tempAngle += InitialAngle;
			host.Position = CenterVector + tempAngle.ToVector2() * Radius;

			return isOver;
		}
	}
}