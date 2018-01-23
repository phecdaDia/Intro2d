using System.Linq;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Orbs
{
	public class SingleLaserPattern : IPattern
	{
		private readonly Vector2 Direction, Position;

		private readonly float ChargeTime, FireTime;

		public SingleLaserPattern(Vector2 position, double rotation = 0.0d, float chargeTime = 1.0f, float fireTime = 2.5f)
		{
			Direction = rotation.ToRadiants().ToVector2();

			this.ChargeTime = chargeTime;
			this.FireTime = fireTime;
			this.Position = position;
		}

		public SingleLaserPattern(SingleLaserPatternTarget target, AbstractSprite host, float chargeTime = 1.0f, float fireTime = 2.5f)
		{
			if (target == SingleLaserPatternTarget.PlayerSprite)
			{
				var player = SceneManager.GetSprites<PlayerSprite>().First();
				this.Direction = host.Position - player.Position;
			}

			this.ChargeTime = chargeTime;
			this.FireTime = fireTime;
			this.Position = host.Position;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var player = SceneManager.GetSprites<PlayerSprite>().First();
			SceneManager.GetCurrentScene().BufferedAddSprite(new LaserOrb(Position, Direction, ChargeTime, FireTime));
			return true;
		}
	}

	public enum SingleLaserPatternTarget
	{
		PlayerSprite,
	}
}