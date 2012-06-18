using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	class i_bulletSprite
	{
		Texture2D onebullet;

		interfaceSprite info = new interfaceSprite();

		public i_bulletSprite(Texture2D newOneBullet)
		{
			onebullet = newOneBullet;
		}
		public i_bulletSprite()
		{

		}

		public void Draw(SpriteBatch spriteBatch, int positionX, int positionY)
		{
			if(info.b_pistol)
				spriteBatch.Draw(onebullet, new Rectangle(positionX, positionY, 11, 25), Color.White);
			if (info.b_subgun)
				spriteBatch.Draw(onebullet, new Rectangle(positionX, positionY, 10, 17), Color.White);
			if (info.b_rifle)
				spriteBatch.Draw(onebullet, new Rectangle(positionX, positionY, 10, 23), Color.White);
		}
	}
}
