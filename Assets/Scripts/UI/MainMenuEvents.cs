using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private Label masterVolumeLabel;

    
    
    void Start(){
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
        masterVolumeLabel = uiDocument.rootVisualElement.Q<Label>("master-volume-label");
        
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
        
        masterVolumeSlider.value = audioSettings.masterVolume; //-80 to make the slider start at 0. Mixer values will be -80 to 0.
        masterVolumeLabel.text = $"{audioSettings.GetVolumeToDisplay()}";
        audioSettings.SetMixerVolume();

        StartMenuMusic();
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
            yield return new WaitForSeconds(audioSettings.introMenuMusic.length-1f); 
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
        var playGameButton = uiDocument.rootVisualElement.Q<Button>("play-game-button");
        playGameButton.text = "Resume Game";
    }

    [ContextMenu("Unpause Game")]
    public void UnpauseGame()
    {
        audioSettings.DeactivateLowPassFilter();
        uiDocument.enabled = true;
    }
}
