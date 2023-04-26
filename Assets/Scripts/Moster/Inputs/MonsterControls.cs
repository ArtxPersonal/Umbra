// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Moster/Inputs/MonsterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MonsterControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MonsterControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MonsterControls"",
    ""maps"": [
        {
            ""name"": ""LandControls"",
            ""id"": ""5717bc37-6a23-4b99-a24d-e7c5918075c1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""459dfc48-911b-416e-9def-153de5a83dff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PsyWall"",
                    ""type"": ""Button"",
                    ""id"": ""7410f5bb-c0a5-420f-8746-ec893ed1e43a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FakeMan"",
                    ""type"": ""Button"",
                    ""id"": ""e06c040a-735c-4bed-8d29-2dd629cf6270"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Teleport"",
                    ""type"": ""Button"",
                    ""id"": ""735aee5f-bb3c-4958-8d97-b7333a62e0ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f1edc3aa-5c07-414c-8d61-e21a0aee9aad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""211292bd-fb60-4740-9fec-3bd624f17ea2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2bf3f99f-2b05-4f0a-ae8d-8acb3a125ae1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0a3d11c1-1f5c-43c3-940c-8fa2cdc85ff4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dca66096-7997-4a84-9033-96c5aa54cc69"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""44d5d9be-f6ef-412c-8bad-5aafc649b1a2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PsyWall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5283910-ed32-4f41-88c4-200091935ddd"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FakeMan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca0f4084-6d43-4ed4-b949-f20dbeadfcc1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Teleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // LandControls
        m_LandControls = asset.FindActionMap("LandControls", throwIfNotFound: true);
        m_LandControls_Move = m_LandControls.FindAction("Move", throwIfNotFound: true);
        m_LandControls_PsyWall = m_LandControls.FindAction("PsyWall", throwIfNotFound: true);
        m_LandControls_FakeMan = m_LandControls.FindAction("FakeMan", throwIfNotFound: true);
        m_LandControls_Teleport = m_LandControls.FindAction("Teleport", throwIfNotFound: true);
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

    // LandControls
    private readonly InputActionMap m_LandControls;
    private ILandControlsActions m_LandControlsActionsCallbackInterface;
    private readonly InputAction m_LandControls_Move;
    private readonly InputAction m_LandControls_PsyWall;
    private readonly InputAction m_LandControls_FakeMan;
    private readonly InputAction m_LandControls_Teleport;
    public struct LandControlsActions
    {
        private @MonsterControls m_Wrapper;
        public LandControlsActions(@MonsterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_LandControls_Move;
        public InputAction @PsyWall => m_Wrapper.m_LandControls_PsyWall;
        public InputAction @FakeMan => m_Wrapper.m_LandControls_FakeMan;
        public InputAction @Teleport => m_Wrapper.m_LandControls_Teleport;
        public InputActionMap Get() { return m_Wrapper.m_LandControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandControlsActions set) { return set.Get(); }
        public void SetCallbacks(ILandControlsActions instance)
        {
            if (m_Wrapper.m_LandControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnMove;
                @PsyWall.started -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnPsyWall;
                @PsyWall.performed -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnPsyWall;
                @PsyWall.canceled -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnPsyWall;
                @FakeMan.started -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnFakeMan;
                @FakeMan.performed -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnFakeMan;
                @FakeMan.canceled -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnFakeMan;
                @Teleport.started -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnTeleport;
                @Teleport.performed -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnTeleport;
                @Teleport.canceled -= m_Wrapper.m_LandControlsActionsCallbackInterface.OnTeleport;
            }
            m_Wrapper.m_LandControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PsyWall.started += instance.OnPsyWall;
                @PsyWall.performed += instance.OnPsyWall;
                @PsyWall.canceled += instance.OnPsyWall;
                @FakeMan.started += instance.OnFakeMan;
                @FakeMan.performed += instance.OnFakeMan;
                @FakeMan.canceled += instance.OnFakeMan;
                @Teleport.started += instance.OnTeleport;
                @Teleport.performed += instance.OnTeleport;
                @Teleport.canceled += instance.OnTeleport;
            }
        }
    }
    public LandControlsActions @LandControls => new LandControlsActions(this);
    public interface ILandControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPsyWall(InputAction.CallbackContext context);
        void OnFakeMan(InputAction.CallbackContext context);
        void OnTeleport(InputAction.CallbackContext context);
    }
}
