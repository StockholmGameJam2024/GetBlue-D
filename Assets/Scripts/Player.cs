using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public interface IColorable
{
    void ChangeColorTint(Color newColor, float colorStrength);
    Color CurrentColor{get;set;}
}

// spawner registers new player with hud controller
// hud controller instantiates hud prefab
// assigns it to player.HUD
// player, when anythinng changes, e,g. CurrentColor
// then does: if(hud != null) hud.color = newColor;

public interface IScorer
{
    float Score { get; set; }
}

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, IColorable, IScorer
{

    [FormerlySerializedAs("colorChanger")] public PlayerSpawner spawner;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    public List<AudioClip> playerHitSounds;
    public List<AudioClip> wallHitSounds;
    public List<AudioClip> blobHitSounds;

    public UnityEvent<float> ScoreChange;
    public Color targetColor;

    private Color _currentColor;
    private float _score;
    private AudioSource _audioSource;


    private HudElement _newHud;

    public void SetHUD(HudElement element)
    {
        _newHud = element;
        element.SetCurrentColor(_currentColor);
        element.SetTargetColor(targetColor);
        element.SetScore(_score);
    }

    public Color CurrentColor
    {
        get => _currentColor;
        set
        {
            _currentColor = value;
            if(TryGetComponent(out SpriteRenderer spriteRenderer))
                spriteRenderer.color = value;
            if(TryGetComponent(out MeshRenderer meshRenderer))
                meshRenderer.material.color = value;
            _newHud?.SetCurrentColor(_currentColor);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Bounding box in the "middle ish" of the screen where we'll try to spawn the player
        var topLeft = Camera.main!.ScreenToWorldPoint(Vector2.zero) / 2;
        var bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) / 2;
        var thisCollider = GetComponent<CircleCollider2D>();
        var i = 0;
        
        while (true)
        {
            var newPosition = new Vector2(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y));
            var collisions = Physics.OverlapSphere(newPosition, thisCollider.radius);

            if (collisions.Length == 0)
            {
                thisCollider.transform.position = newPosition;
                break;
            }

            i++;
            if (i <= 1000)
                continue;
            
            Debug.LogError("Couldn't find non-colliding area to spawn user");
            thisCollider.transform.position = newPosition;
            break;
        }

        spawner = GetComponent<PlayerSpawner>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.reverbZoneMix = 0;
    }

    public void ChangeColorTint(Color newColor, float colorStrength)
    {
        CurrentColor = HueHelper.MoveHueTowards(_currentColor, newColor, colorStrength);
    }


    public float Score
    {
        get => _score;
        set
        {
            _score = value;
            _newHud?.SetScore(value);
        }
    }

    public void SetHUD(Image playerHuD)
    {
        this.hud = playerHuD;
    }

    public Image hud { get; set; }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if(playerHitSounds.Count <= 0)
            {
                Debug.LogWarning("No player hit sounds found",this);
                return;
            }
            _audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            _audioSource.PlayOneShot(playerHitSounds[UnityEngine.Random.Range(0, playerHitSounds.Count)]);
            
        }
        else if(other.transform.CompareTag("Wall"))
        {
            if(wallHitSounds.Count <= 0)
            {
                Debug.LogWarning("No wall hit sounds found",this);
                return;
            }
            _audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            _audioSource.PlayOneShot(wallHitSounds[UnityEngine.Random.Range(0, wallHitSounds.Count)]);
        }
        else if(other.transform.CompareTag("Blob"))
        {
            if(blobHitSounds.Count <= 0)
            {
                Debug.LogWarning("No blob hit sounds found",this);
                return;
            }
            _audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            _audioSource.PlayOneShot(blobHitSounds[UnityEngine.Random.Range(0, blobHitSounds.Count)]);
        }
    }
}
