using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes.Transition
{
	/// <summary>
	/// Example transition
	/// This transition displays a black screen for the duration specified in the constructor
	/// </summary>
	public class TestTransition : AbstractTransition
	{
		private readonly int Milliseconds;
		private int Elapsed = 0;
		private Texture2D Texture;

		public TestTransition(int milliseconds)
		{
			this.Milliseconds = milliseconds;
		}

		protected override void CreateScene()
		{}

		public override void Update(GameTime gameTime)
		{
			this.Elapsed += gameTime.ElapsedGameTime.Milliseconds;

			if (Elapsed >= Milliseconds / 2) RunLambda();

			if (Elapsed >= Milliseconds) SceneManager.CloseTransition();
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch spriteBatch)
		{
			// Drawing the texture over the entire graphics area. 
			// Recoloring it to be black with 255 alpha.
			var alpha = 1f - Math.Abs(Milliseconds / 2f - Elapsed) / (Milliseconds / 2f);

			spriteBatch.Draw(this.Texture, new Rectangle(new Point(), Game.RenderSize), new Color(Color.Black, alpha));
		}

		/// <inheritdoc />
		public override void LoadContent()
		{
			// Creating a 1x1 pixel texture that is white so we can recolor it in the spriteBatch.Draw();
			Texture = new Texture2D(Game.GetInstance().GraphicsDevice, 1, 1);
			Texture.SetData(new [] { Color.White });
		}

		/// <inheritdoc />
		public override void UnloadContent()
		{
			Texture.Dispose();
		}
	}
}
