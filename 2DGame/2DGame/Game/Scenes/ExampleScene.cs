using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Scenes
{
	public class ExampleScene : Scene
	{
		public ExampleScene() : base("example")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 50)));
			//this.AddSprite(new AnimationTestSprite(new Vector2(200, 200)));
			//this.AddSprite(new RandomSpawnerSprite<OrbSprite>(1000));

			AddSprite(new DummyEnemy(new Vector2(700, 350)));
		}
	}
}