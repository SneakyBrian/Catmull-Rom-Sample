using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Catmull_Rom_Sample
{
    public static class CatmullRomSpline
    {
        /// <summary>
        /// Generate spline series of points from array of keyframe points
        /// </summary>
        /// <param name="points">array of key frame points</param>
        /// <param name="numPoints">number of points to generate in spline between each point</param>
        /// <returns>array of points describing spline</returns>
        public static PointF[] Generate(PointF[] points, int numPoints)
        {
            if (points.Length < 4)
                throw new ArgumentException("CatmullRomSpline requires at least 4 points", "points");
            
            var splinePoints = new List<PointF>();

            for (int i = 0; i < points.Length - 3; i++)
            {
                for (int j = 0; j < numPoints; j++)
                {
                    splinePoints.Add(PointOnCurve(points[i], points[i + 1], points[i + 2], points[i + 3], (1f / numPoints) * j));
                }
            }

            splinePoints.Add(points[points.Length - 2]);

            return splinePoints.ToArray();
        }

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <remarks>
        /// Points calculated exist on the spline between points two and three.
        /// </remarks>
        /// <param name="p0">First Point</param>
        /// <param name="p1">Second Point</param>
        /// <param name="p2">Third Point</param>
        /// <param name="p3">Fourth Point</param>
        /// <param name="t">
        /// Normalised distance between second and third point 
        /// where the spline point will be calculated
        /// </param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        public static PointF PointOnCurve(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            PointF ret = new PointF();

            float t2 = t * t;
            float t3 = t2 * t;

            ret.X = 0.5f * ((2.0f * p1.X) +
            (-p0.X + p2.X) * t +
            (2.0f * p0.X - 5.0f * p1.X + 4 * p2.X - p3.X) * t2 +
            (-p0.X + 3.0f * p1.X - 3.0f * p2.X + p3.X) * t3);

            ret.Y = 0.5f * ((2.0f * p1.Y) +
            (-p0.Y + p2.Y) * t +
            (2.0f * p0.Y - 5.0f * p1.Y + 4 * p2.Y - p3.Y) * t2 +
            (-p0.Y + 3.0f * p1.Y - 3.0f * p2.Y + p3.Y) * t3);

            return ret;
        }
    }
}
