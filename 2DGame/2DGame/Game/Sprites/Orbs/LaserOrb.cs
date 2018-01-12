using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Orbs
{
	public class LaserOrb : AbstractOrb
	{
		private readonly float ChargeSpan;
		private readonly float LifeSpan;
		private bool Used;

		public LaserOrb(Vector2 position, Vector2 direction, float chargeSpan = 1.0f, float lifeSpan = 2.5f) : base(
			"OrbLaser", position, direction,
			new Point(32, 8))
		{
			ChargeSpan = chargeSpan;
			LifeSpan = lifeSpan + chargeSpan;

			Scale.X = 100;
			Used = false;
			Hue = Color.Red;
		}

		protected override void AddFrames()
		{
			AddAnimation(new[]
			{
				new Point(0, 0),
				new Point(32, 0)
			});
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			return Vector2.Zero;
		}

		public override void Update(GameTime gameTime)
		{
			//base.Update(gameTime);

			if (LifeTime.TotalGameTime.TotalSeconds > LifeSpan)
			{
				Delete();
			}
			else if (LifeTime.TotalGameTime.TotalSeconds > ChargeSpan && !Used)
			{
				CurrentFrame = 1;

				foreach (var player in SceneManager.GetSprites<PlayerSprite>())
				{
					//float Inc(Vector2 t) => t.X / t.Y;
					//var j = Inc(Direction) / Inc(player.GetPosition() - this.GetPosition());

					//// This is some black magic. TODO Will change this later
					////if (j > 0.5858f && j < 1.4142f) player.Damage(GameConstants.PLAYER_DAMAGE);

					//var invDir = new Vector2(Direction.Y, Direction.X * -1.0f);

					var ex = Position.X;
					var ey = Position.Y;

					var px = player.Position.X;
					var py = player.Position.Y;

					var dx = Direction.X;
					var dy = Direction.Y;

					var d2x = Direction.Y;
					var d2y = Direction.X * -1.0f;

					// (1): ex + a dx = px + b d'x
					// (2): ey + a dy = py + b d'y

					// solving for a:

					// (1) - d2q (2)
					var d2q = d2x / d2y;

					// ex - d2q ey + a dx - a dy d2q = px - py d2q + 0
					// ex - d2q ey + a (dx - dy d2q) = px - py d2q
					// a (dx - dy d2q) = px - py d2q - ex + d2q ey
					// a = (px - py d2q - ex + d2q ey)/(dx - dy d2q)
					var a = (px - py * d2q - ex + d2q * ey) / (dx - dy * d2q);

					var q = Position + a * Direction;

					//Console.WriteLine($"Distance: {(player.GetPosition() - q).Length()}");

					// Change this to different hitboxes. 
					if (player.DoesCollide(q))
					{
						player.Damage((int) GameConstants.Difficulty);
						Used = true;
					}
				}
			}
		}
	}
}