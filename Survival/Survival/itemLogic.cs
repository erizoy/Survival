using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survival
{
    class itemLogic
    {
		int reg_time = 300;

		public int p_fast_reload(int time)// быстрая перезарядка
		{
			time += 30;
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

		public int p_husky(int health) // здоровяк
		{
			health += 30;
			return health;
		}

		public int p_athlete(int run) //  атлет
		{
			run += 30;
			return run;
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
    }
}
