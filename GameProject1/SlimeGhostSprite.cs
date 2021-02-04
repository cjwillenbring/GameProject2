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
    /// <summary>
    /// A class representing a slime ghost
    /// </summary>
    public class SlimeGhostSprite
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private bool flipped;

        private Vector2 position = new Vector2(300, 430);

        private BoundingCircle bounds = new BoundingCircle(300, 430, 64*.35f);

        /// <summary>wddddddddd
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds { get => bounds; }

        /// <summary>
        /// Holds the color of the slime ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("slime");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime, int screenWidth)
        {
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            /* Don't have gamepad to test so commenting out for now
            gamePadState = GamePad.GetState(0);

            position += gamePadState.ThumbSticks.Left.X * new Vector2(1, 0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
            if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;
            */

            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                flipped = true;
                position += new Vector2(-1,0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flipped = false;
            }
            if(position.X - (64*.35) > screenWidth)
            {
                position.X = 1;
            } else if (position.X < 0)
            {
                position.X = (float)(screenWidth - ((64 * .35) + 1));
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S) && position.Y <= 430)
            {
                position += new Vector2(0, 1) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W) && position.Y >= 370)
            {
                position += new Vector2(0, -1) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(64,64), .35f, spriteEffects, 0);
        }
    }
}
