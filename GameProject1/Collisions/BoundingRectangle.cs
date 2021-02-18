using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject1.Collisions
{
    /// <summary>
    /// Holds the logic for the bounding rectangle
    /// </summary>
    public struct BoundingRectangle : ICollision
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width;
        public float Height;

        /// <summary>
        /// Constructs a BoundingRectangle with the provided coordinates
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="w">The width of the rectangle</param>
        /// <param name="h">The height of the rectangle</param>
        public BoundingRectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Determines if this BoundingRectangle collides with a BoundingPoint
        /// </summary>
        /// <param name="p">the bounding point</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingPoint p)
        {
            return CollisionHelper.Collides(this, p);
        }

        /// <summary>
        /// Determines if this BoundingRectangle collides with a BoundingCircle
        /// </summary>
        /// <param name="c">the BoundingCircle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(this, c);
        }

        /// <summary>
        /// Determines if this BoundingRectangle collides with a BoundingRectangle
        /// </summary>
        /// <param name="r">the BoundingRectangle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(r, this);
        }

        /// <summary>
        /// Calculates the X overlap between the rectangle and another
        /// </summary>
        /// <param name="r">The other rectangle</param>
        /// <returns>A float representing the pixel overlap</returns>
        public float CalculateXOverlap(BoundingRectangle r)
        {
            return CollisionHelper.CalculateXOverlap(r, this);
        }

        /// <summary>
        /// Calculates the X overlap between the rectangle and another
        /// </summary>
        /// <param name="r">The other rectangle</param>
        /// <returns>A float representing the pixel overlap</returns>
        public float CalculateYOverlap(BoundingRectangle r)
        {
            return CollisionHelper.CalculateYOverlap(r, this);
        }
    }
}
