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

    /// <summary>
    /// This is the main type for your game
    /// </summary>

    public class Survival : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        heroSprite hero;
        cursorSprite cursor;
        backSprite background;

        //
        private SpriteFont gameFont;
        //
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //
            gameFont = Content.Load<SpriteFont>("font");
            //8
            Vector2 heroPosition = new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2);
            hero = new heroSprite(Content.Load<Texture2D>("idlehero"), Content.Load<Texture2D>("hero"), Content.Load<Texture2D>("bullet"), heroPosition, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            hero.Update(gameTime);
            cursor.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            background.Draw(spriteBatch);
            hero.Draw(spriteBatch);
            cursor.Draw(spriteBatch);

            MouseState mouse = Mouse.GetState();

            // ������ ����
            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, "Position: " + hero.heroPosition.X.ToString() + ";" + hero.heroPosition.Y.ToString(), new Vector2(15, 15), Color.YellowGreen);
            spriteBatch.DrawString(gameFont, "   Mouse: " + mouse.X.ToString() + ";" + mouse.Y.ToString(), new Vector2(15, 30), Color.YellowGreen);
            spriteBatch.DrawString(gameFont, "    Time: " + hero.time, new Vector2(15, 45), Color.YellowGreen);
            spriteBatch.End();


            base.Draw(gameTime);

        }
    }
}