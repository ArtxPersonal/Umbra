// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Moster/Inputs/GameManagerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameManagerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameManagerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameManagerControls"",
    ""maps"": [
        {
            ""name"": ""GameManager"",
            ""id"": ""410898e1-b47c-4050-9d0c-d901221aed3e"",
            ""actions"": [
                {
                    ""name"": ""SwitchCam"",
                    ""type"": ""Button"",
                    ""id"": ""81b36322-eb8d-47bc-9bf8-2e557b65cb69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""522aa6c6-b4af-4d05-9636-64863b0437f9"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GameManager
        m_GameManager = asset.FindActionMap("GameManager", throwIfNotFound: true);
        m_GameManager_SwitchCam = m_GameManager.FindAction("SwitchCam", throwIfNotFound: true);
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

    // GameManager
    private readonly InputActionMap m_GameManager;
    private IGameManagerActions m_GameManagerActionsCallbackInterface;
    private readonly InputAction m_GameManager_SwitchCam;
    public struct GameManagerActions
    {
        private @GameManagerControls m_Wrapper;
        public GameManagerActions(@GameManagerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwitchCam => m_Wrapper.m_GameManager_SwitchCam;
        public InputActionMap Get() { return m_Wrapper.m_GameManager; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameManagerActions set) { return set.Get(); }
        public void SetCallbacks(IGameManagerActions instance)
        {
            if (m_Wrapper.m_GameManagerActionsCallbackInterface != null)
            {
                @SwitchCam.started -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnSwitchCam;
                @SwitchCam.performed -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnSwitchCam;
                @SwitchCam.canceled -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnSwitchCam;
            }
            m_Wrapper.m_GameManagerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwitchCam.started += instance.OnSwitchCam;
                @SwitchCam.performed += instance.OnSwitchCam;
                @SwitchCam.canceled += instance.OnSwitchCam;
            }
        }
    }
    public GameManagerActions @GameManager => new GameManagerActions(this);
    public interface IGameManagerActions
    {
        void OnSwitchCam(InputAction.CallbackContext context);
    }
}
