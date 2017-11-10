using System;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites
{
	public class OrbSprite : AbstractSprite
	{
		private Vector2 LastMovement;
		// I'll just use this sprite to test things for now.


		public OrbSprite(Vector2 position) : base("orb", position)
		{
			var r = new Random();
			Hue = new Color(r.Next(0xFF), r.Next(0xFF), r.Next(0xFF));
		}

		public override void Update(GameTime gameTime)
		{
			var bufferedMovement = new Vector2();
			var playerList = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in playerList)
			{
				var dist = ps.GetPosition() - Position;

				bufferedMovement += dist / (float) Math.Pow(dist.Length(), 1.5);
			}

			bufferedMovement *= 0.9992f; // Basically drag
			LastMovement += bufferedMovement;
			Position += LastMovement;

			// Prevents player from leaving the screen
			if (Position.X + Texture.Width / 2 > Game.GraphicsArea.X) Position.X = Game.GraphicsArea.X - Texture.Width / 2;
			if (Position.Y + Texture.Height / 2 > Game.GraphicsArea.Y) Position.Y = Game.GraphicsArea.Y - Texture.Height / 2;
			if (Position.X - Texture.Width / 2 < 0) Position.X = Texture.Width / 2;
			if (Position.Y - Texture.Height / 2 < 0) Position.Y = Texture.Height / 2;
		}
	}
}