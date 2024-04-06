using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    UIDocument uiDocument;
    private Button setupGameButton;
    private Button quitButton;

    private List<Button> buttons;
    void Awake(){
        uiDocument = GetComponent<UIDocument>();
        
        setupGameButton = uiDocument.rootVisualElement.Q<Button>("setup-game-button");
        quitButton = uiDocument.rootVisualElement.Q<Button>("quit-button");
        buttons = uiDocument.rootVisualElement.Query<Button>().ToList();
        
        setupGameButton.RegisterCallback<ClickEvent>(OnSetupGameButtonClicked);
        quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].RegisterCallback<ClickEvent>(OnAllButtonsClicked);
        }
    }


    private void OnDisable()
    {
        setupGameButton.UnregisterCallback<ClickEvent>(OnSetupGameButtonClicked);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClicked);
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClicked);
        }
    }

    private void OnAllButtonsClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        //This will be called when any button is clicked
        //Play Sound
    }

    private void OnSetupGameButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Setup Game Button Clicked");
    }
    
    private void OnQuitButtonClicked<TEventType>(TEventType evt) where TEventType : EventBase<TEventType>, new()
    {
        Debug.Log("Quit Button Clicked");
    }
}
