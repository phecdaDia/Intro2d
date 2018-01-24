using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Pattern;
using Intro2DGame.Game.Pattern.LaserGuy;
using Intro2DGame.Game.Pattern.Movement;
using Intro2DGame.Game.Pattern.Orbs;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class LaserGuyScene : Scene
	{
		private bool ShownDialog = false;

		public LaserGuyScene() : base("laserguy")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(250, 750)));
			AddSprite(new LaserGuySprite(new Vector2(250, 150)));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var lg = SceneManager.GetSprites<LaserGuySprite>().FirstOrDefault();

			if ((lg?.Health ?? 0) == 0 && !ShownDialog)
			{
				SceneManager.AddScene(new DialogScene("*** PLACEHOLDER TEXT *** YOU ARE WINNER"));
				ShownDialog = true;

				return;
			}
			else if (ShownDialog)
			{
				SceneManager.CloseScene(new TestTransition(1.0d));
				return;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			var lg = SceneManager.GetSprites<LaserGuySprite>().FirstOrDefault();
			if (lg != null)
			{
				var anchor = new Vector2(250, 150);

				var temp = 0.5f * (lg.Position - anchor);
				var rotation = temp.ToAngle();
				rotation += MathHelper.PiOver2 + MathHelper.Pi;
				var scale = new Vector2(5, (lg.Position - anchor).Length());
				spriteBatch.Draw(Game.WhitePixel, anchor, null, Color.DarkRed, (float)rotation, new Vector2(0.5f, 0), scale, SpriteEffects.None, 0.0f);
			}


			// REMOVE THIS ON RELEASE

			//FontManager.DrawString(spriteBatch, "example", new Vector2(510, 10), $"Gegner:{lg?.Health ?? 0}");
			base.Draw(spriteBatch);
		}
	}

	internal class LaserGuySprite : AbstractAnimatedSprite
	{
		private readonly Queue<IPattern> Pattern;
		// This is the current state our enemy is in.

		private int BulletState;
		private int Repeats;


		public LaserGuySprite(Vector2 position) : base("Enemies/LSprite-0001", position)
		{
			MaxHealth = 750;
			Health = 750;
			Enemy = true;
			Persistence = true;
			LayerDepth = 1;

			Pattern = new Queue<IPattern>();
			Pattern.EnqueueMany(AddStates());

			SceneManager.GetCurrentScene().AddSprite(new HealthBarSprite(this, new Vector2(510, 50), 50, 800));
			SceneManager.GetCurrentScene().AddSprite(new HealthBar2Sprite(HealthBarPosition.ScreenTop, this));
		}

		protected override void AddFrames()
		{
			AddAnimation(new[] {new Point()});
		}

		public override void Update(GameTime gameTime)
		{
			if (Pattern.Count == 0)
			{
				// just don't do anything this frame. This should never execute!
				Console.WriteLine($"Queue is empty, Bulletstate {BulletState}");
				BulletState = 0;
				++Repeats;

				Pattern.EnqueueMany(AddStates());
			}

			ExecutePattern(gameTime);
		}

		private void ExecutePattern(GameTime gameTime)
		{
			while (Pattern.Count > 0)
			{
				var success = Pattern.Peek().Execute(this, gameTime);

				if (success)
				{
					Pattern.Dequeue();

					if (Pattern.Count != 0) continue;

					BulletState++;

					// Add the new states. 
					var patterns = AddStates();

					Pattern.EnqueueMany(patterns);
				}
				else
				{

					break;
				}
			}
		}
		
		private IPattern[] AddStates()
		{

			float SPEED = 450 + 50 * this.Repeats;

			switch (BulletState)
			{
				case 0: // Do this pattern twice. 
				case 1:
				case 7: // and we do this pattern again sometimes
				case 8:
					return new IPattern[]
					{
						new SleepPattern(0.25d),
						new BarragePattern(5.0f, 9 + 2 * Repeats, 45.0d, 90.0d),
						LinearMovementPattern.GenerateFromVector2(new Vector2(100, 50), SPEED),
						new CircleLinearPattern(2.0f, 36 / (Repeats + 1) , 0.0d),
						LinearMovementPattern.GenerateFromVector2(new Vector2(-200, 0), SPEED),
						new CircleLinearPattern(2.5f, 36 / (Repeats + 1), 0.0d),
						LinearMovementPattern.GenerateFromVector2(new Vector2(100, -50), SPEED),
						new SleepPattern(0.25d),
					};
				case 2:
					return new IPattern[]
					{
						// I'm sorry for these nested patterns
						new SleepPattern(0.5d),
						new TandemPattern(
							//new SingleLaserPattern(90.0d, 1.25f, 2.5f), 
							new SweepingLaserPattern(this.Position, 112.5d, -45d, 1.5f - 0.1f * Repeats, 0.75f),
							new SequencePattern(
								new SleepPattern(1.0f - 0.1f * Repeats),
								new BarragePattern(5.0f, 16 + 2 * Repeats, 112.5d, 315d)
							)
						)
					};
				case 3:
				case 4:
					return new IPattern[]
					{
						new SleepPattern(0.5d),
						new RadialMovementPattern(this.Position, new Vector2(0, -100), -90.0d, 0.4d),
						new BarragePattern(2.0f, 6 + 2 * Repeats, 101.25d, -56.25d),
						new TandemPattern( // This creates an ellipse
							new RadialMovementPattern(this.Position, new Vector2(-50, 0), 180.0d, 0.6d),
							new RadialMovementPattern(this.Position, new Vector2(-50, 0), 180.0d, 0.6d)
						),
						new BarragePattern(2.0f, 6 + 2 * Repeats, 78.75d, 56.25d),
						new RadialMovementPattern(this.Position, new Vector2(0, 100), 90.0d, 0.4d),

					};
				case 5:
					return new IPattern[]
					{
						// I am, again, so sorry for this heavy nesting.
						new TandemPattern(
							new SequencePattern(
								new CircleLinearPattern(4.0f, 36.0d / (Repeats + 1) , 0.0d),
								new SleepPattern(0.75d),
								new CircleLinearPattern(4.0f, 36.0d / (Repeats + 1), 18.0d),
								new SleepPattern(0.75d),
								new CircleLinearPattern(4.0f, 36.0d / (Repeats + 1), 0.0d),
								new SleepPattern(0.75d),
								new CircleLinearPattern(4.0f, 36.0d / (Repeats + 1), 18.0d),
								new SleepPattern(0.75d),
								new CircleLinearPattern(4.0f, 36.0d / (Repeats + 1), 0.0d),
								new SleepPattern(0.75d),
								new CircleLinearPattern(4.0f, 36.0d / (Repeats + 1), 18.0d),
								new SleepPattern(0.75d)
							),
							new SequencePattern( // it looks horrible, but works
								new SingleLaserPattern(new Vector2(10, 25)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 75)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 125)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 175)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 225)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 275)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 325)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 375)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 425)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 475)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 525)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 575)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 625)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 675)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 725)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 775)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 825)),
								new SleepPattern(0.25),
								new SingleLaserPattern(new Vector2(10, 875))
							)
						)
					};
				case 6:
					return new IPattern[]
					{
						LinearMovementPattern.GenerateFromVector2(new Vector2(0, 200), SPEED),
						new CircleLinearPattern(2.0f, 36.0d, 0.0d),
						new TandemPattern(
							new RadialMovementPattern(this.Position, new Vector2(0, 25), -180d, 0.4d),
							new RadialMovementPattern(this.Position, new Vector2(0, 25), -180d, 0.4d)
						),
						new CircleLinearPattern(2.0f, 36.0d, 0.0d),
						new SleepPattern(0.1d),
						new CircleLinearPattern(2.0f, 36.0d, 18.0d),
						new TandemPattern(
							new RadialMovementPattern(this.Position, new Vector2(0, 25), 180d, 0.4d),
							new RadialMovementPattern(this.Position, new Vector2(0, 25), 180d, 0.4d)
						),
						new CircleLinearPattern(2.0f, 36.0d, 0.0d),
						new SleepPattern(0.1d),
						new CircleLinearPattern(2.0f, 36.0d, 18.0d),
						new BarragePattern(5.0f, 10, 67.5d, 45.0d), 

					};
				case 9:
					return new IPattern[]
					{
						new TandemPattern(
							new SweepingLaserPattern(new Vector2(250, 0), 0.0d, 80.0d, 1.5f, 1f),
							new SweepingLaserPattern(new Vector2(250, 0), 0.0d, -80.0d, 1.5f, 1f),
							new SequencePattern(
								new SleepPattern(0.25f),
								new BarragePattern(2.0f, 10, 45d, 90d)
							)
						), 
					};
				case 10:
					return new IPattern[]
					{
						new BarragePattern(2.0f, 10, 45d, 90d),
						new BarragePattern(3.0f, 10, 45d, 90d),
						new BarragePattern(4.0f, 10, 45d, 90d),
						LinearMovementPattern.GenerateFromVector2(new Vector2(0, 100), SPEED),
						new BarragePattern(2.0f, 10, 45d, 90d),
						new BarragePattern(3.0f, 10, 45d, 90d),
						new BarragePattern(4.0f, 10, 45d, 90d),
						LinearMovementPattern.GenerateFromVector2(new Vector2(0, -100), SPEED),
					};
				case 11: // damages himself
					return new IPattern[]
					{
						new SleepPattern(2.75f),
						new LambdaPattern((host, time) =>
						{

							var lg = SceneManager.GetSprites<LaserGuySprite>().FirstOrDefault();
							if (lg == null) return;

							foreach (var spriteDictionaryKeys in SceneManager.GetAllSprites().Keys)
							{
								foreach (AbstractSprite sprite in SceneManager.GetAllSprites()[spriteDictionaryKeys])
								{
									if (!sprite.GetType().IsSubclassOf(typeof(AbstractOrb)))
									{
										break;
									}

									SceneManager.GetCurrentScene().BufferedAddSprite(new PlayerOrb(sprite.Position, lg.Position - sprite.Position));
									sprite.Delete();

								}
							}
						}),
						new SleepPattern(1.25f),
					};
				default:
					return new IPattern[0];
			}
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - Position).Length() <= 16 ||
			       (position - Position - new Vector2(0, 16)).Length() <= 16 ||
			       (position - Position - new Vector2(0, 32)).Length() <= 16 ||
			       (position - Position + new Vector2(0, 16)).Length() <= 16 ||
			       (position - Position + new Vector2(0, 32)).Length() <= 16;
		}
	}
}