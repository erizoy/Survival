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

    class bulletSprite
    {
        public Texture2D bulletTexture;
        
        public Vector2 bulletPosition, screenSize;
        public bool starting, deleting;
        public float angle, speed = 16f;

        public bulletSprite(Texture2D newBulletTexture, Vector2 newScreenSize, Vector2 newBulletPoition, float newAngle)
        {
            bulletTexture = newBulletTexture;
            starting = true;
            angle = newAngle;
            screenSize = newScreenSize;
            bulletPosition = newBulletPoition;
        }

        public void Update(GameTime gameTime)
        {
            if (starting)
            {
                bulletPosition += new Vector2(speed * (float)Math.Cos(angle), speed * (float)Math.Sin(angle));
            }
            if (bulletPosition.X > screenSize.X || bulletPosition.Y > screenSize.Y || bulletPosition.X < 0 || bulletPosition.Y < 0)
            {
                deleting = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bulletTexture, bulletPosition, Color.White);
            spriteBatch.End();
        }

       
    }

}
