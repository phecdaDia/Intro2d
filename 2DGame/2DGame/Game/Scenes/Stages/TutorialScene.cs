using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Scenes.Stages
{
    /// <summary>
    /// This scene is used for the tutorial
    /// </summary>
	public class TutorialScene : Scene
	{
		public TutorialScene() : base("tutorial")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 350)));
			AddSprite(new TutorialSprite());
		}
	}

    /// <summary>
    /// Main control sprite for the tutorial
    /// </summary>
	internal class TutorialSprite : AbstractSprite
	{


		public override void Update(GameTime gameTime)
		{
		}
	}
}