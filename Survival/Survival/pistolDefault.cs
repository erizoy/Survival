using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survival
{
	public class pistolDefault
	{

		int time_reload;
		bool l_reload = false;

		public pistolDefault()
		{ 
			
		}

		public void Reload(GameTime gameTime)
		{
			time_reload = (int)gameTime.ElapsedGameTime.Seconds + 2;
			l_reload = true;
		}

		public void Update(GameTime gameTime, List<bulletSprite> bullets, bool reload)
		{
			if (bullets.Count == 12)
			{
				Reload(gameTime);
				bullets.Clear();
			}
			if (l_reload)
			{
				if (gameTime.ElapsedGameTime.Seconds < time_reload)
					reload = true;
				else
				{
					reload = false;
					l_reload = false;
				}
			}
		}
	}
}
