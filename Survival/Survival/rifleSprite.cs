using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival
{
	public class rifleSprite
	{
		public Texture2D rifle;
		public Vector2 riflePosition;
		public Rectangle drawingRectangle;
		int time_reload = 0;
		int openfire = 0;
		public bool l_reload = false;

		bulletLogic bullet = new bulletLogic();

		public bool raised_rifle = false; // поднял оружие

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
			time_reload += 100;
			l_reload = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!raised_rifle)
			{
				drawingRectangle = new Rectangle((int)riflePosition.X, (int)riflePosition.Y, 75, 30);
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

		public void Update(GameTime gameTime, List<bulletSprite> bullets, bool reload)
		{
			if (raised_rifle)
			{
				if (bullets.Count == 30)
				{
					Reload(gameTime);
					bullets.Clear();
				}
				if (l_reload)
				{
					if (openfire < time_reload)
					{
						reload = true;
						openfire++;
					}
					else
					{
						reload = false;
						l_reload = false;
					}
				}
			}
			
		}
	}
}
