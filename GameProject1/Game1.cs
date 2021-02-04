using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace GameProject1
{
    /// <summary>
    /// A game demonstrating the use of sprites
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SlimeGhostSprite slimeGhost;
        private Texture2D atlas;
        private List<Enemy> enemies;
        private SpriteFont bangers;
        private int wave;
        private int enemyTotal;
        private Texture2D ball;
        private Texture2D background_texture;

        /// <summary>
        /// Constructs the game
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            slimeGhost = new SlimeGhostSprite();
            Enemy.RegisterViewportWidth(GraphicsDevice.Viewport.Width);
            enemies = new List<Enemy>() {};
            wave = 1;
            for (int i = 0; i < 10; i++) enemies.Add(new Enemy(wave));

            base.Initialize();
        }

        /// <summary>
        /// Loads game content
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            slimeGhost.LoadContent(Content);
            atlas = Content.Load<Texture2D>("colored_packed");
            bangers = Content.Load<SpriteFont>("bangers");
            ball = Content.Load<Texture2D>("basketball");
            background_texture = Content.Load<Texture2D>("space");

        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            slimeGhost.Update(gameTime);
            List<Enemy> toRemove = new List<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
                if (enemy.Position.Y > GraphicsDevice.Viewport.Height)
                {
                    toRemove.Add(enemy);
                }
            }
            foreach (var e in toRemove)
            {
                enemyTotal += 1;
                enemies.Remove(e);
            }

            slimeGhost.Color = Color.White;
            foreach(var enemy in enemies)
            {
                if(enemy.Bounds.CollidesWith(slimeGhost.Bounds))
                {
                    slimeGhost.Color = Color.Red;
                }
            }

            if(enemies.Count == 0)
            {
                wave += 1;
                for (int i = 0; i < 10 + wave; i++) enemies.Add(new Enemy(wave));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background_texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            foreach (var enemy in enemies) {
                enemy.Draw(gameTime, spriteBatch, atlas);
                /* Visual Debugging */
                /*
                var rect = new Rectangle((int)(enemy.Bounds.X - enemy.Bounds.Radius), (int)(enemy.Bounds.Y - enemy.Bounds.Radius), (int)(2 * enemy.Bounds.Radius), (int)(2 * enemy.Bounds.Radius));
                spriteBatch.Draw(ball, rect, Color.Red);
                */
            }
            /* Visual Debugging */
            /*
            var rect2 = new Rectangle((int)(slimeGhost.Bounds.X - slimeGhost.Bounds.Radius), (int)(slimeGhost.Bounds.Y - slimeGhost.Bounds.Radius), (int)(2 * slimeGhost.Bounds.Radius), (int)(2 * slimeGhost.Bounds.Radius));
            spriteBatch.Draw(ball, rect2, Color.Brown);
            */

            slimeGhost.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(bangers, $"Wave : {wave}     {gameTime.TotalGameTime.TotalSeconds:f2}", new Vector2(20, 20), Color.Gold);
            spriteBatch.DrawString(bangers, $"Enemies Dodged : {enemyTotal}", new Vector2(400, 20), Color.Gold);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
