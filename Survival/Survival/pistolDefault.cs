using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Survival
{
	public class pistolDefault
	{
		public SoundEffectInstance pistolshot;

		public int time_reload = 100;
		int openfire = 0;
		public bool l_reload = false;
		public int damage = 35;

		public pistolDefault(SoundEffectInstance newPistolShot)
		{
			pistolshot = newPistolShot;
		}

		public pistolDefault()
		{ 
			
		}

		public void Reload(GameTime gameTime)
		{
			openfire = 0;
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
