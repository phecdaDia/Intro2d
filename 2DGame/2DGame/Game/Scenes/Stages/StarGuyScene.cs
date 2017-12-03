using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class StarGuyScene : Scene
	{
		private StarGuy StarGuy;

		public StarGuyScene() : base("starguy")
		{
			
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 360)));

			this.StarGuy = new StarGuy(new Vector2(1180, 360));
			AddSprite(StarGuy);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			
			FontManager.DrawString(spriteBatch, "example", new Vector2(200, 10), $"Enemy: {StarGuy.Health}");
		}
	}
	internal class StarGuy : AbstractSprite
	{
		// This is the current state our enemy is in.
		private int State = 0;

		private int HelpState = 0;

		private double ElapsedSeconds;

		// Texture is just a placeholder for now.
		public StarGuy(Vector2 position) : base("tutorialplayer", position)
		{
			this.Enemy = true;
			this.Persistence = true;

			this.MaxHealth = 2500;
			this.Health = 2500;
		}

		public override void Update(GameTime gameTime)
		{
			this.ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

			// Tries to see if the state changed. If it changed delete all orbs!
			var state = State;

			if (Health <= 500) State = 4;
			else if (Health <= 1000) State = 3;
			else if (Health <= 1500) State = 2;
			else if (Health <= 2000) State = 1;
			

			if (state != State || KeyboardManager.IsKeyDown(Keys.F1))
			{
				// Remove all orbs once
				var sprites = SceneManager.GetAllSprites();
				foreach (var keyValuePair in sprites)
				{
					if (!keyValuePair.Key.IsSubclassOf(typeof(AbstractOrb))) continue;

					foreach (AbstractOrb t in keyValuePair.Value)
					{
						t.Delete();
						// spawn a new sprite that aims at StarGuy
						SpawnSprite(new PlayerOrb(t.GetPosition(), this.GetPosition()));
					}
				}
			}

			// Now enemy movement and logic

			// Getting the player
			PlayerSprite player = SceneManager.GetSprites<PlayerSprite>().First();

			if (this.State == 0)
			{
				float deltaTime = 0.4f;

				if (ElapsedSeconds > deltaTime && HelpState == 0)
				{
					ElapsedSeconds -= deltaTime;
					SpawnSprite(new BurstOrb(this.Position, -Vector2.UnitX.AddDegrees(12.5f), 3, 0.7d));

					HelpState = 1;
				}
				else if (ElapsedSeconds > deltaTime && HelpState == 1)
				{
					ElapsedSeconds -= deltaTime;
					SpawnSprite(new BurstOrb(this.Position, -Vector2.UnitX.AddDegrees(-12.5f), 3, 0.7d));

					HelpState = 0;
				}

			}
			else if (this.State == 1)
			{
				// Movement and logic for State 1

			}
			else if (this.State == 2)
			{
				// Movement and logic for State 2

			}


		}
	}

	internal class BurstOrb : LinearOrb
	{
		private readonly int RecursiveDepth;
		private readonly double Time;

		public BurstOrb(Vector2 position, Vector2 direction, int recursiveDepth = 0, double time = 1.5f) : base(position, direction, 5f)
		{
			this.RecursiveDepth = recursiveDepth;
			this.Time = time;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (this.LifeTime.TotalGameTime.TotalSeconds > Time)
			{
				// Burst in different orbs.
				if (RecursiveDepth > 0)
				{
					double delta = 1.05d;

					//SpawnSprite(new BurstOrb(this.Position, this.Direction.AddDegrees(0d), this.RecursiveDepth - 1, Time / delta));
					SpawnSprite(new BurstOrb(this.Position, this.Direction.AddDegrees(60d), this.RecursiveDepth - 1, Time / delta));
					//SpawnSprite(new BurstOrb(this.Position, this.Direction.AddDegrees(120d), this.RecursiveDepth - 1, Time / delta));
					SpawnSprite(new BurstOrb(this.Position, this.Direction.AddDegrees(180d), this.RecursiveDepth - 1, Time / delta));
					//SpawnSprite(new BurstOrb(this.Position, this.Direction.AddDegrees(240d), this.RecursiveDepth - 1, Time / delta));
					SpawnSprite(new BurstOrb(this.Position, this.Direction.AddDegrees(300d), this.RecursiveDepth - 1, Time / delta));

				}
				else
				{
					//SpawnSprite(new LinearOrb(this.Position, this.Direction.AddDegrees(0d), this.Speed));
					SpawnSprite(new LinearIncreasingOrb(this.Position, this.Direction.AddDegrees(60d), this.Speed, 1.005f));
					//SpawnSprite(new LinearOrb(this.Position, this.Direction.AddDegrees(120d), this.Speed));
					SpawnSprite(new LinearIncreasingOrb(this.Position, this.Direction.AddDegrees(180d), this.Speed, 1.005f));
					//SpawnSprite(new LinearOrb(this.Position, this.Direction.AddDegrees(240d), this.Speed));
					SpawnSprite(new LinearIncreasingOrb(this.Position, this.Direction.AddDegrees(300d), this.Speed, 1.005f));
				}

				this.Delete();
			}

		}
	}

	internal static class ExtensionMethods
	{
		internal static double ToAngle(this Vector2 vector) => Math.Atan2(vector.Y, vector.X);
		internal static Vector2 ToVector2(this double degrees) => new Vector2((float)Math.Cos(degrees), (float)Math.Sin(degrees));

		internal static Vector2 AddDegrees(this Vector2 vector, double degrees) => (vector.ToAngle() + degrees.ToDegrees()).ToVector2();
		internal static double ToDegrees(this double number) => number / 360d * 2 * Math.PI;

	}
}
