using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject1.Collisions;

namespace GameProject1
{
    /// <summary>
    /// Abstract class to hold falling objects
    /// </summary>
    public abstract class FallingItem
    {
        /// <summary>
        /// Holds the viewportWidth of the screen
        /// </summary>
        protected static int viewportWidth;

        /// <summary>
        /// Stores the random generator for the falling item class
        /// </summary>
        protected static Random rand = new Random();

        ///<summary>
        /// The objects's position in the world
        ///</summary>
        protected Vector2 position;
        public Vector2 Position { get => position; }

        /// <summary>
        /// The objects's scale for when it gets rendered
        /// </summary>
        protected Vector2 scalar;
        public Vector2 Scalar { get => scalar; }

        /// <summary>
        /// The speed of the object falling
        /// </summary>
        protected int speed;

        /// <summary>
        /// The Bounding Shape
        /// </summary>
        protected ICollision bounds = null;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public ICollision Bounds { get => bounds; }

        public bool HasCollided { get; set; }

        public FallingItem(float scale)
        {
            float startingXPos = (float)rand.NextDouble();
            position = new Vector2(startingXPos* viewportWidth, 0);

            speed = rand.Next(40, 70);

            // deal with scaling
            scalar = new Vector2(scale, scale);

            // Make sure the bounds are set in the child class
            if (bounds == null) new Exception();
        }

        /// <summary>
        /// Register the viewport width
        /// </summary>
        /// <param name="w">The viewport width</param>
        public static void RegisterViewportWidth(int w)
        {
            viewportWidth = w;
        }


        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D atlas);
    }
}
