using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject1
{
    public class Enemy
    {
        /// <summary>
        /// Stores the location of the enemy object on the sprite atlas
        /// </summary>
        private static Rectangle atlas_location = new Rectangle(19*16, 10*16, 16, 16);

        /// <summary>
        /// Sores the random generator for the enemy class
        /// </summary>
        private static Random rand = new Random();

        ///<summary>
        /// The enemy's position in the world
        ///</summary>
        private Vector2 position;

        /// <summary>
        /// The enemy's scale for when it gets rendered
        /// </summary>
        private Vector2 scalar;

        /// <summary>
        /// The speed of the enemy falling
        /// </summary>
        private int speed;

        
        public Enemy()
        {
            position = new Vector2((float)rand.NextDouble() * 1000, 0);
            speed = rand.Next(30, 400);
            float rand_scale = (float)rand.NextDouble()*3;
            if(rand_scale > .2)
            {
                scalar = new Vector2(rand_scale, rand_scale);
            }
            else
            {
                scalar = new Vector2(1, 1);
            }
        }

        public void Update(GameTime gameTime)
        {
            position += new Vector2(0, 1) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D atlas)
        {
            spriteBatch.Draw(atlas, position, atlas_location, Color.White, 0, new Vector2(0,0), scalar, SpriteEffects.None, 0);
        }
    }
}
