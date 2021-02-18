﻿using System;
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

        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private bool flipped;

        private Vector2 position = new Vector2(300, 400);

        private BoundingCircle bounds = new BoundingCircle(300, 430, 64 * .35f);

        /// <summary>wddddddddd
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds { get => bounds; }

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
            keyboardState = Keyboard.GetState();
            // Add animation logic here
            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                flipped = true;
                position += new Vector2(-1, 0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                flipped = false;
            }
            if (position.X - (64 * .35) > screenWidth)
            {
                position.X = 1;
            }
            else if (position.X < 0)
            {
                position.X = (float)(screenWidth - ((64 * .35) + 1));
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 1) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1) * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (position.Y > 480 - (64 * .35))
            {
                position.Y = (float)(480 - (64 * .35)) + 1;
            }
            else if (position.Y < 0 + (64 * .35))
            {
                position.Y = (float)(64 * .35) + 1;
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
            spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(64, 64), .35f, spriteEffects, 0);
        }
    }
}
