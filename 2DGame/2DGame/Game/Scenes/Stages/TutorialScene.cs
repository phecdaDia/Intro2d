using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class TutorialScene : Scene
	{
		public TutorialScene() : base("tutorial")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(600, 500)));
			AddSprite(new TutorialSprite());
		}
	}

	internal class TutorialSprite : AbstractSprite
	{
		public override void Update(GameTime gameTime)
		{
		}
	}
}