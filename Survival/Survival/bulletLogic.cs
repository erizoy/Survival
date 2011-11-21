using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival
{
	class bulletLogic
	{
		public List<bulletSprite> bullets = new List<bulletSprite>();

		private Vector2 screenSize;  //размер экрана

		heroSprite hero = new heroSprite();

		public Texture2D bulletTexture; //текстура пуль
		public Vector2 bulletPosition; //позиция пули
		float angle; //угол при повороте мыши
		public int time;
		int attackSpeed = 8; //время между выстрелами

		public bulletLogic(Texture2D newBulletTexture, int screenWidth, int screenHeight)
		{
			bulletTexture = newBulletTexture;
			screenSize = new Vector2(screenWidth, screenHeight);
		}

		public bulletLogic()
		{
			// TODO: Complete member initialization
		}

		/// <summary>
		/// "выстрел" сбрасывает счётчик времени до 0, определяет позицию вылета пули, и добавляет в список.
		/// </summary>
		/// <param name="angle"></param>
		public void AddBullet(float angle, Vector2 heroPosition)
		{
			//screenSize = new Vector2(screenWidth, screenHeight);			
			time = 0;
			bulletPosition.X = heroPosition.X + 32 * (float)Math.Cos(angle) - 6 * (float)Math.Sin(angle);
			bulletPosition.Y = heroPosition.Y + 32 * (float)Math.Sin(angle) + 6 * (float)Math.Cos(angle);
			bullets.Add(new bulletSprite(bulletTexture, screenSize, bulletPosition, angle));
		}

		/// <summary>
		/// удаляет "ненужную" пулю
		/// </summary>
     	public void DeleteBullet()
		{
			int i = 0;
			while (i < bullets.Count)
				if (bullets[i].deleting)
					bullets.RemoveAt(i);
				else
					i++;
		}

		public void Update(GameTime gameTime, Vector2 heroPosition)
		{
			//Логика пули
			if (time != attackSpeed)
				time++;
			else
			{
				MouseState mouse = Mouse.GetState();
				if (mouse.LeftButton == ButtonState.Pressed)
				{
					time = 0;
					if (mouse.X < heroPosition.X & mouse.Y < heroPosition.Y)
					{
						angle = (float)Math.Atan((heroPosition.Y - mouse.Y) / (heroPosition.X - mouse.X)) + (float)Math.PI;
					}
					if (mouse.X < heroPosition.X & mouse.Y > heroPosition.Y)
					{
						angle = -(float)Math.Atan((mouse.Y - heroPosition.Y) / (heroPosition.X - mouse.X)) + (float)Math.PI;
					}
					if (mouse.X > heroPosition.X & mouse.Y < heroPosition.Y)
					{
						angle = -(float)Math.Atan((heroPosition.Y - mouse.Y) / (mouse.X - heroPosition.X));
					}
					if (mouse.X > heroPosition.X & mouse.Y > heroPosition.Y)
					{
						angle = (float)Math.Atan((mouse.Y - heroPosition.Y) / (mouse.X - heroPosition.X));
					}
					AddBullet(angle, heroPosition);
				}
			}
			DeleteBullet();
			foreach (bulletSprite item in bullets)
			{
				item.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// отрисовка пуль
			foreach (bulletSprite item in bullets)
			{
				item.Draw(spriteBatch);
			}
		}
	}
}
