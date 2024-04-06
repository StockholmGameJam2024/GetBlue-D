using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public interface IColorable
{
    void ChangeColorTint(Color newColor, float colorStrength);
    Color CurrentColor{get;set;}
}

public interface IScorer
{
    float Score { get; set; }
}

public class Player : MonoBehaviour, IColorable, IScorer
{

    [FormerlySerializedAs("colorChanger")] public PlayerSpawner spawner;

    Color _currentColor;
    public Color targetColor;
    private float _score;

    public UnityEvent<float> ScoreChange;
    public Color CurrentColor
    {
        get => _currentColor;
        set
        {
            _currentColor = value;
            GetComponent<SpriteRenderer>().color = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<PlayerSpawner>();
    }

    public void ChangeColorTint(Color newColor, float colorStrength)
    {
        _currentColor = HueHelper.MoveHueTowards(_currentColor, newColor, colorStrength);
        GetComponent<SpriteRenderer>().color = _currentColor;
    }


    public float Score
    {
        get => _score;
        set
        {
            _score = value;
            hud.GetComponentInChildren<TMP_Text>().text = value.ToString("0000");
        }
    }

    public void SetHUD(Image playerHuD)
    {
        this.hud = playerHuD;
    }

    public Image hud { get; set; }
}
