//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/InputMaster.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMaster: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player_Map"",
            ""id"": ""71016613-0829-4faa-8b07-1f0350816bdf"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""db0f35ab-5bc3-4c1b-9dca-62f260cedb48"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Spring"",
                    ""type"": ""Button"",
                    ""id"": ""f79d32b3-49c3-45ca-ace2-43195985cbbf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""893a0376-bb95-48b1-84ce-b43e2b7e4503"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e3c779e1-9d73-4395-beae-1344cfe068cd"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""380f4d40-5886-420c-b117-4a1786392e2e"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""52a875cc-b70f-4ed2-908e-71bbaf35b1b3"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""562b0e1c-27b1-473c-aa1a-f23c7c5334e3"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5b0c47ba-ed71-42c1-9210-602a1a1b0c4b"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fd87fef9-c6d3-4b18-ba98-340f8c79b3cf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""df22d2b5-1846-46b7-b5f8-a7e7cc345fa2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce1357bf-114a-4f29-b635-6fbc15175c86"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d9267906-452d-4598-9664-22d5707bba20"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c53468cd-5cf6-404e-812f-a48919e62621"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spring"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c58d1c01-c7fc-4fc6-8324-a9838ff1cecd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spring"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Merry Christmas Xbox"",
            ""bindingGroup"": ""Merry Christmas Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player_Map
        m_Player_Map = asset.FindActionMap("Player_Map", throwIfNotFound: true);
        m_Player_Map_Movement = m_Player_Map.FindAction("Movement", throwIfNotFound: true);
        m_Player_Map_Spring = m_Player_Map.FindAction("Spring", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player_Map
    private readonly InputActionMap m_Player_Map;
    private List<IPlayer_MapActions> m_Player_MapActionsCallbackInterfaces = new List<IPlayer_MapActions>();
    private readonly InputAction m_Player_Map_Movement;
    private readonly InputAction m_Player_Map_Spring;
    public struct Player_MapActions
    {
        private @InputMaster m_Wrapper;
        public Player_MapActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Map_Movement;
        public InputAction @Spring => m_Wrapper.m_Player_Map_Spring;
        public InputActionMap Get() { return m_Wrapper.m_Player_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_MapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_MapActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_MapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_MapActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Spring.started += instance.OnSpring;
            @Spring.performed += instance.OnSpring;
            @Spring.canceled += instance.OnSpring;
        }

        private void UnregisterCallbacks(IPlayer_MapActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Spring.started -= instance.OnSpring;
            @Spring.performed -= instance.OnSpring;
            @Spring.canceled -= instance.OnSpring;
        }

        public void RemoveCallbacks(IPlayer_MapActions instance)
        {
            if (m_Wrapper.m_Player_MapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_MapActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_MapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_MapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_MapActions @Player_Map => new Player_MapActions(this);
    private int m_MerryChristmasXboxSchemeIndex = -1;
    public InputControlScheme MerryChristmasXboxScheme
    {
        get
        {
            if (m_MerryChristmasXboxSchemeIndex == -1) m_MerryChristmasXboxSchemeIndex = asset.FindControlSchemeIndex("Merry Christmas Xbox");
            return asset.controlSchemes[m_MerryChristmasXboxSchemeIndex];
        }
    }
    public interface IPlayer_MapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSpring(InputAction.CallbackContext context);
    }
}
