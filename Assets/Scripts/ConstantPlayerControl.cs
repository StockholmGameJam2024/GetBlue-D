using UnityEngine;
using UnityEngine.InputSystem;

public class ConstantPlayerControl : MonoBehaviour
{
    private InputActionMap playerControls;

    public InputAction Turn { get; private set; }

    void Awake()
    {
        playerControls = new InputActionMap("Player");
        
        Turn = playerControls.AddAction("Turn", binding: "<Gamepad>/leftStick/x");
        
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
}