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
            for (int i = 0; i < 10; i++) enemies.Add(new Enemy());

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
            foreach (var e in toRemove) enemies.Remove(e);

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
            foreach (var enemy in enemies) enemy.Draw(gameTime, spriteBatch, atlas);
            slimeGhost.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(bangers, $"{gameTime.TotalGameTime.TotalSeconds:c}", new Vector2(50, 50), Color.Gold);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
