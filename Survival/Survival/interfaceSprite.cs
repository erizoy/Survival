using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	class interfaceSprite
	{
		public Texture2D healthammo;
		public Texture2D curammo;
		public Texture2D levelup;
		public Texture2D sp_curhealth;
		//heroSprite hero = new heroSprite();

		public List<curhealthSprite> curhealth = new List<curhealthSprite>();

		public int position = 124;

		public bool b_levelup = false;

		public interfaceSprite(Texture2D newHealthAmmo, Texture2D newCurHealth, Texture2D newCurAmmo, Texture2D newLevelUp)
		{
			healthammo = newHealthAmmo;
			curammo = newCurAmmo;
			levelup = newLevelUp;
			sp_curhealth = newCurHealth;
		}

		public void Update(GameTime gameTime, double Health)
		{
			while (curhealth.Count < Health)
			{ 
				curhealth.Add(new curhealthSprite(sp_curhealth));
			}
		}

		public void Draw(SpriteBatch spriteBach)
		{
			spriteBach.Begin();
			spriteBach.Draw(healthammo, new Rectangle(0, 0, 711, 61), Color.White);
			foreach (curhealthSprite item in curhealth)
			{
				item.Draw(spriteBach, position);
				position += 2;
			}
			curhealth.Clear();
			position = 124;
			if(b_levelup)
				spriteBach.Draw(levelup, new Rectangle(649, 0, 375, 60), Color.White);
			spriteBach.End();
		}
	}
}
