using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public class HealthBarSprite : AbstractSprite
	{
		private readonly float Height;
		private readonly AbstractSprite ReferenceSprite;

		private readonly float Width;

		public HealthBarSprite(AbstractSprite referenceSprite, Vector2 position, float width, float height) : base(position)
		{
			ReferenceSprite = referenceSprite;
			Width = width;
			Height = height;


			LayerDepth = 20;
		}

		public override void Update(GameTime gameTime)
		{
			if (ReferenceSprite.IsDeleted())
			{
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			const int borderSize = 5;
			var borderVector = new Vector2(borderSize);

			spriteBatch.Draw(Game.WhitePixel, Position, null, Color.Black, 0.0f, new Vector2(), new Vector2(Width, Height),
				SpriteEffects.None, 0.0f);
			spriteBatch.Draw(Game.WhitePixel, Position + borderVector, null, Color.Red, 0.0f, new Vector2(),
				new Vector2(Width, Height) - 2 * borderVector, SpriteEffects.None, 0.0f);

			spriteBatch.Draw(Game.WhitePixel, Position + new Vector2(borderSize, Height - borderSize), null,
				Color.Green, 0.0f, new Vector2(0, 1),
				new Vector2(Width - 2 * borderSize,
					(float) ReferenceSprite.Health / ReferenceSprite.MaxHealth * (Height - 2 * borderSize)), SpriteEffects.None,
				0.0f);
		}
	}
}