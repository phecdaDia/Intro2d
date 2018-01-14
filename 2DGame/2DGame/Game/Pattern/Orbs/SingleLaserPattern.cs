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
		private readonly Vector2 Direction;

		public SingleLaserPattern(double rotation = 0.0d)
		{
			Direction = rotation.ToVector2();
		}

		public SingleLaserPattern(SingleLaserPatternTarget target, AbstractSprite host)
		{
			if (target == SingleLaserPatternTarget.PlayerSprite)
			{
				var player = SceneManager.GetSprites<PlayerSprite>().First();
				this.Direction = host.Position - player.Position;
			}
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var player = SceneManager.GetSprites<PlayerSprite>().First();
			SceneManager.GetCurrentScene().BufferedAddSprite(new LaserOrb(host.Position, Direction));
			return true;
		}
	}

	public enum SingleLaserPatternTarget
	{
		PlayerSprite,
	}
}