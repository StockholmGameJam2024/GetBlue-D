using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    UIDocument uiDocument;
    private Button setupGameButton;
    private Button quitButton;
    void Awake(){
        uiDocument = GetComponent<UIDocument>();
        
        setupGameButton = uiDocument.rootVisualElement.Q<Button>("setup-game-button");
        quitButton = uiDocument.rootVisualElement.Q<Button>("quit-button");
        
        setupGameButton.RegisterCallback<ClickEvent>(OnSetupGameButtonClicked);
        quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
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
