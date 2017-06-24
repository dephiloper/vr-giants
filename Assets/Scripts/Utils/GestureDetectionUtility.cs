using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GestureDetectionUtility
{
    private const double MinAllowedPointDistance = 20;
    private const double MaxCircleRadiusDiviation = 20;
    private const int MinPointCount = 20;
    private static int filesCounter;

    public static Result Detect(List<Vector2> rawPoints, bool generateDebugTexture = false, bool generateDebugString = false){
        var cleanedPoints = CleanPoints(rawPoints);
    
        Debug.Log(cleanedPoints.Count);
        if (cleanedPoints.Count < MinPointCount) {
            return Result.NotDetectable;
        }
        
        var rebasedPoints = RebasePoints(cleanedPoints);

        // adding first two points to recognize the end to start corner (finish the shape)
        rebasedPoints.Add(rebasedPoints[0]);
        rebasedPoints.Add(rebasedPoints[1]);
        var corners = ExtractCorners(rebasedPoints);

        Result detectionResult;
        
        if (corners.Count < 3) {
            detectionResult = IsCircleShape(rebasedPoints) ? Result.Circle : Result.NotDetectable;
        }
        else if (corners.Count < 4) {
            detectionResult = Result.Triangle;
        }
        else if (corners.Count < 5) {
            detectionResult = Result.Square;
        }
        else {
            detectionResult = Result.NotDetectable;
        }

        if (generateDebugTexture) {
            GenerateDebugTexture(rebasedPoints, corners);
        }

        if (generateDebugTexture) {
            GenerateDebugString(rebasedPoints);
        }

        return detectionResult;
    }
    
    private static List<Vector2> CleanPoints(List<Vector2> points){
        var cleanedPoints = new List<Vector2>();
        for (var i = 0; i < points.Count; i++) {
            if (points[i].x > 0 && points[i].y > 0) {
                if (i > 0) {
                    var distanceToLastPoint = Vector2.Distance(cleanedPoints.Last(), points[i]);
                    if (distanceToLastPoint > MinAllowedPointDistance) {
                        cleanedPoints.Add(points[i]);
                    }
                }
                else {
                    cleanedPoints.Add(points[i]);
                }
            }
        }
        return cleanedPoints;
    }

    private static List<Vector2> RebasePoints(List<Vector2> cleanedPoints){
        var maxX = cleanedPoints.Max(value => value.x);
        var maxY = cleanedPoints.Max(value => value.y);
        var minX = cleanedPoints.Min(value => value.x);
        var minY = cleanedPoints.Min(value => value.y);
        var deltaX = maxX - minX;
        var deltaY = maxY - minY;

        var rebasedPoints = new List<Vector2>();
        for (var i = 0; i < cleanedPoints.Count; i++) {
            var temp = new Vector2(cleanedPoints[i].x, cleanedPoints[i].y);
            temp.x = (int) ((temp.x - minX) / deltaX * 250);
            temp.y = (int) ((temp.y - minY) / deltaY * 250);
            rebasedPoints.Add(temp);
        }
        return rebasedPoints;
    }

    private static List<Corner> ExtractCorners(List<Vector2> points){
        var corners = new List<Corner>();
        var lastAngle = 0d;
        for (var i = 1; i < points.Count; i++) {
            var angle = Math.Atan2(points[i].y - points[i - 1].y, points[i].x - points[i - 1].x);
            angle = angle * (180 / Math.PI);
            if (angle < 0) {
                angle += 360;
            }

            if ((Math.Abs(lastAngle - angle) > 50 && Math.Abs(lastAngle - angle) < 310) && i > 1) {
                var newCorner = new Corner(points[i - 2], points[i - 1], points[i], lastAngle - angle);

                if (corners.Count == 0) {
                    corners.Add(newCorner);
                }
                if (!IsCloseCorner(corners, newCorner)) {
                    corners.Add(newCorner);
                }
                else {
                    Debug.Log(string.Format("Close Corner Found:\n{0}{1}", corners[corners.Count - 1], newCorner));
                }
            }

            lastAngle = angle;
        }

        return corners;
    }
    
    private static bool IsCloseCorner(List<Corner> corners, Corner corner){
        for (var i = corners.Count - 1; i > -1; i--) {
            var closeCornerFound =
                corner.Point1 == corners[i].Point1 ||
                corner.Point1 == corners[i].Point2 ||
                corner.Point1 == corners[i].Point3 ||
                corner.Point2 == corners[i].Point1 ||
                corner.Point2 == corners[i].Point2 ||
                corner.Point2 == corners[i].Point3 ||
                corner.Point3 == corners[i].Point1 ||
                corner.Point3 == corners[i].Point2 ||
                corner.Point3 == corners[i].Point3;

            if (closeCornerFound)
                return true;
        }

        return false;
    }

    private static bool IsCircleShape(List<Vector2> rebasedPoints){
        var deltaDistanceSum = 0d;
        const int optimalRadius = 125;
        var centerPoint = new Vector2(optimalRadius, optimalRadius);

        foreach (var point in rebasedPoints) {
            deltaDistanceSum += optimalRadius - Vector2.Distance(point, centerPoint);
        }

        var avgDiviation = deltaDistanceSum / rebasedPoints.Count;

        return avgDiviation < MaxCircleRadiusDiviation;
    }

    private static void GenerateDebugTexture(List<Vector2> rebasedPoints, List<Corner> corners){
        var debugTexture = new Texture2D(300, 300, TextureFormat.ARGB32, false);

        for (var i = 0; i < rebasedPoints.Count; i++) {
            for (var j = -1; j < 2; j++) {
                for (var k = -1; k < 2; k++) {
                    debugTexture.SetPixel((int) rebasedPoints[i].x + j + 25, (int) rebasedPoints[i].y + k + 25,
                        Color.gray);
                }
            }
        }

        for (var i = 0; i < corners.Count; i++) {
            for (var j = -1; j < 2; j++) {
                for (var k = -1; k < 2; k++) {
                    debugTexture.SetPixel((int) corners[i].Point1.x + j + 25, (int) corners[i].Point1.y + k + 25,
                        Color.red);
                    debugTexture.SetPixel((int) corners[i].Point2.x + j + 25, (int) corners[i].Point2.y + k + 25,
                        Color.blue);
                    debugTexture.SetPixel((int) corners[i].Point3.x + j + 25, (int) corners[i].Point3.y + k + 25,
                        Color.green);
                }
            }
        }

        File.WriteAllBytes(string.Format("./debugTexture{0}.png", filesCounter++), debugTexture.EncodeToPNG());
    }

    private static void GenerateDebugString(List<Vector2> points){
        var s = string.Empty;
        for (var i = 0; i < points.Count; i++) {
            if (i != 0) {
                s += "|";
            }
            s += points[i].x + "," + points[i].y;
        }
        Debug.Log(s);
    }

    internal class Corner
    {
        public Vector2 Point1 { get; set; }
        public Vector2 Point2 { get; set; }
        public Vector2 Point3 { get; set; }
        public double Angle { get; set; }

        public Corner(Vector2 point1, Vector2 point2, Vector2 point3, double angle){
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Angle = angle;
        }

        public override string ToString(){
            return string.Format("Point1: {0}, Point2: {1}, Point3: {2}, Angle: {3}", Point1, Point2, Point3, Angle) ;
        }
    }

    public enum Result
    {
        Circle,
        Triangle,
        Square,
        NotDetectable
    }    
}