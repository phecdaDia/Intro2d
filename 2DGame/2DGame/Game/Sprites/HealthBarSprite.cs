using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public class HealthBarSprite : AbstractSprite
	{
		private readonly AbstractSprite ReferenceSprite;

		private readonly float Width;
		private readonly float Height;

		public HealthBarSprite(AbstractSprite referenceSprite, Vector2 position, float width, float height) : base(position)
		{
			this.ReferenceSprite = referenceSprite;
			this.Width = width;
			this.Height = height;


			this.LayerDepth = 20;
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

			spriteBatch.Draw(Game.WhitePixel, this.Position + new Vector2(borderSize, this.Height - borderSize), null,
				Color.Green, 0.0f, new Vector2(0, 1),
				new Vector2(this.Width - 2 * borderSize,
					((float) ReferenceSprite.Health / ReferenceSprite.MaxHealth) * (this.Height - 2 * borderSize)), SpriteEffects.None,
				0.0f);
		}
	}
}