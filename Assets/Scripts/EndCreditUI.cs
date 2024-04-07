using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCreditUI : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 20f;
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSettings audioSettings;

    private void Update()
    {
        transform.Translate(Camera.main.transform.up * (_scrollSpeed * Time.deltaTime));
        
        //Somewhere detect input, or when the credits are done, call the following method
        //ReturnToMainGame();
    }

    private void Awake()
    {
        musicPlayer.clip = audioSettings.creditsMusic;
        musicPlayer.Play();
    }
    [ContextMenu( "Return to Main Scene")]
    private void ReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
