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
        public Texture2D monsterTexture; //текстура спрайта бега
        public Texture2D deadMonsterTexture;
        private Vector2 screenSize;  //размер экрана
        public Vector2 velocity = new Vector2(2, 2); //скорость перемещения спрайта

        public float rotationAngle; //поворот спрайта персонажа

        public Vector2 monsterPosition  = new Vector2(-11, -11); //позиция персонажа
        public Rectangle monsterRectangle;
        public bool isDead, isAlredyDeath = false;

        int frameWidth, frameHeight, frameWidthDead, frameHeightDead; //высота и ширина экрана

        Random randomPosition = new Random();

        public Vector2 directionMonster;


        /// <summary>
        /// возвращает количество кадров в спрайте монстра
        /// </summary>
        public int Frames
        {
            get
            {
                return monsterTexture.Width / frameWidth;
            }
        }

        public int FramesDead
        {
            get
            {
                return deadMonsterTexture.Width / frameWidthDead;
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
        public monsterSprite(Texture2D newRunTexture, Texture2D newDeadTexture, int screenWidth, int screenHeight)
        {
            monsterTexture = newRunTexture;
            deadMonsterTexture = newDeadTexture;
            screenSize = new Vector2(screenWidth, screenHeight);
            frameWidth = frameHeight = monsterTexture.Height;
            frameWidthDead = frameHeightDead = deadMonsterTexture.Height;
            
            // Случайная выборка позиции монстра
            if (monsterPosition == new Vector2(-11, -11)) 
            {
                switch (randomPosition.Next(1, 5))
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
            }
        }

        public int currentFrame; // текущий кадр анимации
        public int timeElapsed; // сброс времени
        int timeForFrame = 25; // время задержки кадра

        /// <summary>
        ///  отрисовка монстра
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // начало отрисовки монстра
             
            if (!isDead)
            {
                Vector2 vect = new Vector2(48, 48); //начальный угол
                Rectangle rect = new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, 100, 100); //позиция спрайта и его размеры
                spriteBatch.Begin();
                {
                    Rectangle r = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                    spriteBatch.Draw(monsterTexture, rect, r, Color.White, rotationAngle, vect, SpriteEffects.None, 0f);
                }
                spriteBatch.End();
            }
            else
            {
                Vector2 vect = new Vector2(48, 48); //начальный угол
                Rectangle rect = new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, 125, 125);
                spriteBatch.Begin();
                {
                    Rectangle r = new Rectangle(currentFrame * frameWidthDead, 0, frameWidthDead, frameHeightDead);
                    spriteBatch.Draw(deadMonsterTexture, rect, r, Color.White, rotationAngle, vect, SpriteEffects.None, 0f);
                }
                spriteBatch.End();
            }
        }

        public bool CheckCollision(Rectangle firstRectngle, Rectangle secondRectangle)
        {
            monsterRectangle = new Rectangle((int)firstRectngle.X, (int)firstRectngle.Y, firstRectngle.Width, firstRectngle.Height);
            if (monsterRectangle.Intersects(secondRectangle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(GameTime gameTime, Rectangle heroRectangle)
        {
            if (!isDead)
            {
                isAlredyDeath = false;
                monsterRectangle = new Rectangle((int)monsterPosition.X, (int)monsterPosition.Y, monsterTexture.Width / Frames / 2, monsterTexture.Height / 2);
                if (!CheckCollision(monsterRectangle, heroRectangle))
                {
                    directionMonster = monsterPosition - new Vector2(heroRectangle.X, heroRectangle.Y);
                    directionMonster.Normalize();

                    monsterPosition += -directionMonster * velocity;
                }

                rotationAngle = (float)Math.Atan2(heroRectangle.Y - monsterPosition.Y, heroRectangle.X - monsterPosition.X);

                // смена кадров анимации монстра
                timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (timeElapsed > timeForFrame)
                {
                    currentFrame = (currentFrame + 1) % Frames;
                    timeElapsed = 0;
                }
            }
            else
            {
                while (currentFrame != FramesDead)
                {
                    timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                    int timeForDeath = 150;
                    if (timeElapsed > timeForDeath)
                    {
                        if (currentFrame == FramesDead - 1)
                        {
                            break;
                        }
                        currentFrame = (currentFrame + 1) % FramesDead;
                        timeElapsed = 0;
                    }
                    break;
                }
            }
        }
    }
}
