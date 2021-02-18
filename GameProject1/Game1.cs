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
        // Graphics device manager and spritebatch properties
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Player Sprites
        private PlayerSprite player;
        private List<FallingItem> fallingItems;

        // Platform Sprite
        private List<PlatformSprite> platforms;

        // Textures
        private Texture2D humble_atlas;
        private Texture2D colored_pack_atlas;
        private Texture2D ball;
        private Texture2D background_texture;
        private Texture2D coin;

        // Fonts
        private SpriteFont bangers;

        // Game properties/mechanics
        private int best;
        private int currentScore;
        private double countdownTimer;
        private double gameOverTimer;

        // Misc.
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

        /// <summary>
        /// Reset the game
        /// </summary>
        private void Reset()
        {
            countdownTimer = 60;
            currentScore = 0;
            fallingItems = new List<FallingItem>() { };
            gameOverTimer = 0;
            player.GameOver = false;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // add in player sprite
            player = new PlayerSprite();

            // Add countdown timer and reset score
            currentScore = 0;
            countdownTimer = 60;
            gameOverTimer = 0;

            // register the viewport width with the falling items class
            FallingItem.RegisterViewportWidth(GraphicsDevice.Viewport.Width);

            // initialize the falling items list and add some items to it
            fallingItems = new List<FallingItem>() {};
            for (int i = 0; i < 10; i++) fallingItems.Add(new Bomb());

            // initialize platform list and populate with static method
            platforms = new List<PlatformSprite>();
            PlatformBuilder.GeneratePlatforms(platforms);

            // initialize the random object
            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// Loads game content
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new sprite batch for the graphics device
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Loads player content, textures, etc
            player.LoadContent(Content);

            // Load textures
            humble_atlas = Content.Load<Texture2D>("humble-item-pack");
            ball = Content.Load<Texture2D>("basketball");
            background_texture = Content.Load<Texture2D>("ground");
            coin = Content.Load<Texture2D>("coin-sparkle");
            colored_pack_atlas = Content.Load<Texture2D>("colored_packed");

            // Load fonts
            bangers = Content.Load<SpriteFont>("bangers");
        }

        /// <summary>d
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            double t = gameTime.ElapsedGameTime.TotalSeconds;
            countdownTimer -= t;
            if (gameOverTimer > 0) gameOverTimer -= t;
            else if (gameOverTimer < 0) Reset();
            if (countdownTimer < 0) Reset();

            // TODO: Add your update logic here
            player.Update(gameTime, GraphicsDevice.Viewport.Width, platforms);

            // Move through list of falling objects and get which ones have passed the bottom of the screen
            List<FallingItem> toRemove = new List<FallingItem>();
            foreach (var fallingItem in fallingItems)
            {
                fallingItem.Update(gameTime);
                if (fallingItem.Position.Y > GraphicsDevice.Viewport.Height)
                {
                    toRemove.Add(fallingItem);
                }
                best = Math.Max(best, currentScore);
            }

            // Set the player color to be white
            player.Color = Color.White;

            // Loop through falling items and look for collisions
            foreach(var item in fallingItems)
            {
                if(item.Bounds.CollidesWith(player.Bounds))
                {
                    player.Color = Color.Red;
                    item.HasCollided = true;
                    toRemove.Add(item);
                    // Add score logic here
                    if (item is Coin) currentScore++;
                    else if(item is Bomb)
                    {
                        currentScore -= 5;
                        if (currentScore < 0 && gameOverTimer == 0)
                        {
                            gameOverTimer = 1.2;
                            player.GameOver = true;
                        }
                    }
                }
            }

            // Remove the items that have clipped through the bottom of the game
            foreach (var item in toRemove)
                fallingItems.Remove(item);

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

            // Draw the background texture first since it should have the lowest z value and be rendered in the back
            spriteBatch.Draw(background_texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            foreach (var item in fallingItems) {
                // think about making atlas more dynamic, but for now handle with if else
                if(item is Bomb b)
                    b.Draw(gameTime, spriteBatch, humble_atlas);
                else if(item is Coin c)
                    c.Draw(gameTime, spriteBatch, coin);
                /* Visual Debugging */
                /*
                var rect = new Rectangle((int)(enemy.Bounds.X - enemy.Bounds.Radius), (int)(enemy.Bounds.Y - enemy.Bounds.Radius), (int)(2 * enemy.Bounds.Radius), (int)(2 * enemy.Bounds.Radius));
                spriteBatch.Draw(ball, rect, Color.Red);
                */
            }

            foreach(var platform in platforms)
            {
                platform.Draw(gameTime, spriteBatch, colored_pack_atlas);
            }

            player.Draw(gameTime, spriteBatch);
            
            // Render text, measure widths first to get more precise placement
            Vector2 widthScore = bangers.MeasureString($"Current Score : {currentScore}");
            Vector2 widthBest = bangers.MeasureString($"Best : {best}");

            spriteBatch.DrawString(bangers, $"Time Left : {countdownTimer:F}", new Vector2(5, 5), Color.Black);
            spriteBatch.DrawString(bangers, $"Current Score : {currentScore}", new Vector2(800 - (widthScore.X + 5), 5), Color.Black);
            spriteBatch.DrawString(bangers, $"Best : {best}", new Vector2(800 - (widthBest.X + 5), 45), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
