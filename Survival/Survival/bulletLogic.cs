﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Survival
{
	public class bulletLogic
	{
		public List<bulletSprite> bullets = new List<bulletSprite>();

		private Vector2 screenSize;  //размер экрана

		MouseState oldmouse;

		heroSprite hero = new heroSprite();

		public Texture2D bulletTexture; //текстура пуль
		public Vector2 bulletPosition; //позиция пули
		float angle; //угол при повороте мыши
		public int time, time2 = 0, time3 = 0;
		int attackSpeed = 0; //время между выстрелами

		bool shoot = false;

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
			//time = 0;
			bulletPosition.X = heroPosition.X + 10 * (float)Math.Cos(angle) - 6 * (float)Math.Sin(angle);
			bulletPosition.Y = heroPosition.Y + 10 * (float)Math.Sin(angle) + 6 * (float)Math.Cos(angle);
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

		public void Shoot(Vector2 heroPosition, MouseState mouse)
		{
			time = 0;
			time2 = 0;
			time3 = 0;
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
		}

		public void Update(GameTime gameTime, Vector2 heroPosition, bool reload, bool auto, bool b_flame, bool b_pistol, bool subgun, SoundEffectInstance pistolshot, SoundEffectInstance rifleshot)
		{
			//Логика пули
			if (!reload)
			{
				if (auto)
				{
					attackSpeed = 10;
					if (time2 != attackSpeed)
						time2++;
					else
					{
						MouseState mouse = Mouse.GetState();
						if (mouse.LeftButton == ButtonState.Pressed)
						{
							Shoot(heroPosition, mouse);
							rifleshot.Play();
							AddBullet(angle, heroPosition);
						}
					}
				}
				if (b_flame)
				{
					MouseState mouse = Mouse.GetState();
					if (mouse.LeftButton == ButtonState.Pressed)
					{
						Shoot(heroPosition, mouse);

					}
					if (shoot)
					{
						AddBullet(angle, heroPosition);
						shoot = false;
					}
				}
				if (b_pistol)
				{
					attackSpeed = 25;
					if (time != attackSpeed)
					    time++;
					else
					{
						MouseState m_mouse = Mouse.GetState();
						if (((m_mouse.LeftButton == ButtonState.Pressed) && (oldmouse.LeftButton == ButtonState.Released)))
						{
							Shoot(heroPosition, m_mouse);
							pistolshot.Play();
							AddBullet(angle, heroPosition);
						}
						oldmouse = m_mouse;
					}
				}

				if (subgun)
				{
					attackSpeed = 5;
					if (time3 != attackSpeed)
						time3++;
					else 
					{
						MouseState m_mouse = Mouse.GetState();
						if (m_mouse.LeftButton == ButtonState.Pressed)
						{
							Shoot(heroPosition, m_mouse);
							AddBullet(angle, heroPosition);
						}
					}
				}
			}

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
				if (!item.deleting)
					item.Draw(spriteBatch);
			}
		}
	}
}