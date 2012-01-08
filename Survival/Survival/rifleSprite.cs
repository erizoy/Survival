using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Survival
{
	public class rifleSprite
	{
		public Texture2D rifle;
		public Vector2 riflePosition;
		public SoundEffectInstance rifleshot;
		public Rectangle drawingRectangle;
		public int time_reload = 120;
		int openfire = 0;
		public bool l_reload = false;
		public int countammo = 30;
		public int damage = 100;

		public bool raised_rifle = false; // поднял оружие

		public rifleSprite(Texture2D newRifle, Vector2 NewriflePosition, SoundEffectInstance newRifleShot)
		{
			rifle = newRifle;
			rifleshot = newRifleShot;
			riflePosition = NewriflePosition;
		}

		public rifleSprite()
		{ 
			
		}

		public void Reload(GameTime gameTime)
		{
			openfire = 0;
			l_reload = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!raised_rifle)
			{
				drawingRectangle = new Rectangle((int)riflePosition.X, (int)riflePosition.Y, 75, 35);
				spriteBatch.Begin();
				spriteBatch.Draw(rifle, drawingRectangle, Color.White);
				spriteBatch.End();
			}
			else
			{
				riflePosition.X = riflePosition.Y = -100;
				drawingRectangle = new Rectangle((int)riflePosition.X, (int)riflePosition.Y, 65, 50);
				spriteBatch.Begin();
				spriteBatch.Draw(rifle, drawingRectangle, Color.White);
				spriteBatch.End();
			}
		}

		public void Update(GameTime gameTime, List<bulletSprite> bullets, bool reload /*bool perk_aktiv*/)
		{
			if (raised_rifle)
			{
				if (bullets.Count == countammo)
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
