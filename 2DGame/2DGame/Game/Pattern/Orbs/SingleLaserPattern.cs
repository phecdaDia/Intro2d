using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern.Orbs
{
	public class SingleLaserPattern : IPattern
	{
		private readonly double Rotation;

		public SingleLaserPattern(double rotation = 0.0d)
		{
			this.Rotation = rotation;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			var player = SceneManager.GetSprites<PlayerSprite>().First();
			SceneManager.GetCurrentScene().BufferedAddSprite(new LaserOrb(host.Position, this.Rotation.ToVector2()));
			return true;
		}
	}
}