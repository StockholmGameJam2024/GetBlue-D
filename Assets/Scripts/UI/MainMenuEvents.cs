using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    UIDocument uiDocument;
    private Button startGameButton;
    private Button quitButton;
    private Button creditsButton;
    
    private Toggle player1Toggle;
    private Toggle player2Toggle;
    private Toggle player3Toggle;
    private Toggle player4Toggle;

    private List<Button> buttons;

    public AudioClip startGameAudio;
    public AudioClip selectAudio;
    public AudioClip hoverAudio;
    
    private AudioSource audioSource;
    void Awake(){
        uiDocument = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();
        
        startGameButton = uiDocument.rootVisualElement.Q<Button>("start-game-button");
        creditsButton = uiDocument.rootVisualElement.Q<Button>("credits-button");
        quitButton = uiDocument.rootVisualElement.Q<Button>("quit-button");
        
        player1Toggle = uiDocument.rootVisualElement.Q<Toggle>("player1-toggle");
        player2Toggle = uiDocument.rootVisualElement.Q<Toggle>("player2-toggle");
        player3Toggle = uiDocument.rootVisualElement.Q<Toggle>("player3-toggle");
        player4Toggle = uiDocument.rootVisualElement.Q<Toggle>("player4-toggle");
        
        buttons = uiDocument.rootVisualElement.Query<Button>().ToList();
        
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
        
        
        startGameButton.Focus();
        
    }

    private void OnDisable()
    {
        startGameButton.UnregisterCallback<ClickEvent>(OnStartGameButtonClicked);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClicked);
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].UnregisterCallback<MouseOverEvent>(OnAllButtonsHovered);
            if( buttons[i] == startGameButton){
                continue;
            }
            buttons[i].UnregisterCallback<ClickEvent>(OnAnyButtonClicked);
        }
    }
    
    private void OnStartGameButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Setup Game Button Clicked");
        audioSource.clip = startGameAudio;
        audioSource.Play();
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
        audioSource.clip = hoverAudio;
        audioSource.Play();
    }

    private void OnAnyButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Any Button Clicked");
        audioSource.clip = selectAudio;
        audioSource.Play();
    }
    
   
}
