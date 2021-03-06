﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	class flamethrowerSprite
	{
		public Texture2D flame;
		public Vector2 flamePosition;
		public Rectangle drawingRectangle;

		public bool raised_flame = false;

		public flamethrowerSprite(Texture2D newFlame, Vector2 NewflamePosition)
		{
			flame = newFlame;
			flamePosition = NewflamePosition;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!raised_flame)
			{
				drawingRectangle = new Rectangle((int)flamePosition.X, (int)flamePosition.Y, 95, 30);
				spriteBatch.Begin();
				spriteBatch.Draw(flame, drawingRectangle, Color.White);
				spriteBatch.End();
			}
			else
			{
				flamePosition.X = flamePosition.Y = -150;
				drawingRectangle = new Rectangle((int)flamePosition.X, (int)flamePosition.Y, 65, 50);
				spriteBatch.Begin();
				spriteBatch.Draw(flame, drawingRectangle, Color.White);
				spriteBatch.End();
			}
		}

		public void Update(GameTime gameTime)
		{ 
			
		}
	}
}
