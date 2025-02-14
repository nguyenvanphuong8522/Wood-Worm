using UnityEngine;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Linq;
using Sirenix.OdinInspector;

public class Point
{
    public float X { get; }
    public float Y { get; }

    public Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float CompareTo(Point other)
    {
        if (X != other.X)
        {
            return X - other.X;
        }
        return Y - other.Y;
    }

    public static float CrossProduct(Point A, Point B,
                                   Point C)
    {
        return (B.X - A.X) * (C.Y - A.Y)
            - (B.Y - A.Y) * (C.X - A.X);
    }
        // Override Equals và GetHashCode để so sánh đúng đắn các đối tượng Point
    public override bool Equals(object obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

}
public class DemoPolygon : MonoBehaviour
{
    public PolygonCollider2D polygonCollider;
    [Button]
    void Test()
    {
        Point[] points = new Point[transform.childCount*4];
        int index = 0;
        for(int i = 0; i < transform.childCount; i++)
        {
            Vector3 childPosition = transform.GetChild(i).position - transform.position;
            Vector2 childPositionV2 = childPosition;
            Vector2[] boxVertices = new Vector2[4];
            boxVertices[0] = childPositionV2 + new Vector2(-2f / 2, -2f / 2);  // Bottom-left
            boxVertices[1] = childPositionV2 + new Vector2(2f / 2, -2f / 2);   // Bottom-right
            boxVertices[2] = childPositionV2 + new Vector2(2f / 2, 2f / 2);    // Top-right
            boxVertices[3] = childPositionV2 + new Vector2(-2f / 2, 2f / 2);   // Top-left

            for(int j = 0; j < 4; j++)
            {
                Point newPoint = new Point(boxVertices[j].x,boxVertices[j].y);
                if(!points.Contains(newPoint))
                {
                    points[index] = newPoint;
                    index++;
                }
            }
            
        }

        List<Point> hull = ComputeConvexHull(points);
        Vector2[] pointsPolygon = new Vector2[hull.Count];

        for(int i = 0; i < pointsPolygon.Length; i++)
        {
            pointsPolygon[i] = new Vector2(hull[i].X, hull[i].Y);
        }

        polygonCollider.SetPath(0, pointsPolygon);
    }

    public List<Point> ComputeConvexHull(Point[] points)
    {
        List<Point> hull = new List<Point>();

        // Sort points by x-coordinate
        //
        //Array.Sort(points);

        SortPoints(points);
        // Compute upper hull
        for (int i = 0; i < points.Length; i++)
        {
            while (hull.Count >= 2
                   && Point.CrossProduct(
                          hull[hull.Count - 2],
                          hull[hull.Count - 1], points[i])
                          <= 0)
            {
                hull.RemoveAt(hull.Count - 1);
            }
            hull.Add(points[i]);
        }

        // Compute lower hull
        int lowerHullIndex = hull.Count + 1;
        for (int i = points.Length - 2; i >= 0; i--)
        {
            while (hull.Count >= lowerHullIndex
                   && Point.CrossProduct(
                          hull[hull.Count - 2],
                          hull[hull.Count - 1], points[i])
                          <= 0)
            {
                hull.RemoveAt(hull.Count - 1);
            }
            hull.Add(points[i]);
        }

        // Remove last point (it's a duplicate of the first
        // point)
        hull.RemoveAt(hull.Count - 1);

        return hull;
    }

    public void SortPoints(Point[] points)
    {
        int n = points.Length;

        // Duyệt qua tất cả các phần tử trong mảng
        for (int i = 0; i < n - 1; i++)
        {
            // So sánh và hoán đổi các phần tử nếu cần thiết
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (points[j].X > points[j + 1].X)
                {
                    // Hoán đổi các phần tử
                    Point temp = points[j];
                    points[j] = points[j + 1];
                    points[j + 1] = temp;
                }
            }
        }
    }
}
