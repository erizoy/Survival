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

		pistolDefault pistol;
		itemLogic item_b;
		perkSprite perk;
		flamethrowerSprite flame;
		rifleSprite rifle;
		subgunSprite subgun;
        heroSprite hero;
        cursorSprite cursor;
		bulletLogic bullet;
        backSprite background;
        Texture2D monsterTexture;
        Texture2D deadTexture;
		Menu menu;
		deathMenu deathmenu;
		SoundEffectInstance gamesound;
		interfaceSprite info;

        Random randomPosition = new Random();
        int time;
		public int score = 0;
		int i_levelup = 900;
        bool enableConsole;
		bool reload;
		bool auto = false;
		bool b_subgun = false;
		bool b_flame;
		public bool b_pistol = true;
		public bool p_first_raised = true;
		bool b_menuState = true;
		bool game_start = false;
		bool b_deathState = false;
		bool first_raised = true;
		bool b_restart;
		bool p_choose = false;
		bool u_indicator = true, d_indicator = true;
		bool perk_reg_activ = false;
		int reg_time;
		int level = 0;
		int monsteratack = 100;
		int hard = 4, count_monster = 0;

        Rectangle heroRectangle;
		Rectangle rifleRectangle;
		Rectangle flameRectangle;
		Rectangle firstaidbRectangle;
		Rectangle subgunRectangle;

        MouseState mouse = Mouse.GetState();
		MouseState oldmouse = Mouse.GetState();
        KeyboardState oldKey = Keyboard.GetState();

        List<monsterSprite> monsters = new List<monsterSprite>(); // список, содержащий монстров. 
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
			graphics.IsFullScreen = true;
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			graphics.ApplyChanges();
			base.Initialize();
		}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
			gamesound = Content.Load<SoundEffect>("Sound/gamesound").CreateInstance();
            monsterTexture = Content.Load<Texture2D>("Texture/monster");
            deadTexture = Content.Load<Texture2D>("Texture/deadmonster");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("font");
            Vector2 heroPosition = new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2);
			Vector2 riflePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 + 100);
			Vector2 subgunPosition = new Vector2(200, 500);
			Vector2 flamePosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 - 100);
			Vector2 itemPosition = new Vector2(graphics.PreferredBackBufferWidth / 2 + 100, graphics.PreferredBackBufferHeight / 2);
			Vector2 menuPosition = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
			item_b = new itemLogic(Content.Load<Texture2D>("Texture/first-aid"),  itemPosition);
			flame = new flamethrowerSprite(Content.Load<Texture2D>("Texture/flamethrower"), flamePosition);
			rifle = new rifleSprite(Content.Load<Texture2D>("Texture/rifle"), riflePosition, Content.Load<SoundEffect>("Sound/rifleshot").CreateInstance());
            hero = new heroSprite(Content.Load<Texture2D>("Texture/idlehero"), Content.Load<Texture2D>("Texture/hero"), Content.Load<Texture2D>("Texture/herodead"), heroPosition, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
			bullet = new bulletLogic(Content.Load<Texture2D>("Texture/bullet"), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            hero.velocity = new Vector2(2, 2);
			menu = new Menu(Content.Load<Texture2D>("Texture/Menu"), Content.Load<Texture2D>("Texture/start_game"), Content.Load<Texture2D>("Texture/options"), Content.Load<Texture2D>("Texture/stat"), Content.Load<Texture2D>("Texture/exit"),
				Content.Load<SoundEffect>("Sound/choose").CreateInstance(), Content.Load<SoundEffect>("Sound/guidance").CreateInstance(), Content.Load<SoundEffect>("Sound/mainsound").CreateInstance());
			deathmenu = new deathMenu(Content.Load<Texture2D>("Texture/deathmenu"), Content.Load<Texture2D>("Texture/restart"), Content.Load<Texture2D>("Texture/mainmenu"), Content.Load<SoundEffect>("Sound/deathsound").CreateInstance());
			info = new interfaceSprite(Content.Load<Texture2D>("Texture/healthammo"), Content.Load<Texture2D>("Texture/curhealth"), Content.Load<Texture2D>("Texture/curammo"), Content.Load<Texture2D>("Texture/levelup"), Content.Load<Texture2D>("Texture/xp"), 
				Content.Load<Texture2D>("Texture/hp100"));
			subgun = new subgunSprite(Content.Load<Texture2D>("Texture/pp"), subgunPosition);
			perk = new perkSprite(Content.Load<Texture2D>("Texture/mainmenuperk"), Content.Load<Texture2D>("Texture/fr"), Content.Load<Texture2D>("Texture/about_fr"), Content.Load<Texture2D>("Texture/athlete"), Content.Load<Texture2D>("Texture/about_athlete"),
				Content.Load<Texture2D>("Texture/about_def"), Content.Load<Texture2D>("Texture/big"), Content.Load<Texture2D>("Texture/about_big"), Content.Load<Texture2D>("Texture/regeneration"), Content.Load<Texture2D>("Texture/about_regeneration"),
				Content.Load<Texture2D>("Texture/notfr"), Content.Load<Texture2D>("Texture/notathlete"), Content.Load<Texture2D>("Texture/notbig"), Content.Load<Texture2D>("Texture/notregeneration"), Content.Load<Texture2D>("Texture/back"));
			pistol = new pistolDefault(Content.Load<SoundEffect>("Sound/pistolshot").CreateInstance());

            background = new backSprite(Content.Load<Texture2D>("Texture/background"));

            cursor = new cursorSprite(Content.Load<Texture2D>("Texture/cursor"), Content.Load<Texture2D>("Texture/maincursor"));


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Освобождаем ранее выделенные ресурсы
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
			if (b_menuState)
			{
				u_indicator = true;
				deathmenu.deathsound.Stop();
				menu.Update(gameTime);
				gamesound.Stop();
				cursor.Update(gameTime, u_indicator);
				MouseState m_cursor = Mouse.GetState();
				if ((m_cursor.LeftButton == ButtonState.Pressed) && (menu.mouseOnGame))
				{
					game_start = true;
					b_menuState = false;
					menu.mouseOnGame = false;
					menu.choose.Play();
				}
				if ((m_cursor.LeftButton == ButtonState.Pressed) && (menu.mouseOnExit))
				{
					this.Exit();
					menu.choose.Play();
				}
			}
			MouseState m_mouse = Mouse.GetState();
			MouseState m_mouse2 = Mouse.GetState();
			if (b_deathState)
			{
				u_indicator = true;
				menu.mainsound.Stop();
				gamesound.Stop();
				deathmenu.Update(gameTime);
				MouseState m_state = Mouse.GetState();
				if (m_state.LeftButton == ButtonState.Pressed && deathmenu.b_restart)
				{
					game_start = true;
					b_deathState = false;
					b_restart = true;
					hero.heroIsDead = false;
					menu.choose.Play();
				}

				if (m_state.LeftButton == ButtonState.Pressed && deathmenu.b_mainmenu)
				{
					b_menuState = true;
					b_deathState = false;
					b_restart = true;
					hero.heroIsDead = false;
					menu.choose.Play();
					game_start = false;
				}
			}
			else
			{
				if ((m_mouse.RightButton == ButtonState.Pressed) && (oldmouse.RightButton == ButtonState.Released) && (info.b_levelup))
					p_choose = true;
				if (p_choose)
				{
					u_indicator = true;
					perk.Update(gameTime);
					//cursor.Update(gameTime, u_indicator);
					if (m_mouse2.LeftButton == ButtonState.Pressed && perk.show_back)
						p_choose = false;
					if (m_mouse2.LeftButton == ButtonState.Pressed && perk.show_big && !perk.notshow_big) // перк здоровяк
					{
						hero.currentHealth = item_b.p_husky(hero.currentHealth);
						p_choose = false;
						perk.notshow_big = true;
						info.b_hp100 = false;
						level--;
					}
					if (m_mouse2.LeftButton == ButtonState.Pressed && perk.show_athlete && !perk.notshow_athlete) // перк атлет
					{
						hero.velocity = new Vector2(3, 3);
						p_choose = false;
						perk.notshow_athlete = true;
						level--;
					}
					if (m_mouse2.LeftButton == ButtonState.Pressed && perk.show_fr && !perk.notshow_fr) // перк быстрая перезарядка
					{
						pistol.time_reload -= 30;
						subgun.time_reload -= 30;
						rifle.time_reload -= 30;
						p_choose = false;
						perk.notshow_fr = true;
						level--;
					}
					if(m_mouse2.LeftButton == ButtonState.Pressed && perk.show_regeneration && !perk.notshow_regeneration) // восстановление здоровья
					{
						perk_reg_activ = true;
						p_choose = false;
						perk.notshow_regeneration = true;
						level--;
					}
				}
				else
				{
					if (game_start)
					{
						u_indicator = false;
						menu.mainsound.Stop();
						deathmenu.deathsound.Stop();
						gamesound.Play();
						perk.Update(gameTime);
						if (b_restart) // сброс параметров на defualt после перезапуска(!)
						{
							auto = false;
							b_flame = false;
							b_pistol = true;
							rifle.raised_rifle = false;
							hero.Health = 100;
							hero.currentHealth = 100;
							score = 0;
							b_restart = false;
							monsters.Clear();
							hero.heroPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
							bullet.bullets.Clear();
							perk_reg_activ = false;
							level = 0;
							hero.velocity = new Vector2(2, 2);
							pistol.time_reload += 30;
							subgun.time_reload += 30;
							rifle.time_reload += 30;
							info.b_hp100 = true;
						}
						if (Keyboard.GetState().IsKeyDown(Keys.OemTilde) && oldKey.IsKeyUp(Keys.OemTilde))
						{
							enableConsole = !enableConsole;
						}

						if (perk_reg_activ) // перк регенерация
						{
							if (hero.Health < (hero.currentHealth - 1))
							{
								if (reg_time != 0)
									reg_time--;
								else
								{
									hero.Health += 1;
									reg_time = 500;
								}
							}
						}
						//mouse = Mouse.GetState();

						if (subgun.raised_rifle)
						{
							if (first_raised)
							{
								bullet.bullets.Clear();
								first_raised = false;
							}
							subgun.Update(gameTime, bullet.bullets, reload);
							if (subgun.l_reload)
								reload = true;
							else
								reload = false;
						}

						if (rifle.raised_rifle)
						{
							if (first_raised)
							{
								bullet.bullets.Clear();
								first_raised = false;
							}
							rifle.Update(gameTime, bullet.bullets, reload);
							if (rifle.l_reload)
								reload = true;
							else
								reload = false;
						}
						if (b_pistol) // Валера, пришло твое время
						{
							if (p_first_raised)
							{
								bullet.bullets.Clear();
								p_first_raised = false;
							}
							pistol.Update(gameTime, bullet.bullets, reload);
							if (pistol.l_reload)
								reload = true;
							else
								reload = false;
						}

						info.Update(gameTime, hero.Health);
						//cursor.Update(gameTime, u_indicator);
						if (!hero.heroIsDead)
						{
							hero.Update(gameTime);
							bullet.Update(gameTime, hero.heroPosition, reload, auto, b_flame, b_pistol, b_subgun, pistol.pistolshot, rifle.rifleshot);
						}
						else
						{
							game_start = true;
							b_deathState = true;
						}
						//
						//if (monsters.Count < 1)
						//{
						if (count_monster == hard)
						{
							monsteratack -= 50;
						}
						else
						{
							count_monster++;
						}

						if (time > monsteratack)
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
						heroRectangle = new Rectangle((int)hero.heroPosition.X, (int)hero.heroPosition.Y, hero.drawingRectangle.Width / 2, hero.drawingRectangle.Height / 2);
						rifleRectangle = new Rectangle((int)rifle.riflePosition.X, (int)rifle.riflePosition.Y, rifle.drawingRectangle.Width, rifle.drawingRectangle.Height);
						subgunRectangle = new Rectangle((int)subgun.subgunPosition.X, (int)subgun.subgunPosition.Y, subgun.drawingRectangle.Width, subgun.drawingRectangle.Height);
						flameRectangle = new Rectangle((int)flame.flamePosition.X, (int)flame.flamePosition.Y, flame.drawingRectangle.Width, flame.drawingRectangle.Height);
						firstaidbRectangle = new Rectangle((int)item_b.itemPosition.X, (int)item_b.itemPosition.Y, item_b.drawingRectangle.Width, item_b.drawingRectangle.Height);
						
						if (heroRectangle.Intersects(firstaidbRectangle))
						{
							hero.Health = item_b.first_aid(hero.Health, hero.currentHealth);
						}

						if (heroRectangle.Intersects(subgunRectangle))
						{
							subgun.raised_rifle = true;
							auto = false;
							b_subgun = true;
							b_flame = false;
							b_pistol = false;
							p_first_raised = true;
							first_raised = true;
						}

						if (heroRectangle.Intersects(rifleRectangle))
						{
							rifle.raised_rifle = true;
							auto = true;
							b_flame = false;
							b_pistol = false;
							b_subgun = false;
							p_first_raised = true;
							first_raised = true;
						}

						if (heroRectangle.Intersects(flameRectangle))
						{
							flame.raised_flame = true;
							b_flame = true;
							auto = false;
							b_pistol = false;
							first_raised = true;
							p_first_raised = true;
							b_subgun = false;
						}

						foreach (monsterSprite one_monster in monsters)
						{

							if (!one_monster.isDead)
								foreach (bulletSprite one_bullet in bullet.bullets)
								{
									Rectangle bulletRectangle = new Rectangle((int)one_bullet.bulletPosition.X, (int)one_bullet.bulletPosition.Y, 1, 1);
									if (one_monster.monsterRectangle.Intersects(bulletRectangle))
									{
										one_bullet.deleting = true;
										if (b_pistol)
											one_monster.health -= pistol.damage;
										if (b_subgun)
											one_monster.health -= subgun.damage;
										if (auto)
											one_monster.health -= rifle.damage;
										if (one_monster.health <= 0)
										{
											if (!one_monster.isDead)
												one_monster.rotationAngle = (float)Math.PI + one_bullet.angle;//угол падения трупа
											one_monster.isDead = true;
											one_monster.currentFrame = 0;
											one_monster.timeElapsed = 151;
											score += 30;
										}
									}
								}
							if (!one_monster.isDead)
							{
								if (one_monster.monsterRectangle.Intersects(heroRectangle))
									hero.Damage(one_monster.damage);
							}
							one_monster.Update(gameTime, heroRectangle);
						}

						if (score >= i_levelup)
						{
							level++;
							i_levelup *= 3;
						}

						if (level != 0)
							info.b_levelup = true;
						else
							info.b_levelup = false;
					}

					oldmouse = m_mouse;
				}


			}

			cursor.Update(gameTime, u_indicator);

            base.Update(gameTime);

            oldKey = Keyboard.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
			if (b_menuState)
			{
				d_indicator = true;
				menu.Draw(spriteBatch);
				cursor.Draw(spriteBatch, d_indicator);
				spriteBatch.Begin();
				spriteBatch.DrawString(gameFont, "version 1.0", new Vector2(925, 730), Color.FromNonPremultiplied(0, 175, 220, 500));
				spriteBatch.End();
			}

			if (game_start)
			{
				d_indicator = false;
				//GraphicsDevice.Clear(Color.CornflowerBlue);
				background.Draw(spriteBatch);
				bullet.Draw(spriteBatch);
				flame.Draw(spriteBatch);
				subgun.Draw(spriteBatch);
				item_b.Draw(spriteBatch);

				// отрисовка монстров
				foreach (monsterSprite item in monsters)
				{
					item.Draw(spriteBatch);
				}

				hero.Draw(spriteBatch);
				info.Draw(spriteBatch);
				rifle.Draw(spriteBatch);
				spriteBatch.Begin();
				spriteBatch.DrawString(gameFont, "" + score, new Vector2(60, 212), Color.FromNonPremultiplied(0, 175, 220, 1000));
				spriteBatch.End();
				if (p_choose)
				{
					d_indicator = true;
					perk.Draw(spriteBatch);
				}

				if (b_deathState)
				{
					d_indicator = true;
					deathmenu.Draw(spriteBatch);
				}
				cursor.Draw(spriteBatch, d_indicator);

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
					spriteBatch.DrawString(gameFont, "     Health: " + (int)hero.Health, new Vector2(15, 105), Color.YellowGreen);
					spriteBatch.DrawString(gameFont, "  curHealth: " + (int)hero.currentHealth, new Vector2(15, 120), Color.YellowGreen);
					spriteBatch.DrawString(gameFont, "      Score: " + score, new Vector2(15, 135), Color.YellowGreen);
					spriteBatch.DrawString(gameFont, "Bullet Count:" + bullet.bullets.Count, new Vector2(15, 150), Color.YellowGreen);
					spriteBatch.DrawString(gameFont, "Time Reload: " + subgun.time_reload, new Vector2(15, 165), Color.YellowGreen);
					if (bullet.bullets.Count != 0)
						spriteBatch.DrawString(gameFont, "     Bullet: " + (int)bullet.bullets[0].bulletPosition.X + ";" + (int)bullet.bullets[0].bulletPosition.Y, new Vector2(15, 180), Color.YellowGreen);
					spriteBatch.End();
				}
			}

			

            base.Draw(gameTime);

        }
    }
}
