using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudElement : MonoBehaviour
{
    public Image current;
    public Image target;
    public TMP_Text score;
    public TMP_Text scoreRate;
    public RectTransform hueBarsTransform;
    public RectTransform[] targetHueIndicators;
    private bool initializedAnchoredPosition;

    private float _targetAnchoredPositionX;
    private float _anchoredPositionVelocityX;

    void Update()
    {
        float newX = Mathf.SmoothDamp(hueBarsTransform.anchoredPosition.x, _targetAnchoredPositionX, ref _anchoredPositionVelocityX, 0.2f, 100f);
        hueBarsTransform.anchoredPosition = new Vector2(newX, hueBarsTransform.anchoredPosition.y);
    }
    
    public void SetCurrentColor(Color color)
    {
        Color.RGBToHSV(color, out var hue, out _, out _);
        Debug.Log(hue);
        _targetAnchoredPositionX = (-hue - 1) * 300f;
        float difference = _targetAnchoredPositionX - hueBarsTransform.anchoredPosition.x;
        if (difference > 150)
        {
            hueBarsTransform.anchoredPosition = new Vector2(hueBarsTransform.anchoredPosition.x + 300, hueBarsTransform.anchoredPosition.y);
        } else if (difference < -150)
        {
            hueBarsTransform.anchoredPosition = new Vector2(hueBarsTransform.anchoredPosition.x - 300, hueBarsTransform.anchoredPosition.y);
        }

        if (!initializedAnchoredPosition)
        {
            initializedAnchoredPosition = true;
            hueBarsTransform.anchoredPosition = new Vector2(_targetAnchoredPositionX, hueBarsTransform.anchoredPosition.y);
        }
        current.color = color;
    }

    public void SetTargetColor(Color color)
    {
        target.color = color;
        Color.RGBToHSV(color, out var hue, out _, out _);
        for (int i = 0; i < targetHueIndicators.Length; i++)
        {
            targetHueIndicators[i].anchoredPosition = new Vector2((hue + i) * 300f, targetHueIndicators[i].anchoredPosition.y);
        }
    }

    public void SetScore(float newScore)
    {
        score.text = newScore.ToString("0000");
    }

    public void SetScoreRate(float newScoreRate)
    {
        Debug.Log(newScoreRate);
        scoreRate.text = $"{newScoreRate:P1}.";
    }
}
