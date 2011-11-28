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
    /// This is the main type for your game
    /// </summary>

    public class Survival : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        heroSprite hero;
        cursorSprite cursor;
		bulletLogic bullet;
        backSprite background;
        Texture2D monsterTexture;
        Texture2D deadTexture;

        Random randomPosition = new Random();
        int time;
        bool enableConsole = true;

        Rectangle heroRectangle;

        MouseState mouse = Mouse.GetState();
        KeyboardState oldKey = Keyboard.GetState();

        List<monsterSprite> monsters = new List<monsterSprite>(); // ������, ���������� ��������. 
        SpriteFont gameFont;
  
        public Survival()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

      
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            monsterTexture = Content.Load<Texture2D>("monster");
            deadTexture = Content.Load<Texture2D>("deadmonster");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("font");
            Vector2 heroPosition = new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2);
            hero = new heroSprite(Content.Load<Texture2D>("idlehero"), Content.Load<Texture2D>("hero"), heroPosition, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
			bullet = new bulletLogic(Content.Load<Texture2D>("bullet"), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            hero.velocity = new Vector2(2, 2);

            background = new backSprite(Content.Load<Texture2D>("background"));

            cursor = new cursorSprite(Content.Load<Texture2D>("cursor"));


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // ����������� ����� ���������� �������
            hero.idle.Dispose();
            spriteBatch.Dispose();
            // TODO: Unload any non ContentManager content here
        }


        public void AddMonster()
        {
            monsters.Add(new monsterSprite(monsterTexture, deadTexture, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde) && oldKey.IsKeyUp(Keys.OemTilde))
            {
                enableConsole = !enableConsole;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            cursor.Update(gameTime);
			bullet.Update(gameTime, hero.heroPosition);
            hero.Update(gameTime);
            if (time > 100)
            {
                time = 0;
            }
            if (time == randomPosition.Next(0, 100))
            {
                AddMonster();
            }
            else
                time++;

            heroRectangle = new Rectangle((int)hero.heroPosition.X, (int)hero.heroPosition.Y, hero.idle.Width / 2, hero.idle.Height / 2);

            foreach (monsterSprite one_monster in monsters)
            {
                if (!one_monster.isDead)
                foreach (bulletSprite one_bullet in bullet.bullets)
                {
                    Rectangle bulletRectangle = new Rectangle((int)bullet.bulletPosition.X, (int)bullet.bulletPosition.Y, 2, 2);
                    if (one_monster.monsterRectangle.Intersects(bulletRectangle))
                    {
                        if (!one_monster.isDead)
                            one_monster.rotationAngle = (float)Math.Atan2(heroRectangle.Y - one_monster.monsterPosition.Y, heroRectangle.X - one_monster.monsterPosition.X);
                        one_monster.isDead = true;
                        one_monster.currentFrame = 0;
                        one_monster.timeElapsed = 151;
                    }   
                }
                one_monster.Update(gameTime, heroRectangle);
            }


            base.Update(gameTime);

            oldKey = Keyboard.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            background.Draw(spriteBatch);
            
            cursor.Draw(spriteBatch);
			bullet.Draw(spriteBatch);
            
            if (enableConsole)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(gameFont, "   Position: " + hero.heroPosition.X.ToString() + ";" + hero.heroPosition.Y.ToString() + ";" + heroRectangle.Width.ToString() + ";" + heroRectangle.Height.ToString(), new Vector2(15, 15), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "      Mouse: " + mouse.X.ToString() + ";" + mouse.Y.ToString(), new Vector2(15, 30), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "       Time: " + bullet.time, new Vector2(15, 45), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "   Monsters: " + monsters.Count, new Vector2(15, 60), Color.YellowGreen);
                if (monsters.Count != 0)
                {
                    spriteBatch.DrawString(gameFont, "Monster Pos: " + monsters[0].monsterRectangle.X.ToString() + ";" + monsters[0].monsterRectangle.Y.ToString() + ";" + monsters[0].monsterRectangle.Width.ToString() + ";" + monsters[0].monsterRectangle.Height.ToString(), new Vector2(15, 75), Color.YellowGreen);
                }
                spriteBatch.DrawString(gameFont, "     Target: " + heroRectangle.X + ";" + heroRectangle.Y, new Vector2(15, 90), Color.YellowGreen);
                spriteBatch.End();
            }


            // ��������� ��������
            foreach (monsterSprite item in monsters)
            {
                item.Draw(spriteBatch);
            }

            hero.Draw(spriteBatch);

            base.Draw(gameTime);

        }
    }
}
