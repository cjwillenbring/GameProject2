using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject1
{
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3
    }
    public class BatSprite
    {
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame;

        ///<summary>
        /// The bat's position in the world
        ///</summary>
        public Vector2 Position { get; set; }

        ///<summary>
        /// The bat's direction
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The ContentManager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("32x32-bat-sprite");
        }

        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // every two seconds, change direction
            if (directionTimer > 2.0)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Direction = Direction.Down;
                        break;
                    case Direction.Down:
                        Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        Direction = Direction.Right;
                        break;
                    case Direction.Right:
                        Direction = Direction.Up;
                        break;
                }
                directionTimer -= 2.0;
            }

            switch (Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Every 1/16th of a second, advance the animation frame 
            if (animationTimer > .3)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 0;
                animationTimer -= .3;
            }

            var sourceRect = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
            spriteBatch.Draw(texture, Position, sourceRect, Color.White);
        }
    }
}
