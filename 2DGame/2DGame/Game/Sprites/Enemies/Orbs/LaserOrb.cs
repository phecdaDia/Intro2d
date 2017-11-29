using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class LaserOrb : AbstractOrb
	{
		protected int Time = 0;
		protected static Texture2D FirstTexture2D;
		protected static Texture2D SecondTexture2D;
		//protected Vector2 LaserPosition1;
		//protected Vector2 LaserPosition2;
		public Boolean AfterDamage;
		public LaserOrb(Vector2 Position, Vector2 Direction) : base("OrbLaser", Position, Direction, 100, new Point(32, 8))
		{
			Scale.X = 50;
			Scale.Y = 1;
			AfterDamage = false;
			Hue = Color.Blue;
			
			
		}

		protected override void AddFrames()
		{
			AddAnimation(new Point[]
			{
				new Point(0, 0),
				new Point(32, 0),
			});
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			return Position;

		}
		public override void Update(GameTime gameTime)
		{

			Time++;
			if (Time > 90) this.Delete();
			if (Time > 60 && !AfterDamage)
			{
				var Players = SceneManager.GetSprites<PlayerSprite>();
				foreach (var Player in Players)
				{
					if (!Player.DoesCollide(this)) continue;
					Player.Damage(GameConstants.PLAYER_DAMAGE);
					AfterDamage = true;
				}
				return;
			}

		}
		
	}
}
