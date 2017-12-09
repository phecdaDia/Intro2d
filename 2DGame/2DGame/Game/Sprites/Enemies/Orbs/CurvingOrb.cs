using System;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	/// <summary>
	/// Orbs that slowly changes <see cref="AbstractAnimatedOrb.Direction"/> and curves
	/// </summary>
	public class CurvingOrb : AbstractOrb
	{
		public CurvingOrb(Vector2 position, Vector2 direction) : base("orb3", position, direction)
		{
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			var rot = Rotation + 0.25f / (2 * Math.PI);
			var f = new Vector2((float) Math.Cos(rot), (float) Math.Sin(rot)) * 5f;

			//Console.WriteLine("{0} - {1}", f.X, f.Y);

			return f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var players = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in players)
			{
				if (!ps.DoesCollide(this)) continue;

				ps.Damage((int)GameConstants.Difficulty);
				this.Delete();
			}
		}
	}
}