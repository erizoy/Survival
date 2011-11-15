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

        int frameWidth, frameHeight; //высота и ширина экрана

        Random randomPosition = new Random();

        public Vector2 directionMonster;


        /// <summary>
        /// возвращает количество кадров в спрайте монстра
        /// </summary>
        public int Frames
        {
            get
            {
                return run.Width / frameWidth;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="newTexture"></param>
        /// <param name="newRunTexture"></param>
        /// <param name="newMonsterPosition"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public monsterSprite(Texture2D newRunTexture, int screenWidth, int screenHeight)
        {
            run = newRunTexture;
            screenSize = new Vector2(screenWidth, screenHeight);
            frameWidth = frameHeight = run.Height;
        }

        int currentFrame; // текущий кадр анимации
        int timeElapsed; // сброс времени
        int timeForFrame = 110; // время задержки кадра

        /// <summary>
        ///  отрисовка монстра
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Случайная выборка позиции монстра
            switch (randomPosition.Next(1, 4)) 
            {
                case 1:
                    monsterPosition = new Vector2(randomPosition.Next(0, (int)screenSize.X), randomPosition.Next(-10, 0));
                    break;
                case 2:
                    monsterPosition = new Vector2(randomPosition.Next((int)screenSize.X, (int)screenSize.X + 10), randomPosition.Next(0, (int)screenSize.Y));
                    break;
                case 3:
                    monsterPosition = new Vector2(randomPosition.Next(0, (int)screenSize.X), randomPosition.Next((int)screenSize.Y, (int)screenSize.Y + 10));
                    break;
                case 4:
                    monsterPosition = new Vector2(randomPosition.Next(-10, 0), randomPosition.Next(0, (int)screenSize.Y));
                    break;
            }

            // начало отрисовки монстра

            

            Vector2 vect = new Vector2(48, 48); //начальный угол
            Rectangle rect = new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, 100, 100); //позиция спрайта и его размеры
            spriteBatch.Begin();
            {
                Rectangle r = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                spriteBatch.Draw(run, rect, r, Color.White, rotationAngle, vect, SpriteEffects.None, 0f);
            }
        }

        public void Update(GameTime gameTime, Vector2 heroPosition)
        {
            directionMonster = monsterPosition - new Vector2(heroPosition.X, heroPosition.Y);
            directionMonster.Normalize();

            monsterPosition += -directionMonster * velocity;

            // смена кадров анимации монстра
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (timeElapsed > timeForFrame)
            {
                currentFrame = (currentFrame + 1) % Frames;
                timeElapsed = 0;
            }
        }
    }
}
