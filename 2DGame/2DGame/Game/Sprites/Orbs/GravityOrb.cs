using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Orbs
{

	public class GravityOrb : AbstractOrb
	{
		private Vector2 LastVector2;


		public GravityOrb(Vector2 position, Vector2 initialVelocity = new Vector2()) : base("orb3", position, new Vector2())
		{
			this.LastVector2 = initialVelocity;

			this.Persistence = true;
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			// get the player
			var player = SceneManager.GetSprites<PlayerSprite>().First();

			var temp = player.Position - this.Position;
			temp.Normalize();
			temp *= 300.0f / (player.Position - this.Position).Length();

			temp *= (float)gameTime.ElapsedGameTime.TotalSeconds;

			LastVector2 = LastVector2 + temp;
			return LastVector2;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var players = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in players)
			{
				if (!ps.DoesCollide(this)) continue;

				ps.Damage((int)GameConstants.Difficulty);
				Delete();
			}
		}
	}
}
