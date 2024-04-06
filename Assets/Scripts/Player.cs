using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public interface IColorable
{
    void ChangeColorTint(Color newColor, float colorStrength);
    Color CurrentColor{get;set;}
}

public class Player : MonoBehaviour, IColorable
{

    [FormerlySerializedAs("colorChanger")] public PlayerSpawner spawner;

    Color _currentColor;
    public Color targetColor;

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
    
    
}
