using System.Collections.Generic;
using UnityEngine;

public class BeizerUtil
{
    private static Vector3 GetBeizerPoint(float t, Vector3 originPoint, Vector3 controlPoint, Vector3 endPoint)
    {
        return (1 - t) * (1 - t) * originPoint + 2 * t * (1 - t) * controlPoint + t * t * endPoint;
    }

    public static List<Vector3> GetBeizerPointList(Vector3 originPoint, Vector3 controlPoint, Vector3 endPoint,
        int pointCount)
    {
        List<Vector3> targetPointList = new List<Vector3>();
        for (int i = 0; i < pointCount; i++)
        {
            float t = (float) i / pointCount;
            Vector3 targetPoint = GetBeizerPoint(t, originPoint, controlPoint, endPoint);
            targetPointList.Add(targetPoint);
        }

        return targetPointList;
    }
}