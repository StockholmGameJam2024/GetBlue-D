using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameScoreController : MonoBehaviour
{
    public AnimationCurve scoreCurve;
    public float targetScore;
    public UnityEvent<Player> PlayerWin;
    [SerializeField] bool started;
    
    List<Player> players = new();

    public void RegisterPlayer(Player player)
    {
        players.Add(player);
    }

    public void StartGame()
    {
        started = true;
    }
    
    private void FixedUpdate()
    {
        if (!started)
        {
            return;
        }
        foreach (var player in players)
        {
            player.Score += GetScoreForColor(player.CurrentColor, player.targetColor);
        }

        var winnerList = from player in players
            where player.Score > targetScore
            orderby player.Score descending
            select player;
        
        var winner = winnerList.FirstOrDefault();
        if (winner != null)
        {
            PlayerWin.Invoke(winner);
            enabled = false;
        }
    }

    float GetScoreForColor(Color currentColor, Color targetColor)
    {
        Color.RGBToHSV(currentColor, out var _currentHue, out _, out _);
        Color.RGBToHSV(targetColor, out var _targetHue, out _, out _);

        float hueDistance = MathsExtensions.DistanceBetween01UnClamped(_currentHue, _targetHue);
        float scoreSample = 1 - 2 * hueDistance;
        float scoreMultiplier = scoreCurve.Evaluate(scoreSample);
        return scoreMultiplier;
    }
}