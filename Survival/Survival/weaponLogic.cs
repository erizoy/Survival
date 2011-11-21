using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survival
{
    class weaponLogic
    {
		public int assult_rufle_round = 1;
		public int time_reload;
		private int i = 0;

		public weaponLogic()
		{

		}

		//public int assault_rufle(List<bulletSprite> bullets)
		//{
		//    bullets = new List<bulletSprite>(1);
		//    //damage = 40; // повреждение для монстров если по-умолчанию у них 100 hp
		//    return bullets.Count;
		//}

		public void Reload(GameTime gameTime)
		{
			Update(gameTime);
		}

		public void Update(GameTime gameTime)
		{ 
			time_reload = (int)gameTime.ElapsedGameTime.Ticks + 300;
			if (gameTime.ElapsedGameTime.Ticks < time_reload)
			{
				i++;
			}
		}

    }
}
