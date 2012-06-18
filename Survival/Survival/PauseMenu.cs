using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival
{
	class PauseMenu
	{
		Texture2D pauseMenu;
		Texture2D pauseBack;
		Texture2D pauseOptions;
		Texture2D pauseQuit;
		public bool mouseOnBack = false, mouseOnQuit = false, mouseOnOption = false;

		public PauseMenu(Texture2D newPauseMenu, Texture2D newPauseBack, Texture2D newPauseOptions, Texture2D newPauseQuit)
		{
			pauseMenu = newPauseMenu;
			pauseBack = newPauseBack;
			pauseOptions = newPauseOptions;
			pauseQuit = newPauseQuit;
		}

		public void Update(GameTime gameTime)
		{
			MouseState m_cursor = Mouse.GetState();
			Rectangle back_rec = new Rectangle(320, 285, 394, 56);
			Rectangle quit_rec = new Rectangle(320, 403, 394, 56);
			Rectangle option_rec = new Rectangle(320, 344, 394, 56);
			Rectangle r_cursor = new Rectangle((int)m_cursor.X, (int)m_cursor.Y, 1, 1);
			if (r_cursor.Intersects(back_rec))
				mouseOnBack = true;
			else
				mouseOnBack = false;

			if (r_cursor.Intersects(option_rec))
				mouseOnOption = true;
			else
				mouseOnOption = false;

			if (r_cursor.Intersects(quit_rec))
				mouseOnQuit = true;
			else
				mouseOnQuit = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(pauseMenu, new Rectangle(0, 0, 1024, 768), Color.White);
			if (mouseOnBack)
				spriteBatch.Draw(pauseBack, new Rectangle(320, 285, 394, 56), Color.White);
			if (mouseOnOption)
				spriteBatch.Draw(pauseOptions, new Rectangle(320, 344, 394, 56), Color.White);
			if (mouseOnQuit)
				spriteBatch.Draw(pauseQuit, new Rectangle(320, 403, 394, 56), Color.White);
			spriteBatch.End();
		}
	}
}
