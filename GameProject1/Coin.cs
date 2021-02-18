using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject1.Collisions;

namespace GameProject1
{
    public class Coin : FallingItem
    {
        protected const int Y_AXIS_ACCELERATION = 5;

        /// <summary>
        /// Stores the location of the enemy object on the sprite atlas
        /// </summary>
        private static Rectangle atlas_location = new Rectangle(19 * 16, 9 * 16, 16, 16);

        public Coin() : base(2)
        {
            speed = new Vector2(0, rand.Next(30, 50));
            bounds = new BoundingCircle((int) position.X, (int) position.Y, (int)(7 * scalar.X));
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            speed += t * Y_AXIS_ACCELERATION * Vector2.UnitY;
            position += speed * t;
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D atlas)
        {
            spriteBatch.Draw(atlas, position, atlas_location, Color.White, 0, new Vector2(8, 10), scalar, SpriteEffects.None, 0);
        }
    }
}
