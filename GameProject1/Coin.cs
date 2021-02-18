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
        /// <summary>
        /// Stores the location of the enemy object on the sprite atlas
        /// </summary>
        private static Rectangle atlas_location = new Rectangle(19 * 16, 9 * 16, 16, 16);

        public Coin() : base(false)
        {
            bounds = new BoundingCircle((int) position.X, (int) position.Y, (int)(7 * scalar.X));
        }

        public override void Update(GameTime gameTime)
        {
            position += new Vector2(0, 1) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D atlas)
        {
            spriteBatch.Draw(atlas, position, atlas_location, Color.White, 0, new Vector2(8, 10), scalar, SpriteEffects.None, 0);
        }
    }
}
