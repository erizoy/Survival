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

		pistolDefault pistol = new pistolDefault();
		itemLogic item_b;
		flamethrowerSprite flame;
		rifleSprite rifle;
        heroSprite hero;
        cursorSprite cursor;
		bulletLogic bullet;
        backSprite background;
        Texture2D monsterTexture;
        Texture2D deadTexture;

        Random randomPosition = new Random();
        int time;
        bool enableConsole = true;
		bool reload;
		bool auto;
		bool b_flame;

        Rectangle heroRectangle;
		Rectangle rifleRectangle;
		Rectangle flameRectangle;
		Rectangle firstaidbRectangle;
		Rectangle huskyRectangle;

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
			Vector2 riflePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 + 100);
			Vector2 flamePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 - 100);
			Vector2 itemPosition = new Vector2(graphics.PreferredBackBufferWidth / 2 + 100, graphics.PreferredBackBufferHeight / 2);
			Vector2 itemPosition2 = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 + 200);
			item_b = new itemLogic(Content.Load<Texture2D>("first-aid"), Content.Load<Texture2D>("husky"), itemPosition, itemPosition2);
			flame = new flamethrowerSprite(Content.Load<Texture2D>("flamethrower"), flamePosition);
			rifle = new rifleSprite(Content.Load<Texture2D>("rifle"), riflePosition);
            hero = new heroSprite(Content.Load<Texture2D>("idlehero"), Content.Load<Texture2D>("hero"), Content.Load<Texture2D>("herodead"), heroPosition, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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
            mouse = Mouse.GetState();

			if (rifle.raised_rifle)
			{
				rifle.Update(gameTime, bullet.bullets, reload);
				if (rifle.l_reload)
					reload = true;
				else
					reload = false;
			}
			else
			{
				pistol.Update(gameTime, bullet.bullets, reload);
			}

            cursor.Update(gameTime);
			if (!hero.heroIsDead)
			{
				hero.Update(gameTime);
				bullet.Update(gameTime, hero.heroPosition, reload, auto, b_flame);
			}
            //
            //if (monsters.Count < 1)
            //{
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
            //}
            heroRectangle = new Rectangle((int)hero.heroPosition.X, (int)hero.heroPosition.Y, hero.drawingRectangle.Width, hero.drawingRectangle.Height);
			rifleRectangle = new Rectangle((int)rifle.riflePosition.X, (int)rifle.riflePosition.Y, rifle.drawingRectangle.Width, rifle.drawingRectangle.Height);
			flameRectangle = new Rectangle((int)flame.flamePosition.X, (int)flame.flamePosition.Y, flame.drawingRectangle.Width, flame.drawingRectangle.Height);
			firstaidbRectangle = new Rectangle((int)item_b.itemPosition.X, (int)item_b.itemPosition.Y, item_b.drawingRectangle.Width, item_b.drawingRectangle.Height);
			huskyRectangle = new Rectangle((int)item_b.itemPosition2.X, (int)item_b.itemPosition2.Y, item_b.drawingRectangle.Width, item_b.drawingRectangle.Height);

			if (heroRectangle.Intersects(huskyRectangle))
			{
				hero.currentHealth = item_b.p_husky(hero.currentHealth);
			}

			if (heroRectangle.Intersects(firstaidbRectangle))
			{
				hero.Health = item_b.first_aid(hero.Health, hero.currentHealth);
			}

			if (heroRectangle.Intersects(flameRectangle))
			{
				flame.raised_flame = true;
				b_flame = true;
			}

			if (heroRectangle.Intersects(rifleRectangle))
			{
				rifle.raised_rifle = true;
				auto = true;
			}
            foreach (monsterSprite one_monster in monsters)
            {
                if (!one_monster.isDead)
                foreach (bulletSprite one_bullet in bullet.bullets)
                {
                    Rectangle bulletRectangle = new Rectangle((int)one_bullet.bulletPosition.X, (int)one_bullet.bulletPosition.Y, 1, 1);
                    if (bulletRectangle.Intersects(one_monster.monsterRectangle))
                    {
                        one_bullet.deleting = true;
                        if (!one_monster.isDead)
                            one_monster.rotationAngle = (float)Math.PI + one_bullet.angle;//���� ������� �����
                        one_monster.isDead = true;
                        one_monster.currentFrame = 0;
                        one_monster.timeElapsed = 151;
                    }   
                }
				if (!one_monster.isDead)
				{
					if (heroRectangle.Intersects(one_monster.monsterRectangle))
						hero.Damage(one_monster.damage);
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
			bullet.Draw(spriteBatch);
			flame.Draw(spriteBatch);
			item_b.Draw(spriteBatch);
           
            // ��������� ��������
            foreach (monsterSprite item in monsters)
            {
                item.Draw(spriteBatch);
            }

            hero.Draw(spriteBatch);
            cursor.Draw(spriteBatch);
			rifle.Draw(spriteBatch);

            if (enableConsole)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(gameFont, "   Position: " + hero.heroPosition.X.ToString() + ";" + hero.heroPosition.Y.ToString() + ";" + heroRectangle.Width.ToString() + ";" + heroRectangle.Height.ToString(), new Vector2(15, 15), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "      Mouse: " + mouse.X.ToString() + ";" + mouse.Y.ToString(), new Vector2(15, 30), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "       Time: " + bullet.time, new Vector2(15, 45), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "   Monsters: " + monsters.Count, new Vector2(15, 60), Color.YellowGreen);
                if (monsters.Count != 0)
                    spriteBatch.DrawString(gameFont, "Monster Pos: " + monsters[0].monsterRectangle.X.ToString() + ";" + monsters[0].monsterRectangle.Y.ToString() + ";" + monsters[0].monsterRectangle.Width.ToString() + ";" + monsters[0].monsterRectangle.Height.ToString(), new Vector2(15, 75), Color.YellowGreen);
                spriteBatch.DrawString(gameFont, "     Target: " + heroRectangle.X + ";" + heroRectangle.Y, new Vector2(15, 90), Color.YellowGreen);
				spriteBatch.DrawString(gameFont, "     Health: " + hero.Health, new Vector2(15, 105), Color.YellowGreen);
				spriteBatch.DrawString(gameFont, "  curHealth: " + (int)hero.currentHealth, new Vector2(15, 120), Color.YellowGreen);
                if (bullet.bullets.Count != 0)
                    spriteBatch.DrawString(gameFont, "     Bullet: " + (int)bullet.bullets[0].bulletPosition.X + ";" + (int)bullet.bullets[0].bulletPosition.Y, new Vector2(15, 135), Color.YellowGreen);
                spriteBatch.End();
            }

            base.Draw(gameTime);

        }
    }
}
