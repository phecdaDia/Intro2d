using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes
{
	public class DialogScene : Scene
	{
		private readonly string DialogText;

		private static readonly Random Random = new Random();

		/// <summary>
		/// Dialog textbox
		/// The scenekey has to be unique. Therefor we generate some random numbers.
		/// </summary>
		/// <param name="dialogText">Text to be displayed</param>
		public DialogScene(string dialogText) : base($"dialog-{Random.Next(0x7fffffff):X08}-{DateTime.Now.Ticks:X016}")
		{
			this.DialogText = dialogText;
		}

		protected override void CreateScene()
		{
			AddSprite(new DialogBoxSprite(this.DialogText));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (SceneManager.GetCurrentScene().SceneKey != this.SceneKey) return;

			base.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			if (SceneManager.GetCurrentScene().SceneKey != this.SceneKey) return;

			base.Update(gameTime);
		}
	}

	internal class DialogBoxSprite : AbstractSprite
	{
		private readonly string DialogText;

		/// <summary>
		/// Basic sprite that displays the dialogbox with text
		/// </summary>
		/// <param name="dialogText">Text to be displayed</param>
		public DialogBoxSprite(string dialogText) : base("title", new Vector2(400, 400))
		{
			this.DialogText = dialogText;
		}

		public override void Update(GameTime gameTime)
		{
			if (KeyboardManager.IsKeyDown(Keys.Enter)) SceneManager.CloseScene();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			spriteBatch.DrawString(Game.FontArial, this.DialogText, this.Position - new Vector2(300, 25), Color.Black);
		}
	}
}
