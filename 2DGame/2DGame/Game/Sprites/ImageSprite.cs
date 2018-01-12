using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites
{
	/// <summary>
	///     <see cref="AbstractSprite" /> that displays a static image
	/// </summary>
	public class ImageSprite : AbstractSprite
	{
		public ImageSprite(string key, Vector2 position) : base(key, position)
		{
		}

		/// <summary>
		///     <see cref="ImageSprite" /> don't Update.
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
		}
	}
}