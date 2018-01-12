using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Movement
{
	public class RadialMovementPattern : IPattern
	{
		private readonly float Radius;
		private readonly double InitialAngle;
		private readonly double DeltaAngle;
		private readonly Vector2 CenterVector;
		private readonly double Timespan;

		private double ElapsedTime;

		public RadialMovementPattern(Vector2 position, float radius, double initialAngle, double deltaAngle, double timespan)
		{
			this.Radius = radius;
			this.InitialAngle = initialAngle.ToDegrees();
			this.DeltaAngle = deltaAngle.ToDegrees();
			this.Timespan = timespan;

			var normalVector = this.InitialAngle.ToVector2();

			this.CenterVector = position - normalVector * radius;
		}

		public RadialMovementPattern(Vector2 position, Vector2 normal, double deltaAngle, double timespan)
		{
			this.CenterVector = position + normal;
			this.DeltaAngle = deltaAngle.ToDegrees();
			this.Timespan = timespan;
			
			this.Radius = normal.Length();
			this.InitialAngle = normal.ToAngle() + Math.PI;

			Console.WriteLine($"{this.CenterVector} - {this.DeltaAngle} - {this.Radius} - {this.InitialAngle}");

		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			this.ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

			var isOver = this.ElapsedTime > this.Timespan;

			var delta = this.ElapsedTime / this.Timespan;

			var tempAngle = !isOver ? this.DeltaAngle * delta : this.DeltaAngle;
			tempAngle += this.InitialAngle;
			host.Position = CenterVector + tempAngle.ToVector2() * Radius;

			return isOver;

		}
	}
}
