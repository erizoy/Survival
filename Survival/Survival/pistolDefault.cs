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
		int openfire = 0;
		public bool l_reload = false;

		public pistolDefault()
		{ 
			
		}

		public void Reload(GameTime gameTime)
		{
			time_reload = 100;
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
