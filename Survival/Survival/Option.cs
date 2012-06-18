using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival
{
	class Option
	{
		Texture2D options;
		Texture2D backOption;
		public bool mouseOnBack = false;

		public Option(Texture2D newOptions, Texture2D newBackoption)
		{
			options = newOptions;
			backOption = newBackoption;
		}

		public void Update(GameTime gameTime)
		{
			MouseState m_cursor = Mouse.GetState();
			Rectangle back_rec = new Rectangle(322, 564, 124, 33);
			Rectangle r_cursor = new Rectangle((int)m_cursor.X, (int)m_cursor.Y, 1, 1);
			if (r_cursor.Intersects(back_rec))
				mouseOnBack = true;
			else
				mouseOnBack = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(options, new Rectangle(0, 0, 1024, 768), Color.White);
			if (mouseOnBack)
				spriteBatch.Draw(backOption, new Rectangle(322, 564, 124, 33), Color.White);
			spriteBatch.End();
		}
	}
}
