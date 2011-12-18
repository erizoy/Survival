using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	class rifleSprite
	{
		public Texture2D rifle;
		public Vector2 riflePosition;
		public Rectangle drawingRectangle;

		bulletLogic bullet = new bulletLogic();

		public bool raised = false; // поднял оружие

		public rifleSprite(Texture2D newRifle, Vector2 NewriflePosition)
		{
			rifle = newRifle;
			riflePosition = NewriflePosition;
		}

		public void Reload()
		{
			raised = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!raised)
			{
				drawingRectangle = new Rectangle((int)riflePosition.X, (int)riflePosition.Y, 65, 50);
				spriteBatch.Begin();
				spriteBatch.Draw(rifle, drawingRectangle, Color.White);
				spriteBatch.End();
			}
			else
			{
				drawingRectangle = new Rectangle(-100, -100, 65, 50);
				spriteBatch.Begin();
				spriteBatch.Draw(rifle, drawingRectangle, Color.White);
				spriteBatch.End();
			}
		}

		public void Update(GameTime gameTime, List<bulletSprite> bullets, int Count)
		{
			if (raised)
			{
				if (bullets.Count == 30)
				{
					Reload();
					bullets.Clear();
				}
			}
		}
	}
}
