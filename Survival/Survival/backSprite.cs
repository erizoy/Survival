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

    class backSprite
    {
        public Texture2D backgroundTexture;

        public backSprite(Texture2D newBackgroundTexture)
        {
            backgroundTexture = newBackgroundTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0,
                             1024,
                             768),
                             Color.LightGray);
            spriteBatch.End();
        }
        
    }

}
