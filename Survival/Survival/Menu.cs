using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Survival
{
	class Menu
	{
		public Texture2D menuTexture;
		public Texture2D newgame;
		public Texture2D option;
		public Texture2D stat;
		public Texture2D exit;
		public Vector2 menuPosition;
		public bool mouseOnGame, mouseOnStat, mouseOnOption, mouseOnExit;

		public Menu(Texture2D newMenuTexture, Texture2D n_newgame, Texture2D newOption, Texture2D newStat, Texture2D newExit)
        {
            menuTexture = newMenuTexture;
			newgame = n_newgame;
			option = newOption;
			stat = newStat;
			exit = newExit;
			//menuPosition = newMenuPosition;
        }

		public void Update(GameTime gameTime)
		{
			MouseState State = Mouse.GetState();
			Rectangle mouseRectangle = new Rectangle((int)State.X, (int)State.Y, 7, 7);
			Rectangle r_newgame = new Rectangle(308, 331, 419, 78);
			Rectangle r_stat = new Rectangle(308, 410, 419, 78);
			Rectangle r_option = new Rectangle(308, 488, 419, 78);
			Rectangle r_exit = new Rectangle(308, 566, 419, 78);

			if (mouseRectangle.Intersects(r_newgame))
				mouseOnGame = true;
			else
				mouseOnGame = false;

			if (mouseRectangle.Intersects(r_stat))
				mouseOnStat = true;
			else
				mouseOnStat = false;

			if (mouseRectangle.Intersects(r_option))
				mouseOnOption = true;
			else
				mouseOnOption = false;

			if (mouseRectangle.Intersects(r_exit))
				mouseOnExit = true;
			else
				mouseOnExit = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(menuTexture, new Rectangle(0, 0, 1024, 768), Color.White);
			spriteBatch.End();
			if (mouseOnGame)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(newgame, new Rectangle(308, 331, 419, 78), Color.White);
				spriteBatch.End();
			}
			if (mouseOnStat)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(stat, new Rectangle(308, 410, 419, 78), Color.White);
				spriteBatch.End();
			}
			if (mouseOnOption)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(option, new Rectangle(308, 488, 419, 78), Color.White);
				spriteBatch.End();
			}
			if (mouseOnExit)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(exit, new Rectangle(308, 566, 419, 78), Color.White);
				spriteBatch.End();
			}
		}
	}
}
