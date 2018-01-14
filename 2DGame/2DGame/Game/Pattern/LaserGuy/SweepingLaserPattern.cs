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

namespace Intro2DGame.Game.Pattern.LaserGuy
{
	public class SweepingLaserPattern : IPattern
	{
		private readonly Vector2 Position;
		private readonly double Angle;
		private readonly double DeltaAngle;
		private readonly float TimespanPreview;
		private readonly float TimespanFire;

		private double ElapsedSeconds;

		private LaserOrb ChildLaser;

		public SweepingLaserPattern(Vector2 position, double angle, double deltaAngle, float timespanPreview, float timespanFire)
		{
			Position = position;
			Angle = angle.ToRadiants();
			DeltaAngle = deltaAngle.ToRadiants();
			TimespanPreview = timespanPreview;
			TimespanFire = timespanFire;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			this.ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

			if (ChildLaser == null)
			{
				ChildLaser = new LaserOrb(this.Position, this.Angle.ToVector2(), this.TimespanPreview, this.TimespanFire);
				SceneManager.GetCurrentScene().BufferedAddSprite(ChildLaser);
			}

			if (this.ElapsedSeconds < this.TimespanPreview)
			{
				ChildLaser.Direction = (this.Angle + (this.ElapsedSeconds / this.TimespanPreview) * this.DeltaAngle).ToVector2();
			}
			else if (this.ElapsedSeconds < this.TimespanPreview + this.TimespanFire)
			{
				ChildLaser.Direction = (this.Angle + ((this.ElapsedSeconds - this.TimespanPreview) / this.TimespanFire) * this.DeltaAngle).ToVector2();
			}


			return this.ElapsedSeconds > this.TimespanPreview + this.TimespanFire;
		}
	}
}
