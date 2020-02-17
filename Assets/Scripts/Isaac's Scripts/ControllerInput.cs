// GENERATED AUTOMATICALLY FROM 'Assets/Scenes/ControllerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControllerInput : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @ControllerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControllerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""c5373d63-6025-46fc-a294-c94632d4405d"",
            ""actions"": [
                {
                    ""name"": ""Switch States Up"",
                    ""type"": ""Button"",
                    ""id"": ""7df1d2d1-2a19-41ce-a9e8-f3a07fe990fc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch States Down"",
                    ""type"": ""Button"",
                    ""id"": ""9f42cf7f-50b3-4cde-8093-cc07453841a8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""da8c2080-67a6-4be6-a418-8f20a9accb5e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""Button"",
                    ""id"": ""66d050e4-be5a-404b-911e-b29b657ac222"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hook"",
                    ""type"": ""Button"",
                    ""id"": ""126b90ce-6c29-4853-acc3-cc2b3e2cc3b1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d0bc0e9d-91fa-429c-80c6-18bfcc746c67"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2af30cb5-0870-434e-b952-773c8d5522b7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""f3b6ba82-52b4-4116-9008-0a092689c76a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e50e8ffa-8db1-46f9-81a5-f7d5a70afa7e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae1b88ae-c293-4793-aedc-acdf11d0bf5f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""853be2cf-674e-43ae-9a68-cb174f4c2078"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch States Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8edbfa7-23df-4fb4-9c42-49ab31eebc98"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch States Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11840dac-97f2-4614-a072-d383fe655fed"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11e3f245-020a-45da-96d8-1c3fb390d023"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03e5f808-d0b2-4ea7-b51c-eee1b1c122fb"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80d38d49-b4f5-418a-988c-dac012c5c98a"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_SwitchStatesUp = m_Gameplay.FindAction("Switch States Up", throwIfNotFound: true);
        m_Gameplay_SwitchStatesDown = m_Gameplay.FindAction("Switch States Down", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Camera = m_Gameplay.FindAction("Camera", throwIfNotFound: true);
        m_Gameplay_Hook = m_Gameplay.FindAction("Hook", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_SwitchStatesUp;
    private readonly InputAction m_Gameplay_SwitchStatesDown;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Camera;
    private readonly InputAction m_Gameplay_Hook;
    private readonly InputAction m_Gameplay_Interact;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Aim;
    public struct GameplayActions
    {
        private @ControllerInput m_Wrapper;
        public GameplayActions(@ControllerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwitchStatesUp => m_Wrapper.m_Gameplay_SwitchStatesUp;
        public InputAction @SwitchStatesDown => m_Wrapper.m_Gameplay_SwitchStatesDown;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Camera => m_Wrapper.m_Gameplay_Camera;
        public InputAction @Hook => m_Wrapper.m_Gameplay_Hook;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @SwitchStatesUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchStatesUp;
                @SwitchStatesUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchStatesUp;
                @SwitchStatesUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchStatesUp;
                @SwitchStatesDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchStatesDown;
                @SwitchStatesDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchStatesDown;
                @SwitchStatesDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchStatesDown;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Camera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCamera;
                @Hook.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHook;
                @Hook.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHook;
                @Hook.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHook;
                @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwitchStatesUp.started += instance.OnSwitchStatesUp;
                @SwitchStatesUp.performed += instance.OnSwitchStatesUp;
                @SwitchStatesUp.canceled += instance.OnSwitchStatesUp;
                @SwitchStatesDown.started += instance.OnSwitchStatesDown;
                @SwitchStatesDown.performed += instance.OnSwitchStatesDown;
                @SwitchStatesDown.canceled += instance.OnSwitchStatesDown;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Hook.started += instance.OnHook;
                @Hook.performed += instance.OnHook;
                @Hook.canceled += instance.OnHook;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnSwitchStatesUp(InputAction.CallbackContext context);
        void OnSwitchStatesDown(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnHook(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
