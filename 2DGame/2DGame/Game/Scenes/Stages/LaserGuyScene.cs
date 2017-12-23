using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Intro2DGame.Game.Fonts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class LaserGuyScene : Scene
	{
		private LaserGuySprite LaserGuy;
		public LaserGuyScene() : base("laserguy")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 360)));
			this.LaserGuy=new LaserGuySprite(new Vector2(1180, 360));
			AddSprite(this.LaserGuy);

		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			FontManager.DrawString(spriteBatch, "example", new Vector2(200, 10), $"Gegner:{LaserGuy.Health}");
		}
	}

	internal class LaserGuySprite : AbstractAnimatedSprite
	{
		private float OrbRotation;

		private double ElapsedStateSeconds, ElapsedBulletSeconds;

		// This is the current state our enemy is in.

		private int BulletState, State, BulletHelpState, HelpState;

		private const int TOTAL_STATES = 3;
		private const int TOTAL_BULLET_STATES = 2;


		public LaserGuySprite(Vector2 position):base("Enemies/LSprite-0001", position)
		{

			this.MaxHealth = 750;
			this.Health = 750;
			this.Enemy = true;
			this.Persistence = true;
			this.LayerDepth = 1;
		}

		protected override void AddFrames()
		{
			AddAnimation(new Point[] {new Point(), });
		}

		public override void Update(GameTime gameTime)
		{
			ElapsedStateSeconds += gameTime.ElapsedGameTime.TotalSeconds;
			ElapsedBulletSeconds += gameTime.ElapsedGameTime.TotalSeconds;

			UpdateState(gameTime);
			UpdateBulletState(gameTime);
			
		}

		private void UpdateState(GameTime gameTime)
		{

			
		}

		private void UpdateBulletState(GameTime gameTime)
		{
			var player = SceneManager.GetSprites<PlayerSprite>().First();

			if (BulletState == 0)
			{
				//Shoots a ring of bullets
				if (ElapsedBulletSeconds <= 0.4f) return;
				ElapsedBulletSeconds -= 0.4f;

				var totalBullets = 24;
				var increment = 360f / totalBullets;
				var direction = -Vector2.UnitX;

				if ((BulletHelpState & 1) == 1) direction = direction.AddDegrees(0.5f * increment);

				for (var i = 0; i < totalBullets; i++)
					SpawnSprite(new LinearOrb(this.Position, direction.AddDegrees(i * increment), 3.5f));

				BulletHelpState++;

				if (BulletHelpState < 2) return;

				BulletState = 1;
				BulletHelpState = 0;
			}
			else if (BulletState == 1)
			{
				SpawnSprite(new LinearTracer1(this.Position, -Vector2.UnitX.AddDegrees(35d)));
				SpawnSprite(new LinearTracer1(this.Position, -Vector2.UnitX.AddDegrees(-35d)));

				BulletState = 0;
			}

			//while (true)
			//{
			//	var player = SceneManager.GetSprites<PlayerSprite>().First();

			//	if (BulletState == 0)
			//	{
			//		// Shoots a ring of bullets
			//		if (ElapsedBulletSeconds <= 0.4f) return;
			//		ElapsedBulletSeconds -= 0.4f;

			//		var totalBullets = 24;
			//		var increment = 360f / totalBullets;
			//		var direction = -Vector2.UnitX;

			//		if ((BulletHelpState & 1) == 1) direction = direction.AddDegrees(0.5f * increment);

			//		for (var i = 0; i < totalBullets; i++)
			//			SpawnSprite(new LinearOrb(this.Position, direction.AddDegrees(i * increment), 3.5f));

			//		BulletHelpState++;

			//		if (BulletHelpState < 2) return;
			//	}
			//	else if (BulletState == 1)
			//	{
			//		// shoots a barrage
			//		// does not have a cooldown.
			//		var direction = player.GetPosition() - this.Position;

			//		for (var i = -4; i <= 4; i++)
			//			SpawnSprite(new LinearOrb(this.Position, direction.AddDegrees(-0.5f * i), 5f));

			//		do
			//			BulletState = GameConstants.Random.Next(TOTAL_BULLET_STATES);
			//		while (BulletState == 1);

			//		// Execute the next state in the same frame.
			//		continue;
			//	}
			//	else if (BulletState == 2)
			//	{

			//	}

			//	BulletState = GameConstants.Random.Next(TOTAL_BULLET_STATES);
			//	break;
			//}
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - this.GetPosition()).Length() <= 16 ||
			       (position - this.GetPosition() - new Vector2(0, 16)).Length() <= 16 ||
			       (position - this.GetPosition() - new Vector2(0, 32)).Length() <= 16 ||
			       (position - this.GetPosition() + new Vector2(0, 16)).Length() <= 16 ||
			       (position - this.GetPosition() + new Vector2(0, 32)).Length() <= 16;
		}
	}

	internal class LinearTracer1 : AbstractSprite
	{
		private readonly Vector2 Direction;
		private double ElapsedSeconds, TotalElapsedSeconds;

		private readonly Vector2 Goal;

		public LinearTracer1(Vector2 position, Vector2 direction) : base(position)
		{
			this.Direction = direction;

			var player = SceneManager.GetSprites<PlayerSprite>().First();
			this.Goal = player.GetPosition();
		}

		public override void Update(GameTime gameTime)
		{
			this.Position += Direction * 5f;
			this.ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
			this.TotalElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;


			if (ElapsedSeconds >= 1.5f)
			{
				ElapsedSeconds -= 1.5f;

				SpawnSprite(new LaserOrb(this.Position, this.Goal - this.Position, (float) (3f - TotalElapsedSeconds), 0.5f));
			}

			if (TotalElapsedSeconds >= 3.0f)
			{
				this.Delete();
				return;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.Draw(Game.WhitePixel, this.Position, Color.Red);
		}
	}
}

