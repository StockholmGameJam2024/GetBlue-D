using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public AudioSettings audioSettings;
    public AudioSource uiSoundPlayer;
    public AudioSource musicPlayer;
    
    UIDocument uiDocument;
    private Button startGameButton;
    private Button quitButton;
    private Button creditsButton;
    
    private Toggle player1Toggle;
    private Toggle player2Toggle;
    private Toggle player3Toggle;
    private Toggle player4Toggle;

    private List<Button> buttons;
    
    private Slider masterVolumeSlider;
    private Slider sfxVolumeSlider;
    private Label masterVolumeLabel;
    private Label sfxVolumeLabel;
    

    
    
    void Start()
    {
        InitUI();
        StartMenuMusic();
    }

    private void InitUI()
    {
        uiDocument = GetComponent<UIDocument>();
        
        startGameButton = uiDocument.rootVisualElement.Q<Button>("start-game-button");
        creditsButton = uiDocument.rootVisualElement.Q<Button>("credits-button");
        quitButton = uiDocument.rootVisualElement.Q<Button>("quit-button");
        
        player1Toggle = uiDocument.rootVisualElement.Q<Toggle>("player1-toggle");
        player2Toggle = uiDocument.rootVisualElement.Q<Toggle>("player2-toggle");
        player3Toggle = uiDocument.rootVisualElement.Q<Toggle>("player3-toggle");
        player4Toggle = uiDocument.rootVisualElement.Q<Toggle>("player4-toggle");
        
        buttons = uiDocument.rootVisualElement.Query<Button>().ToList();
        
        masterVolumeSlider = uiDocument.rootVisualElement.Q<Slider>("master-volume-slider");
        sfxVolumeSlider = uiDocument.rootVisualElement.Q<Slider>("sfx-volume-slider");
        masterVolumeLabel = uiDocument.rootVisualElement.Q<Label>("master-volume-label");
        sfxVolumeLabel = uiDocument.rootVisualElement.Q<Label>("sfx-volume-label");
        
        startGameButton.RegisterCallback<ClickEvent>(OnStartGameButtonClicked);
        creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClicked);
        quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].RegisterCallback<MouseOverEvent>(OnAllButtonsHovered);
            if( buttons[i] == startGameButton){
                continue;
            }
            buttons[i].RegisterCallback<ClickEvent>(OnAnyButtonClicked);
        }
        
        masterVolumeSlider.RegisterCallback<ChangeEvent<float>>(OnMasterVolumeSliderChanged);
        sfxVolumeSlider.RegisterCallback<ChangeEvent<float>>(OnSFXVolumeSliderChanged);
        
        SetVolumeSliderValues();

    }

    private void SetVolumeSliderValues()
    {
        masterVolumeSlider.value = audioSettings.masterVolume; 
        sfxVolumeSlider.value = audioSettings.sfxVolume;
        masterVolumeLabel.text = $"{audioSettings.GetVolumeToDisplay()}";
        sfxVolumeLabel.text = $"{audioSettings.GetSFXVolumeToDisplay()}";
        audioSettings.SetMixerVolume();
    }

    private void OnSFXVolumeSliderChanged(ChangeEvent<float> evt)
    {
        audioSettings.SetSFXVolume(evt.newValue);
        sfxVolumeLabel.text = $"{audioSettings.GetSFXVolumeToDisplay()}";
    }


    private void OnDisable()
    {
        startGameButton.UnregisterCallback<ClickEvent>(OnStartGameButtonClicked);
        creditsButton.UnregisterCallback<ClickEvent>(OnCreditsButtonClicked);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClicked);
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].UnregisterCallback<MouseOverEvent>(OnAllButtonsHovered);
            if( buttons[i] == startGameButton){
                continue;
            }
            buttons[i].UnregisterCallback<ClickEvent>(OnAnyButtonClicked);
        }
        masterVolumeSlider.UnregisterCallback<ChangeEvent<float>>(OnMasterVolumeSliderChanged);
        sfxVolumeSlider.UnregisterCallback<ChangeEvent<float>>(OnSFXVolumeSliderChanged);
    }
    
    private void OnStartGameButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Setup Game Button Clicked");
        uiSoundPlayer.clip = audioSettings.startGameButtonAudio;
        uiSoundPlayer.Play();

        StartCoroutine(nameof(ActivateGameMusic));
        
        //Activate or instantiate players here
        
        uiDocument.enabled = false;
    }
    
    private IEnumerator ActivateMenuMusic()
    {

        if (audioSettings.introMenuMusic != null)
        {
            musicPlayer.clip = audioSettings.introMenuMusic;
            musicPlayer.Play();
            musicPlayer.loop = false;
            //Music has lag between the end and the start of the next clip, either fix the clips or tweak this offset
            yield return new WaitForSeconds(audioSettings.introMenuMusic.length-0.125f); 
            musicPlayer.time = 0;
        }
        musicPlayer.clip = audioSettings.menuMusic;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }
    private IEnumerator ActivateGameMusic()
    {
        if(audioSettings.startGameIntroAudio != null){
            musicPlayer.clip = audioSettings.startGameIntroAudio;
            musicPlayer.Play();
            musicPlayer.loop = false;
            yield return new WaitForSeconds(audioSettings.startGameIntroAudio.length);
        }
       
        musicPlayer.clip = audioSettings.gameMusic;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }
    
    private void OnCreditsButtonClicked(ClickEvent evt)
    {
        Debug.Log("Credits Button Clicked");
        //Activate Credits here
        SceneManager.LoadScene("EndCreditsScene");
       // throw new NotImplementedException();
    }
    
    private void OnQuitButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
    
    private void OnAllButtonsHovered(MouseOverEvent evt)
    {
        uiSoundPlayer.clip = audioSettings.hoverButtonAudio;
        uiSoundPlayer.Play();
    }

    private void OnAnyButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Any Button Clicked");
        uiSoundPlayer.clip = audioSettings.clickedButtonAudio;
        uiSoundPlayer.Play();
    }
    
    private void OnMasterVolumeSliderChanged(ChangeEvent<float> evt)
    {
        audioSettings.SetMasterVolume(evt.newValue);
        masterVolumeLabel.text = $"{audioSettings.GetVolumeToDisplay()}";
    }
    
    private void StartMenuMusic()
    {
        StartCoroutine(ActivateMenuMusic());
    }
    [ContextMenu("Pause Game")]
    public void PauseGame()
    {
        audioSettings.ActivateLowPassFilter();
        uiDocument.enabled = true;
        InitUI();
        var playGameButton = uiDocument.rootVisualElement.Q<Button>("start-game-button");
        playGameButton.text = "Resume Game";
        
    }

    [ContextMenu("Unpause Game")]
    public void UnpauseGame()
    {
        audioSettings.DeactivateLowPassFilter();
        uiDocument.enabled = true;
    }
}
