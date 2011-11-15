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
    /// <summary>
    /// класс, описывающий спрайт и передвижение со стрельбой персонажа.
    /// </summary>
    class heroSprite 
    {
        public Texture2D idle;  //текстура спрайта
        public Texture2D run; //текстура спрайта бега
        private Vector2 screenSize;  //размер экрана
        public Vector2 velocity; //скорость перемещения спрайта

        public Texture2D bulletTexture; //текстура пуль
        public Vector2 bulletPosition; //позиция пули
        float angle; //угол при повороте мыши

        public float rotationAngle; //поворот спрайта персонажа

        public Vector2 heroPosition; //позиция персонажа

        public bool isRunning; //логическая переменная показывающая нахождение персонажа в движении

        public List<bulletSprite> bullets = new List<bulletSprite>();  //список с пулями
        int attackSpeed = 8; //время между выстрелами
        public int time;

        int frameWidth, frameHeight; //высота и ширина экрана
        /// <summary>
        /// "выстрел" сбрасывает счётчик времени до 0, определяет позицию вылета пули, и добавляет в список.
        /// </summary>
        /// <param name="angle"></param>
        public void AddBullet(float angle)
        {
            time = 0;
            bulletPosition.X = heroPosition.X + 32 * (float)Math.Cos(angle) - 6 * (float)Math.Sin(angle);
            bulletPosition.Y = heroPosition.Y + 32 * (float)Math.Sin(angle) + 6 * (float)Math.Cos(angle);
            bullets.Add(new bulletSprite(bulletTexture, screenSize, bulletPosition, angle));
        }

        /// <summary>
        /// удаляет "ненужную" пулю
        /// </summary>
        public void DeleteBullet()
        {
            int i = 0;
            while (i < bullets.Count)
                if (bullets[i].deleting)
                    bullets.RemoveAt(i);
                else
                    i++;
        }

        /// <summary>
        ///  считает, сколько кадров должно быть
        /// </summary>
        public int Frames
        {
            get
            {
                return run.Width / frameWidth;
            }
        }


        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="newTexture"></param> текстура состояния покоя
        /// <param name="newRunTexture"></param> текстура бега
        /// <param name="newBulletTexture"></param> текстура пули
        /// <param name="newHeroPosition"></param> стартовая позиция персонажа
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        public heroSprite(Texture2D newTexture, Texture2D newRunTexture, Texture2D newBulletTexture, Vector2 newHeroPosition , int screenWidth, int screenHeight)
        {
            idle = newTexture;
            run = newRunTexture;
            bulletTexture = newBulletTexture;

            heroPosition = newHeroPosition;

            screenSize = new Vector2(screenWidth, screenHeight);
            frameWidth = frameHeight = run.Height;
        }

        int currentFrame;
        int timeElapsed;
        int timeForFrame = 110;

        /// <summary>
        /// отрисовка героя
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 vect = new Vector2(48, 48); //начальный угол
            Rectangle rect = new Rectangle((int)heroPosition.X, (int)heroPosition.Y, 100, 100); //позиция спрайта и его размеры
            spriteBatch.Begin();
            if (isRunning)
            {
                Rectangle r = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                spriteBatch.Draw(run, rect, r, Color.White, rotationAngle, vect, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(idle, rect, null, Color.White, rotationAngle, vect, SpriteEffects.None, 0f);
            }
            spriteBatch.End();

            // отрисовка пуль
            foreach (bulletSprite item in bullets)
            {
                item.Draw(spriteBatch);
            }
            
        }


        /// <summary>
        /// обновление кадров героя
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            isRunning = false;
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.D) & heroPosition.X < screenSize.X)
            {
                heroPosition.X += (int)velocity.X;
                isRunning = true;
            }
            if (keyboard.IsKeyDown(Keys.A) & heroPosition.X > 0)
            {
                heroPosition.X -= (int)velocity.X;
                isRunning = true;
            }
            if (keyboard.IsKeyDown(Keys.W) & heroPosition.Y > 0)
            {
                heroPosition.Y -= (int)velocity.Y;
                isRunning = true;
            }
            if (keyboard.IsKeyDown(Keys.S) & heroPosition.Y < screenSize.Y)
            {
                heroPosition.Y += (int)velocity.Y;
                isRunning = true;
            }

            if (isRunning)
            {
                timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (timeElapsed > timeForFrame)
                {
                    currentFrame = (currentFrame + 1) % Frames;
                    timeElapsed = 0;
                }
            }


            rotationAngle = (float)Math.Atan2(Mouse.GetState().Y - heroPosition.Y, Mouse.GetState().X - heroPosition.X);

            //Логика пули
            if (time != attackSpeed)
                time++;
            else
            {
                MouseState mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    time = 0;
                    if (mouse.X < heroPosition.X & mouse.Y < heroPosition.Y)
                    {
                        angle = (float)Math.Atan((heroPosition.Y - mouse.Y) / (heroPosition.X - mouse.X)) + (float)Math.PI;
                    }
                    if (mouse.X < heroPosition.X & mouse.Y > heroPosition.Y)
                    {
                        angle = -(float)Math.Atan((mouse.Y - heroPosition.Y) / (heroPosition.X - mouse.X)) + (float)Math.PI;
                    }
                    if (mouse.X > heroPosition.X & mouse.Y < heroPosition.Y)
                    {
                        angle = -(float)Math.Atan((heroPosition.Y - mouse.Y) / (mouse.X - heroPosition.X));
                    }
                    if (mouse.X > heroPosition.X & mouse.Y > heroPosition.Y)
                    {
                        angle = (float)Math.Atan((mouse.Y - heroPosition.Y) / (mouse.X - heroPosition.X));
                    }
                    AddBullet(angle);
                }
            }
            DeleteBullet();
            foreach (bulletSprite item in bullets)
            {
                item.Update(gameTime);
            }
        }
    }
}