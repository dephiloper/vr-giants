using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GestureDetectionUtility
{
    private const double MaxEndToStartDistance = 150;
    private const double MinAllowedPointDistance = 15;
    private const double MaxCircleRadiusDiviation = 15;
    private const int MinPointCount = 20;
    private static int filesCounter;
    private const int MinCornerAngle = 25;
    private const int MaxCornerAngle = 145;

    public static Result Detect(List<Vector2> rawPoints, bool generateDebugTexture = false, bool generateDebugString = false, bool generateDebugAngleOutput = false){
        var cleanedPoints = CleanPoints(rawPoints);
    
        if (cleanedPoints.Count < MinPointCount) {
            return Result.NotDetectable;
        }
        
        var rebasedPoints = RebasePoints(cleanedPoints);

        // adding first two points to recognize the end to start corner (finish the shape)
        var corners = ExtractCorners(rebasedPoints);
         
        var corner = ExtractCorner(rebasedPoints[0], rebasedPoints[1], rebasedPoints[rebasedPoints.Count - 2], 
            rebasedPoints[rebasedPoints.Count - 1], corners);
        
        if (corner != null)
        {
            corners.Add(corner);
        }
        
        if (generateDebugAngleOutput)
           GenerateDebugAngleOutput(corners);

        Result detectionResult;

        if (!IsClosedShape(rebasedPoints))
        {
            detectionResult = Result.NotDetectable;
        }
        else if (corners.Count < 3) {
            detectionResult = IsCircleShape(rebasedPoints) ? Result.Circle : Result.NotDetectable;
        }
        else if (corners.Count < 4) {
            detectionResult = Result.Triangle;
        }
        else if (corners.Count < 5)
        {

           /* for (var i = 0; i < corners.Count; i++)
            {
                var angle = CalculateAngle(corners[i % 4].P2, corners[(i + 1) % 4].P2, corners[(i+1) % 4].P2, corners[(i + 2) % 4].P2);
                Debug.Log("Angles: " + angle);
            }*/
            
            detectionResult = Result.Square;
        }
        else {
            detectionResult = Result.NotDetectable;
        }

        if (generateDebugTexture) {
            GenerateDebugTexture(rebasedPoints, corners);
        }

        if (generateDebugString) {
            GenerateDebugString(rebasedPoints);
        }
        return detectionResult;
    }

    private static List<Vector2> CleanPoints(List<Vector2> points){
        var cleanedPoints = new List<Vector2>();
        // Todo: sometimes throws null pointer ?
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
            temp.x = (temp.x - minX) / deltaX * 250;
            temp.y = (temp.y - minY) / deltaY * 250;
            rebasedPoints.Add(temp);
        }
        return rebasedPoints;
    }
    
    private static List<Corner> ExtractCorners(List<Vector2> points){
        var corners = new List<Corner>();
        for (var i = 2; i < points.Count; i++)
        {
            var corner = ExtractCorner(points[i - 2], points[i - 1], points[i - 1], points[i], corners);

            if (corner != null)
            {
                corners.Add(corner);
            }
        }
        
        return corners;
    }
    
    private static Corner ExtractCorner(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, List<Corner> corners)
    {
        var angle = CalculateAngle(p1, p2, p3, p4);

        if (angle > MinCornerAngle && angle < MaxCornerAngle)
        {
            var newCorner = new Corner(p1, p2, p3, p4, angle);

            if (corners.Count == 0 || !IsCloseCorner(corners, newCorner))
            {
                return newCorner;
            }
            
            Debug.Log(string.Format("Close Corner Found:\n{0}{1}", corners[corners.Count - 1], newCorner));
        }
        
        return null;
    }
    
    private static double CalculateAngle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        // Todo: NaN is possible
        var gradient1 = CalculateGradient(p1, p2);
        var gradient2 = CalculateGradient(p3, p4);

        var tanA = (gradient1 - gradient2) / (1 + (gradient1 * gradient2));
        var angle = RadToDeg(Math.Atan(tanA));
        if (angle < 0) 
        {
            angle += 180;
        }
        return angle;
    }

    private static float CalculateGradient(Vector2 p1, Vector2 p2)
    {
        return (p2.y - p1.y) / (p2.x - p1.x);
    }

    private static double RadToDeg(double radAngle)
    {
        return (360 / (2 * Math.PI) * radAngle);
    }
    
    private static bool IsCloseCorner(List<Corner> corners, Corner corner){
        for (var i = corners.Count - 1; i > -1; i--) {
            var closeCornerFound =
                corner.P1 == corners[i].P1 ||
                corner.P1 == corners[i].P2 ||
                corner.P1 == corners[i].P3 ||
                corner.P2 == corners[i].P1 ||
                corner.P2 == corners[i].P2 ||
                corner.P2 == corners[i].P3 ||
                corner.P3 == corners[i].P1 ||
                corner.P3 == corners[i].P2 ||
                corner.P3 == corners[i].P3;

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
            deltaDistanceSum += Math.Abs(optimalRadius - Vector2.Distance(point, centerPoint));
        }

        var avgDiviation = deltaDistanceSum / rebasedPoints.Count;

        return avgDiviation < MaxCircleRadiusDiviation;
    }
    
    private static bool IsClosedShape(List<Vector2> points)
    {
        return Vector2.Distance(points[0], points[points.Count - 1]) <= MaxEndToStartDistance;
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
                    debugTexture.SetPixel((int) corners[i].P1.x + j + 25, (int) corners[i].P1.y + k + 25,
                        Color.red);
                    debugTexture.SetPixel((int) corners[i].P2.x + j + 25, (int) corners[i].P2.y + k + 25,
                        Color.blue);
                    debugTexture.SetPixel((int) corners[i].P3.x + j + 25, (int) corners[i].P3.y + k + 25,
                        Color.blue);
                    debugTexture.SetPixel((int) corners[i].P4.x + j + 25, (int) corners[i].P4.y + k + 25,
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
    
    private static void GenerateDebugAngleOutput(List<Corner> corners)
    {
        var angleSum = 0d;

        foreach (var c in corners)
        {
            Debug.Log("Angle " + c.Angle);
            angleSum += c.Angle;
        }

        Debug.Log("Angle Sum " + angleSum);
    }

    internal class Corner
    {
        public Vector2 P1 { get; set; }
        public Vector2 P2 { get; set; }
        public Vector2 P3 { get; set; }
        public Vector2 P4 { get; set; }
        public double Angle { get; set; }

        public Corner(Vector2 p1, Vector2 p2, Vector2 p3, Vector3 p4, double angle){
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
            Angle = angle;
        }

        public override string ToString(){
            return string.Format("P1: {0}, P2: {1}, P3: {2}, P4: {3}, Angle: {3}", P1, P2, P3, P4, Angle) ;
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