using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject1.Collisions
{
    /// <summary>
    /// Holds the logic for a bounding circle
    /// </summary>
    public struct BoundingCircle
    {
        public float X;
        public float Y;
        public float Radius;

        /// <summary>
        /// Constructs a BoundingCircle with the provided coordinates
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="r">The radius of the circle</param>
        public BoundingCircle(float x, float y, float r)
        {
            X = x;
            Y = y;
            Radius = r;
        }

        /// <summary>
        /// Determines if this BoundingCircle collides with a BoundingPoint
        /// </summary>
        /// <param name="p">the bounding point</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingPoint p)
        {
            return CollisionHelper.Collides(this, p);
        }

        /// <summary>
        /// Determines if this BoundingCircle collides with a BoundingCircle
        /// </summary>
        /// <param name="c">the BoundingCircle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(this, c);
        }

        /// <summary>
        /// Determines if this BoundingCircle collides with a BoundingRectangle
        /// </summary>
        /// <param name="r">the BoundingRectangle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(r, this);
        }
    }
}
