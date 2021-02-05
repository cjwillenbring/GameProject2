using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject1.Collisions;
using System;

namespace GameProject1
{
    public class Enemy
    {
        /// <summary>
        /// Holds the viewportWidth of the screen
        /// </summary>
        private static int viewportWidth;

        /// <summary>
        /// Stores the location of the enemy object on the sprite atlas
        /// </summary>
        private static Rectangle atlas_location = new Rectangle(19*16, 9*16, 16, 16);

        /// <summary>
        /// Sores the random generator for the enemy class
        /// </summary>
        private static Random rand = new Random();

        public Vector2 Position { get => position; }
        ///<summary>
        /// The enemy's position in the world
        ///</summary>
        private Vector2 position;

        /// <summary>
        /// The enemy's scale for when it gets rendered
        /// </summary>
        private Vector2 scalar;

        public Vector2 Scalar { get  => scalar; }

        /// <summary>
        /// The speed of the enemy falling
        /// </summary>
        private int speed;

        private BoundingCircle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds { get => bounds; }

        public bool HasCollided { get; set; }

        public Enemy(int waveSpeedMultiplier)
        {
            position = new Vector2((float)rand.NextDouble() * viewportWidth, 0);
            speed = rand.Next(40, 70*waveSpeedMultiplier);
            float rand_scale = (float)rand.NextDouble()*5;
            if(rand_scale > .5)
            {
                scalar = new Vector2(rand_scale, rand_scale);
            }
            else
            {
                scalar = new Vector2(1, 1);
            }
            bounds = new BoundingCircle((int)position.X, (int)position.Y, (int)(7 * scalar.X));
        }

        public static void RegisterViewportWidth(int w)
        {
            viewportWidth = w;
        }

        public void Update(GameTime gameTime)
        {
            position += new Vector2(0, 1) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D atlas)
        {
            spriteBatch.Draw(atlas, position, atlas_location, Color.White, 0, new Vector2(8,10), scalar, SpriteEffects.None, 0);
        }
    }
}
