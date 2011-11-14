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
namespace SecondAttempt
{

    class cursorSprite
    {
        public Texture2D cursorTexture;
        public Vector2 mousePosition = Vector2.Zero;

        public cursorSprite(Texture2D newCoursorTexture)
        {
            cursorTexture = newCoursorTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cursorTexture, mousePosition, Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            MouseState m_lastState = Mouse.GetState();
            mousePosition = new Vector2(m_lastState.X, m_lastState.Y);
        }

    }

}
