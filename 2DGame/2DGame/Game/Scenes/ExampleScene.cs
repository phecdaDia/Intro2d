using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
    public class ExampleScene : Scene
    {
        public ExampleScene() : base("example")
        {

        }

		protected override void CreateScene()
        {
            this.AddSprite(new PlayerSprite(new Vector2(100, 50)));
			//this.AddSprite(new AnimationTestSprite(new Vector2(200, 200)));
			//this.AddSprite(new RandomSpawnerSprite<OrbSprite>(1000));

			this.AddSprite(new DummyEnemy(new Vector2(200, 100)));
        }
    }
}
