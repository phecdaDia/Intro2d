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
using Intro2DGame.Game.Pattern;
using Intro2DGame.Game.Pattern.Movement;
using Intro2DGame.Game.Pattern.Orbs;
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
		//private float OrbRotation;

		private double ElapsedSeconds;

		// This is the current state our enemy is in.

		private int BulletState, BulletHelpState;

		private readonly Queue<IPattern> Pattern;


		public LaserGuySprite(Vector2 position):base("Enemies/LSprite-0001", position)
		{

			this.MaxHealth = 750;
			this.Health = 750;
			this.Enemy = true;
			this.Persistence = true;
			this.LayerDepth = 1;

			this.Pattern = new Queue<IPattern>();
			AddStates();

		}

		protected override void AddFrames()
		{
			AddAnimation(new [] {new Point(), });
		}

		public override void Update(GameTime gameTime)
		{
			ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
			
			if (Pattern.Count == 0)
			{
				// just don't do anything this frame. This should never execute!
				Console.WriteLine($"Queue is empty, Bulletstate {BulletState}");
				BulletState = 0;

				AddStates();
				return;
			}

			if (Pattern.Peek().Execute(this, gameTime))
			{
				Pattern.Dequeue();

				if (Pattern.Count == 0)
				{
					BulletState++;

					// Add the new states. 
					AddStates();
				} // else we still have patterns left. 
			}

		}

		private void AddStates()
		{

			if (BulletState == 0)
			{
				//Pattern.Enqueue(new SingleLaserPattern());
				Pattern.Enqueue(new SleepPattern(0.5f));
			}
			else if (BulletState == 1)
			{
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
				Pattern.Enqueue(new SleepPattern(0.5f));
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
				Pattern.Enqueue(new SleepPattern(0.5f));
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
			}
			else if (BulletState == 2)
			{
				Pattern.Enqueue(new LinearMovePattern(new Vector2(0, -100), 0.5d));
			}
			else if (BulletState == 3)
			{
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
				Pattern.Enqueue(new SleepPattern(0.5f));
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
				Pattern.Enqueue(new SleepPattern(0.5f));
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));

			}
			else if (BulletState == 4)
			{
				Pattern.Enqueue(new LinearMovePattern(new Vector2(0, 200), 0.25d));
			}
			else if (BulletState == 5)
			{
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
				Pattern.Enqueue(new SleepPattern(0.5f));
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
				Pattern.Enqueue(new SleepPattern(0.5f));
				Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));

			}
			else if (BulletState == 6)
			{
				Pattern.Enqueue(new LinearMovePattern(new Vector2(0, -100), 0.5d));
			}
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - this.Position).Length() <= 16 ||
			       (position - this.Position - new Vector2(0, 16)).Length() <= 16 ||
			       (position - this.Position - new Vector2(0, 32)).Length() <= 16 ||
			       (position - this.Position + new Vector2(0, 16)).Length() <= 16 ||
			       (position - this.Position + new Vector2(0, 32)).Length() <= 16;
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
			this.Goal = player.Position;
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

