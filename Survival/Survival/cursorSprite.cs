using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Survival
{

    public class cursorSprite
    {
        public Texture2D gamecursorTexture;
		public Texture2D maincursorTexture;
        public Vector2 mousePosition = Vector2.Zero;

        public cursorSprite(Texture2D newCoursorTexture, Texture2D newMainCursorTexture)
        {
            gamecursorTexture = newCoursorTexture;
			maincursorTexture = newMainCursorTexture;
        }

        public void Draw(SpriteBatch spriteBatch,bool indecator)
        {
			if (!indecator)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(gamecursorTexture, mousePosition, Color.White);
				spriteBatch.End();
			}
			else
			{
				spriteBatch.Begin();
				spriteBatch.Draw(maincursorTexture, new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 21, 28), Color.White);
				spriteBatch.End();
			}

        }

        public void Update(GameTime gameTime, bool indecator)
        {
			if (!indecator)
			{
				MouseState m_lastState = Mouse.GetState();
				mousePosition = new Vector2(m_lastState.X - gamecursorTexture.Width / 2, m_lastState.Y - gamecursorTexture.Height / 2);
			}
			else
			{
				MouseState m_lastState = Mouse.GetState();
				mousePosition = new Vector2(m_lastState.X - maincursorTexture.Width / 2, m_lastState.Y - maincursorTexture.Height / 2);
			}
        }

    }

}
