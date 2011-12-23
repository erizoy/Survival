using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
    public class itemLogic
    {

		int reg_time = 300;
		public Rectangle drawingRectangle;
		public Vector2 itemPosition;
		public Texture2D firstaidTexture;
		public Texture2D huskyTexture;
		public Vector2 itemPosition2;
		bool activ = false;
		bool activ2 = false;
		double rez = 0.0;

		public itemLogic(Texture2D newItemTexture, Texture2D newHuskyTexture, Vector2 newitemPosition, Vector2 newitemPosition_2)
		{
			firstaidTexture = newItemTexture;
			huskyTexture = newHuskyTexture;
			itemPosition = newitemPosition;
			itemPosition2 = newitemPosition_2;
		}

		public itemLogic()
		{ 
			
		}

		public int p_fast_reload(int time)// быстрая перезарядка
		{
			time -= 30;
			return time;
		}

		public int p_armorer(/*уточнить параметры*/) // оружейник
		{
			// нужен класс с оружием т. к. передаваемые параметры зависят именно от количества патронов в разных стволах
			return 0;
		}

		public void p_alternativ_weapon()// альтернативное оружие
		{

		}

		public double p_husky(double health) // здоровяк
		{
			health += 50;
			activ2 = true;
			return health;
		}

		public void p_athlete(int X, int Y) //  атлет
		{
			X += 30;
			Y += 30;
		}

		public void p_regeneration(int health) // в основной программе сделать глобальную проверку health при взятии этого перка(!)
		{
			if ((health != 100) || (health != 130))
			{
				while ((health == 100) || (health == 130))
				{
					if (reg_time != 0)
					{
						reg_time--; // скорее всего здоровье будет восстанавливаться слишком быстро(!)
					}
					else
					{
						health += 1;
						reg_time = 300; // но будем посмотреть
					}
				}
			}

		}

		public double first_aid(double health, double currentHealth)
		{
			if (health != currentHealth)
			{
				rez = currentHealth - health;
				if (rez < 30)
					health += rez;
				else
					health += 30;
				activ = true;
			}
			return health;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!activ)
			{
				drawingRectangle = new Rectangle((int)itemPosition.X, (int)itemPosition.Y, 20, 20);
				spriteBatch.Begin();
				spriteBatch.Draw(firstaidTexture, drawingRectangle, Color.White);
				spriteBatch.End();
			}
			else
			{
				itemPosition.X = -100;
				itemPosition.Y = -100;
				drawingRectangle = new Rectangle((int)itemPosition.X, (int)itemPosition.Y, 20, 20);
				spriteBatch.Begin();
				spriteBatch.Draw(firstaidTexture, drawingRectangle, Color.White);
				spriteBatch.End();
			}
			if (!activ2)
			{
				drawingRectangle = new Rectangle((int)itemPosition2.X, (int)itemPosition2.Y, 20, 20);
				spriteBatch.Begin();
				spriteBatch.Draw(huskyTexture, drawingRectangle, Color.White);
				spriteBatch.End();
			}
			else
			{
				itemPosition2.X = -150;
				itemPosition2.Y = -150;
				drawingRectangle = new Rectangle((int)itemPosition2.X, (int)itemPosition2.Y, 20, 20);
				spriteBatch.Begin();
				spriteBatch.Draw(huskyTexture, drawingRectangle, Color.White);
				spriteBatch.End();
			}
		}
	}
}
