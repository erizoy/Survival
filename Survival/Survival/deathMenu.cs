using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Survival
{
	class deathMenu
	{
		public Texture2D deathMenuTexture;
		public Texture2D restartTexture;
		public Texture2D mainMenuTexture;
		public SoundEffectInstance deathsound;
		public bool b_restart, b_mainmenu;

		public deathMenu(Texture2D newDeathMenu, Texture2D newRestartTexture, Texture2D newMainMenuTexture, SoundEffectInstance newDeathSound)
		{
			deathMenuTexture = newDeathMenu;
			restartTexture = newRestartTexture;
			mainMenuTexture = newMainMenuTexture;
			deathsound = newDeathSound;
		}

		public void Update(GameTime gameTime)
		{
			deathsound.Play();
			MouseState State = Mouse.GetState();
			Rectangle mouse = new Rectangle((int)State.X, (int)State.Y, 7, 7);
			Rectangle r_restart = new Rectangle(517, 398, 231, 44);
			Rectangle r_mainmenu = new Rectangle(275, 398, 231, 44);
			if (mouse.Intersects(r_restart))
				b_restart = true;
			else
				b_restart = false;

			if (mouse.Intersects(r_mainmenu))
				b_mainmenu = true;
			else
				b_mainmenu = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(deathMenuTexture, new Rectangle(0, 0, 1024, 768), Color.White);
			if (b_restart)
				spriteBatch.Draw(restartTexture, new Rectangle(517, 398, 231, 44), Color.White);
			if (b_mainmenu)
				spriteBatch.Draw(mainMenuTexture, new Rectangle(275, 398, 231, 44), Color.White);
			spriteBatch.End();
		}
	}
}
