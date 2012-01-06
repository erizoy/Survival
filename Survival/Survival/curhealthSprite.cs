using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survival
{
	class curhealthSprite
	{
		Texture2D curhealth;

		public curhealthSprite(Texture2D newCurHealth)
		{
			curhealth = newCurHealth;
		}

		public curhealthSprite()
		{

		}

		public void Draw(SpriteBatch spriteBatch, int position)
		{
			//spriteBatch.Begin();
			spriteBatch.Draw(curhealth, new Rectangle(position, 23, 2, 9), Color.Red);
			//spriteBatch.End();
		}
	}
}
