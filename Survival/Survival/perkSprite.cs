using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival
{
	class perkSprite
	{
		Texture2D mainperkmenu;
		Texture2D frperk;
		Texture2D about_frperk;
		Texture2D athlete;
		Texture2D about_athlete;
		Texture2D about_def;
		Texture2D big;
		Texture2D about_big;
		public bool show_fr = false, show_def = true, show_big = false, show_athlete = false;

		public perkSprite(Texture2D newMainPerkMenu, Texture2D newFrPerk, Texture2D newAdout_FrPerk, Texture2D newAthlete, Texture2D newAbout_Athlete, Texture2D newAbout_def, Texture2D newBig, Texture2D newAbout_Big)
		{
			mainperkmenu = newMainPerkMenu;
			frperk = newFrPerk;
			about_frperk = newAdout_FrPerk;
			athlete = newAthlete;
			about_athlete = newAbout_Athlete;
			about_def = newAbout_def;
			big = newBig;
			about_big = newAbout_Big;
		}

		public void Update(GameTime gameTime)
		{
			MouseState m_cursor = Mouse.GetState(); 
			Rectangle fr_rec = new Rectangle(296, 323, 200, 28);
			Rectangle big_rec = new Rectangle(296, 351, 200, 28);
			Rectangle athlete_rec = new Rectangle(296, 379, 200, 28);
			Rectangle r_cursor = new Rectangle((int)m_cursor.X, (int)m_cursor.Y, 1, 1);
			
			if (r_cursor.Intersects(fr_rec))
			{
				show_def = false;
				show_fr = true;				
			}
			else
				show_fr = false;

			if (r_cursor.Intersects(big_rec))
			{
				show_big = true;
				show_def = false;
			}
			else
				show_big = false;

			if (r_cursor.Intersects(athlete_rec))
			{
				show_athlete = true;
				show_def = false;
			}
			else
				show_athlete = false;

			if (!show_big && !show_fr && !show_athlete)
				show_def = true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(mainperkmenu, new Rectangle(261, 258, 502, 252), Color.White);
			if (show_fr)
			{
				spriteBatch.Draw(frperk, new Rectangle(296, 323, 200, 28), Color.White);
				spriteBatch.Draw(about_frperk, new Rectangle(296, 461, 460, 26), Color.White);
			}
			if (show_def)
				spriteBatch.Draw(about_def, new Rectangle(296, 461, 460, 26), Color.White);
			if (show_big)
			{
				spriteBatch.Draw(big, new Rectangle(296, 351, 200, 28), Color.White);
				spriteBatch.Draw(about_big, new Rectangle(296, 461, 460, 26), Color.White);
			}
			if (show_athlete)
			{
				spriteBatch.Draw(athlete, new Rectangle(296, 379, 200, 28), Color.White);
				spriteBatch.Draw(about_athlete, new Rectangle(296, 461, 460, 26), Color.White);
			}
			spriteBatch.End();
		}
	}
}
