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

    
    
    public void SetCurrentColor(Color color)
    {
        current.color = color;
    }

    public void SetTargetColor(Color color)
    {
        target.color = color;
    }

    public void SetScore(float newScore)
    {
        score.text = newScore.ToString("0000");
    }

    public void SetScoreRate(float newScoreRate)
    {
        scoreRate.text = newScoreRate.ToString("00.0%");
    }
}