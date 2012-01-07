using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	class subgunSprite
	{
		public Texture2D subgun;
		public Vector2 subgunPosition;
		public Rectangle drawingRectangle;
		int time_reload = 0;
		int openfire = 0;
		public bool l_reload = false;
		public int damage = 50;

		public bool raised_rifle = false; // поднял оружие

		public subgunSprite(Texture2D newSubGun, Vector2 NewsubgunPosition)
		{
			subgun = newSubGun;
			subgunPosition = NewsubgunPosition;
		}

		public subgunSprite()
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
				drawingRectangle = new Rectangle((int)subgunPosition.X, (int)subgunPosition.Y, 55, 35);
				spriteBatch.Begin();
				spriteBatch.Draw(subgun, drawingRectangle, Color.Gray);
				spriteBatch.End();
			}
			else
			{
				subgunPosition.X = subgunPosition.Y = -100;
				drawingRectangle = new Rectangle((int)subgunPosition.X, (int)subgunPosition.Y, 65, 50);
				spriteBatch.Begin();
				spriteBatch.Draw(subgun, drawingRectangle, Color.White);
				spriteBatch.End();
			}
		}

		public void Update(GameTime gameTime, List<bulletSprite> bullets, bool reload)
		{
			if (raised_rifle)
			{
				if (bullets.Count == 40)
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
