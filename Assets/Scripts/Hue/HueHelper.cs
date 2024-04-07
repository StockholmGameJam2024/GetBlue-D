using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class HueHelper
{
    public static Color[] GenerateEvenlyDistributedColors(int count)
    {
        Color[] colors = new Color[count];
        float randomHue = Random.Range(0, 1f);
        for (int i = 0; i < count; i++)
        {
            colors[i] = Color.HSVToRGB((randomHue + (float)i / count)%1f, 1, 1);
        }

        return colors;
    }

    public static Color MoveHueTowards(Color currentColor, Color targetColor, float maxHueDistance)
    {
        Color.RGBToHSV(currentColor, out float currentHue, out var _s, out var _v);
        Color.RGBToHSV(targetColor, out float targetHue, out _, out _);
        float newHue = HueHelper.MoveTowards(currentHue, targetHue, maxHueDistance);
        return Color.HSVToRGB(newHue,_s,_v);
    }

    public static float MoveTowards(float currentHue, float targetHue, float maxDistance)
    {
        // Calculate the absolute difference between targetHue and currentHue
        float absoluteDifference = Mathf.Abs(targetHue - currentHue);

        // If the absolute difference is greater than 0.5, we need to move in the opposite direction
        if (absoluteDifference > 0.5f)
        {
            // Move towards the target hue by subtracting or adding 1 depending on the direction
            if (currentHue < targetHue)
                currentHue += 1.0f;
            else
                targetHue += 1.0f;
        }

        // Calculate the direction and distance to move towards the target hue
        float direction = Mathf.Sign(targetHue - currentHue);
        float distance = Mathf.Min(Mathf.Abs(targetHue - currentHue), maxDistance);

        // Move towards the target hue
        currentHue += direction * distance;

        // Wrap around if the hue exceeds the range [0, 1]
        if (currentHue < 0)
            currentHue += 1.0f;
        else if (currentHue > 1)
            currentHue -= 1.0f;

        float result = (float)Math.Round(currentHue, 3);

        return result;
    }
}