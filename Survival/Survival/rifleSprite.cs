using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	public class rifleSprite
	{
		public Texture2D rifle;
		public Vector2 riflePosition;
		public Rectangle drawingRectangle;
		public int time_reload;
		public bool l_reload = false;

		bulletLogic bullet = new bulletLogic();

		public bool raised = false; // поднял оружие

		public rifleSprite(Texture2D newRifle, Vector2 NewriflePosition)
		{
			rifle = newRifle;
			riflePosition = NewriflePosition;
		}

		public rifleSprite()
		{ 
			
		}

		public void Reload(GameTime gameTime)
		{
			time_reload = (int)gameTime.ElapsedGameTime.Seconds + 2;
			l_reload = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!raised)
			{
				drawingRectangle = new Rectangle((int)riflePosition.X, (int)riflePosition.Y, 85, 50);
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

		public void Update(GameTime gameTime, List<bulletSprite> bullets)
		{
			if (raised)
			{
				if (bullets.Count == 30)
				{
					Reload(gameTime);
					bullets.Clear();
				}
				if (l_reload)
				{
					if (gameTime.ElapsedGameTime.Seconds < time_reload)
						raised = false;
					else
					{
						raised = true;
						l_reload = false;
					}
				}
			}
			
		}
	}
}
