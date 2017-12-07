using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
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

		private double ElapsedSeconds;

		// This is the current state our enemy is in.
		//private int State = 0;

		private int HelpState;


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
			//var MoveMent = new Vector2();
			ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;
			
			var sprites = SceneManager.GetAllSprites();
			foreach (var k in sprites)
			{
				if (!k.Key.IsSubclassOf(typeof(PlayerOrb))) continue;
				foreach (AbstractOrb t in k.Value)
					t.Delete();
			}

			if(ElapsedSeconds > 0.25f)
			{
				ElapsedSeconds %= 0.25f;

				HelpState++;
				if(HelpState > 3)
				{
					HelpState = 0;
					SpawnSprite(new LaserOrb(Position, SceneManager.GetSprites<PlayerSprite>().First().GetPosition() - this.GetPosition()));
				}
				OrbRotation+= (float)Math.PI / 36f;
				if (OrbRotation > (float)Math.PI) OrbRotation = 0f;
				for (int i=0;i<12;++i)
				{
					SpawnSprite(new LinearOrb(Position, new Vector2((float)Math.Sin(Math.PI/6*i+ OrbRotation), (float)Math.Cos(Math.PI / 6 * i+ OrbRotation)),3.0f));
				}
			}
		}
		public override bool DoesCollide(Vector2 position)
		{
			return (position - this.GetPosition()).Length() <= 16 ||
				   (position - this.GetPosition() - new Vector2(0, 16)).Length() <= 16 ||
				   (position - this.GetPosition() + new Vector2(0, 16)).Length() <= 16;
		}
	}
}

