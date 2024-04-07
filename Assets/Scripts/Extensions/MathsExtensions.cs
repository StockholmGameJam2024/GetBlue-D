using UnityEngine;

public static class MathsExtensions
{
    public static float DistanceBetween01UnClamped(float current, float target)
    {
        var distance = Mathf.Abs(current - target);
        return distance > 0.5f ? 1f - distance : distance;
    }
}