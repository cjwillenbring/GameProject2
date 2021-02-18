using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject1.Collisions
{
    /// <summary>
    /// Holds the logic for the bounding point
    /// </summary>
    public struct BoundingPoint : ICollision
    {
        public float X { get; set; }
        public float Y { get; set; }

        /// <summary>
        /// Constructs a BoundingPoint with the provided coordinates
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public BoundingPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determines if this BoundingPoint collides with another BoundingPoint
        /// </summary>
        /// <param name="o">the other bounding point</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingPoint o)
        {
            return CollisionHelper.Collides(o, this);
        }

        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingCircle
        /// </summary>
        /// <param name="c">the BoundingCircle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(c, this);
        }

        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingCircle
        /// </summary>
        /// <param name="r">the BoundingRectangle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(r, this);
        }
    }
}
