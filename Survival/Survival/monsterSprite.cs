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

    class monsterSprite
    {
        public Texture2D idle;  //текстура спрайта
        public Texture2D run; //текстура спрайта бега
        private Vector2 screenSize;  //размер экрана
        public Vector2 velocity; //скорость перемещения спрайта

        public Texture2D bulletTexture; //текстура пуль
        public Vector2 bulletPosition; //позиция пули
        float angle; //угол

        public float rotationAngle; //поворот спрайта персонажа

        public Vector2 monsterPosition; //позиция персонажа

        public bool isRunning; //

        int frameWidth, frameHeight; //высота и ширина экрана

        public int Frames
        {
            get
            {
                return run.Width / frameWidth;
            }
        }

        public monsterSprite(Texture2D newTexture, Texture2D newRunTexture, Vector2 newMonsterPosition , int screenWidth, int screenHeight)
        {
            idle = newTexture;
            run = newRunTexture;

            monsterPosition = newMonsterPosition;

            screenSize = new Vector2(screenWidth, screenHeight);
            frameWidth = frameHeight = run.Height;
        }
    }
}
