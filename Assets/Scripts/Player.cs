using System;
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
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.reverbZoneMix = 0;
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
