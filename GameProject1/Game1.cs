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

        private PlayerSprite player;
        private Texture2D humble_atlas;
        private List<Bomb> enemies;
        private SpriteFont bangers;
        private int wave;
        private int enemyTotal;
        private Texture2D ball;
        private Texture2D background_texture;
        private int lives;
        private int best;

        private double waveStart;
        private double waveTimer;
        private int waveHeats;

        private Random random;

        /// <summary>
        /// Constructs the game
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void Reset()
        {
            enemies = new List<Bomb>() { };
            wave = 1;
            waveTimer = 0;
            waveStart = 0;
            waveHeats = 0;
            lives = 3;
            enemyTotal = 0;
            for (int i = 0; i < 10; i++) enemies.Add(new Bomb());
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new PlayerSprite();
            Bomb.RegisterViewportWidth(GraphicsDevice.Viewport.Width);
            enemies = new List<Bomb>() {};
            wave = 1;
            waveTimer = 0;
            waveStart = 0;
            waveHeats = 0;
            lives = 3;
            for (int i = 0; i < 10; i++) enemies.Add(new Bomb());
            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// Loads game content
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.LoadContent(Content);
            humble_atlas = Content.Load<Texture2D>("humble-item-pack");
            bangers = Content.Load<SpriteFont>("bangers");
            ball = Content.Load<Texture2D>("basketball");
            background_texture = Content.Load<Texture2D>("ground");

        }

        /// <summary>d
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            waveTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(waveTimer - waveStart > random.Next(3,5)*(waveHeats+1) && waveHeats < wave)
            {
                for (int i = 0; i < random.Next(7, 7 + wave); i++) enemies.Add(new Bomb());
                waveHeats++;
            }

            // TODO: Add your update logic here
            player.Update(gameTime, GraphicsDevice.Viewport.Width);
            List<Bomb> toRemove = new List<Bomb>();
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
                best = Math.Max(best, enemyTotal);
                enemies.Remove(e);
            }

            player.Color = Color.White;
            foreach(var enemy in enemies)
            {
                if(enemy.Bounds.CollidesWith(player.Bounds))
                {
                    player.Color = Color.Red;
                    if(!enemy.HasCollided)
                    {
                        enemy.HasCollided = true;
                        lives -= 1;
                    }
                }
            }

            if (enemies.Count == 0 && waveHeats == wave)
            {
                wave += 1;
                waveStart = gameTime.TotalGameTime.TotalSeconds;
                waveTimer = waveStart;
                waveHeats = 0;
                for (int i = 0; i < random.Next(7, 7 + wave); i++) enemies.Add(new Bomb());
            }

            if(lives < 1)
            {
                Reset();
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
                enemy.Draw(gameTime, spriteBatch, humble_atlas);
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

            player.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(bangers, $"Wave : {wave}", new Vector2(20, 20), Color.Gold);
            spriteBatch.DrawString(bangers, $"Lives : {lives}", new Vector2(20, 60), Color.Gold);
            spriteBatch.DrawString(bangers, $"Enemies Dodged : {enemyTotal}", new Vector2(400, 20), Color.Gold);
            spriteBatch.DrawString(bangers, $"Best : {best}", new Vector2(587, 60), Color.Gold);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
