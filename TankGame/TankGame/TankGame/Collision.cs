using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TankGame
{
    public static class Collision
    {
        public static bool Intersects(Rectangle rectangle1, Vector2 origin1, float rotation1, Rectangle rectangle2, Vector2 origin2, float rotation2)
        {
            //Calculate the Axis we will use to determine if a collision has occurred
            //Since the objects are rectangles, we only have to generate 4 Axis (2 for
            //each rectangle) since we know the other 2 on a rectangle are parallel.
            List<Vector2> aRectangleAxis = new List<Vector2>();
            aRectangleAxis.Add(UpperRightCorner(rectangle1, origin1, rotation1) - UpperLeftCorner(rectangle1, origin1, rotation1));
            aRectangleAxis.Add(UpperRightCorner(rectangle1, origin1, rotation1) - LowerRightCorner(rectangle1, origin1, rotation1));
            aRectangleAxis.Add(UpperLeftCorner(rectangle2, origin2, rotation2) - LowerLeftCorner(rectangle2, origin2, rotation2));
            aRectangleAxis.Add(UpperLeftCorner(rectangle2, origin2, rotation2) - UpperRightCorner(rectangle2, origin2, rotation2));

            //Cycle through all of the Axis we need to check. If a collision does not occur
            //on ALL of the Axis, then a collision is NOT occurring. We can then exit out 
            //immediately and notify the calling function that no collision was detected. If
            //a collision DOES occur on ALL of the Axis, then there is a collision occurring
            //between the rotated rectangles. We know this to be true by the Seperating Axis Theorem
            foreach (Vector2 aAxis in aRectangleAxis)
            {
                if (!IsAxisCollision(rectangle1, origin1, rotation1, rectangle2, origin2, rotation2, aAxis))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Determines if a collision has occurred on an Axis of one of the
        /// planes parallel to the Rectangle
        /// </summary>
        /// <param name="theRectangle"></param>
        /// <param name="aAxis"></param>
        /// <returns></returns>
        private static bool IsAxisCollision(Rectangle rectangle1, Vector2 origin1, float rotation1, Rectangle rectangle2, Vector2 origin2, float rotation2, Vector2 aAxis)
        {
            //Project the corners of the Rectangle we are checking on to the Axis and
            //get a scalar value of that project we can then use for comparison
            List<int> aRectangleAScalars = new List<int>();
            aRectangleAScalars.Add(GenerateScalar(UpperLeftCorner(rectangle2, origin2, rotation2), aAxis));
            aRectangleAScalars.Add(GenerateScalar(UpperRightCorner(rectangle2, origin2, rotation2), aAxis));
            aRectangleAScalars.Add(GenerateScalar(LowerLeftCorner(rectangle2, origin2, rotation2), aAxis));
            aRectangleAScalars.Add(GenerateScalar(LowerRightCorner(rectangle2, origin2, rotation2), aAxis));

            //Project the corners of the current Rectangle on to the Axis and
            //get a scalar value of that project we can then use for comparison
            List<int> aRectangleBScalars = new List<int>();
            aRectangleBScalars.Add(GenerateScalar(UpperLeftCorner(rectangle1, origin1, rotation1), aAxis));
            aRectangleBScalars.Add(GenerateScalar(UpperRightCorner(rectangle1, origin1, rotation1), aAxis));
            aRectangleBScalars.Add(GenerateScalar(LowerLeftCorner(rectangle1, origin1, rotation1), aAxis));
            aRectangleBScalars.Add(GenerateScalar(LowerRightCorner(rectangle1, origin1, rotation1), aAxis));

            //Get the Maximum and Minium Scalar values for each of the Rectangles
            int aRectangleAMinimum = aRectangleAScalars.Min();
            int aRectangleAMaximum = aRectangleAScalars.Max();
            int aRectangleBMinimum = aRectangleBScalars.Min();
            int aRectangleBMaximum = aRectangleBScalars.Max();

            //If we have overlaps between the Rectangles (i.e. Min of B is less than Max of A)
            //then we are detecting a collision between the rectangles on this Axis
            if (aRectangleBMinimum <= aRectangleAMaximum && aRectangleBMaximum >= aRectangleAMaximum)
            {
                return true;
            }
            else if (aRectangleAMinimum <= aRectangleBMaximum && aRectangleAMaximum >= aRectangleBMaximum)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generates a scalar value that can be used to compare where corners of 
        /// a rectangle have been projected onto a particular axis. 
        /// </summary>
        /// <param name="theRectangleCorner"></param>
        /// <param name="theAxis"></param>
        /// <returns></returns>
        private static int GenerateScalar(Vector2 theRectangleCorner, Vector2 theAxis)
        {
            //Using the formula for Vector projection. Take the corner being passed in
            //and project it onto the given Axis
            float aNumerator = (theRectangleCorner.X * theAxis.X) + (theRectangleCorner.Y * theAxis.Y);
            float aDenominator = (theAxis.X * theAxis.X) + (theAxis.Y * theAxis.Y);
            float aDivisionResult = aNumerator / aDenominator;
            Vector2 aCornerProjected = new Vector2(aDivisionResult * theAxis.X, aDivisionResult * theAxis.Y);

            //Now that we have our projected Vector, calculate a scalar of that projection
            //that can be used to more easily do comparisons
            float aScalar = (theAxis.X * aCornerProjected.X) + (theAxis.Y * aCornerProjected.Y);
            return (int)aScalar;
        }

        // <summary>
        /// Rotate a point from a given location and adjust using the Origin we
        /// are rotating around
        /// </summary>
        /// <param name="thePoint"></param>
        /// <param name="theOrigin"></param>
        /// <param name="theRotation"></param>
        /// <returns></returns>
        private static Vector2 RotatePoint(Vector2 thePoint, Vector2 theOrigin, float theRotation)
        {
            Vector2 aTranslatedPoint = new Vector2();
            aTranslatedPoint.X = (float)(theOrigin.X + (thePoint.X - theOrigin.X) * Math.Cos(theRotation)
                - (thePoint.Y - theOrigin.Y) * Math.Sin(theRotation));
            aTranslatedPoint.Y = (float)(theOrigin.Y + (thePoint.Y - theOrigin.Y) * Math.Cos(theRotation)
                + (thePoint.X - theOrigin.X) * Math.Sin(theRotation));
            return aTranslatedPoint;
        }

        private static Vector2 UpperLeftCorner(Rectangle rectangle, Vector2 origin, float rotation)
        {
            Vector2 aUpperLeft = new Vector2(rectangle.Left, rectangle.Top);
            aUpperLeft = RotatePoint(aUpperLeft, aUpperLeft + origin, rotation);
            return aUpperLeft;
        }

        private static Vector2 UpperRightCorner(Rectangle rectangle, Vector2 origin, float rotation)
        {
            Vector2 aUpperRight = new Vector2(rectangle.Right, rectangle.Top);
            aUpperRight = RotatePoint(aUpperRight, aUpperRight + new Vector2(-origin.X, origin.Y), rotation);
            return aUpperRight;
        }

        private static Vector2 LowerLeftCorner(Rectangle rectangle, Vector2 origin, float rotation)
        {
            Vector2 aLowerLeft = new Vector2(rectangle.Left, rectangle.Bottom);
            aLowerLeft = RotatePoint(aLowerLeft, aLowerLeft + new Vector2(origin.X, -origin.Y), rotation);
            return aLowerLeft;
        }

        private static Vector2 LowerRightCorner(Rectangle rectangle, Vector2 origin, float rotation)
        {
            Vector2 aLowerRight = new Vector2(rectangle.Right, rectangle.Bottom);
            aLowerRight = RotatePoint(aLowerRight, aLowerRight + new Vector2(-origin.X, -origin.Y), rotation);
            return aLowerRight;
        }

    }
}
