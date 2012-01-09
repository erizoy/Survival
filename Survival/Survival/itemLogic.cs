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
		public Vector2 itemPosition2;
		bool activ = false;
		public bool activ2 = false;
		double rez = 0.0;

		subgunSprite subgun = new subgunSprite();
		pistolDefault pistol = new pistolDefault();
		rifleSprite rifle = new rifleSprite();

		public itemLogic(Texture2D newItemTexture, Vector2 newitemPosition)
		{
			firstaidTexture = newItemTexture;
			itemPosition = newitemPosition;
		}

		public itemLogic()
		{

		}

		public int p_armorer(/*уточнить параметры*/) // оружейник
		{
			
			return 0;
		}

		public void p_alternativ_weapon()// альтернативное оружие
		{

		}

		public double p_husky(double health) // здоровяк
		{
			health += 30;
			return health;
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
		}
	}
}
