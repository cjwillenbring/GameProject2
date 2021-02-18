using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject1.Collisions;

namespace GameProject1
{
    public class PlayerSprite
    {
        /// <summary>
        /// Keyboard state
        /// </summary>
        private KeyboardState keyboardState;

        /// <summary>
        /// Player art/animations texture
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The location of the currently used frame
        /// </summary>
        private static Rectangle atlas_location = new Rectangle(0, 0, 16, 16);

        /// <summary>
        /// Whether or not the sprite is facing to the left or right
        /// </summary>
        private bool flipped;

        /// <summary>
        /// Holds the player's speed
        /// </summary>
        private Vector2 speed = new Vector2(150,150);

        /// <summary>
        /// Current position of the player
        /// </summary>
        private Vector2 position = new Vector2(300, 380);

        private BoundingRectangle bounds = new BoundingRectangle(300, 460, 38, 48);

        /// <summary>
        /// The bounding volume of the player
        /// </summary>
        public BoundingRectangle Bounds { get => bounds; }

        /// <summary>
        /// Holds the color of the player
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime, int screenWidth)
        {
            float playerWidth = 40;
            float playerHeight = 48;
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 unitY = Vector2.UnitY;
            Vector2 unitX = Vector2.UnitX;

            keyboardState = Keyboard.GetState();
            // Add animation logic here
            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                flipped = false;
                position += -unitX * speed * t;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += unitX * speed * t;
                flipped = true;
            }

            // Allow player to clip through the wall
            if (position.X > screenWidth) position.X = 1;
            else if (position.X < 0) position.X = (float)(screenWidth - (playerWidth + 1));

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += unitY * speed * t;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += -unitY * speed * t;
            }
            if (position.Y > 480 - playerHeight)
            {
                position.Y = (float)(480 - playerHeight) + 1;
            }
            else if (position.Y < 0 + playerHeight)
            {
                position.Y = (float)playerHeight + 1;
            }
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            // Origin is calculated using the original size
            spriteBatch.Draw(texture, position, atlas_location, Color, 0, new Vector2(2, 0), 3, spriteEffects, 0);
        }
    }
}
