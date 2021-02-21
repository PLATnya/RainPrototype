// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControll.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControll : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControll()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControll"",
    ""maps"": [
        {
            ""name"": ""MovementAxis"",
            ""id"": ""8fd24706-e8bc-4c26-bb98-7896427056e0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""d350f533-3f2b-4fe3-8e82-1c7939c36e6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6c80c453-84f1-4378-ae4a-57cee6c64e3d"",
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
                    ""id"": ""9b24e44b-a440-404f-9309-c3167a00197f"",
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
                    ""id"": ""9185fc84-c40b-4943-ac8b-68a78e6bc711"",
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
                    ""id"": ""5af06ac4-f065-4d42-9eac-8fe7c35be7d1"",
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
                    ""id"": ""c84648f1-9c96-4c96-a6d2-dce3f6be036b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""MouseAxis"",
            ""id"": ""fca4fffd-9697-4b8e-8f35-c43f476dc888"",
            ""actions"": [
                {
                    ""name"": ""MouseDeltaX"",
                    ""type"": ""Value"",
                    ""id"": ""5f5c2e55-14f6-4808-be7b-d4a0b3e704bc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDeltaY"",
                    ""type"": ""Value"",
                    ""id"": ""4bb3dadd-9f3f-4725-886f-8ef2f2137e6a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2a116902-b279-44f5-b16e-ba145d31b646"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.02)"",
                    ""groups"": """",
                    ""action"": ""MouseDeltaX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bacaa315-c718-4d81-a06f-b4b95ef29f88"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.02)"",
                    ""groups"": """",
                    ""action"": ""MouseDeltaY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MovementAxis
        m_MovementAxis = asset.FindActionMap("MovementAxis", throwIfNotFound: true);
        m_MovementAxis_Move = m_MovementAxis.FindAction("Move", throwIfNotFound: true);
        // MouseAxis
        m_MouseAxis = asset.FindActionMap("MouseAxis", throwIfNotFound: true);
        m_MouseAxis_MouseDeltaX = m_MouseAxis.FindAction("MouseDeltaX", throwIfNotFound: true);
        m_MouseAxis_MouseDeltaY = m_MouseAxis.FindAction("MouseDeltaY", throwIfNotFound: true);
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

    // MovementAxis
    private readonly InputActionMap m_MovementAxis;
    private IMovementAxisActions m_MovementAxisActionsCallbackInterface;
    private readonly InputAction m_MovementAxis_Move;
    public struct MovementAxisActions
    {
        private @PlayerControll m_Wrapper;
        public MovementAxisActions(@PlayerControll wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MovementAxis_Move;
        public InputActionMap Get() { return m_Wrapper.m_MovementAxis; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementAxisActions set) { return set.Get(); }
        public void SetCallbacks(IMovementAxisActions instance)
        {
            if (m_Wrapper.m_MovementAxisActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementAxisActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementAxisActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementAxisActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_MovementAxisActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public MovementAxisActions @MovementAxis => new MovementAxisActions(this);

    // MouseAxis
    private readonly InputActionMap m_MouseAxis;
    private IMouseAxisActions m_MouseAxisActionsCallbackInterface;
    private readonly InputAction m_MouseAxis_MouseDeltaX;
    private readonly InputAction m_MouseAxis_MouseDeltaY;
    public struct MouseAxisActions
    {
        private @PlayerControll m_Wrapper;
        public MouseAxisActions(@PlayerControll wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseDeltaX => m_Wrapper.m_MouseAxis_MouseDeltaX;
        public InputAction @MouseDeltaY => m_Wrapper.m_MouseAxis_MouseDeltaY;
        public InputActionMap Get() { return m_Wrapper.m_MouseAxis; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseAxisActions set) { return set.Get(); }
        public void SetCallbacks(IMouseAxisActions instance)
        {
            if (m_Wrapper.m_MouseAxisActionsCallbackInterface != null)
            {
                @MouseDeltaX.started -= m_Wrapper.m_MouseAxisActionsCallbackInterface.OnMouseDeltaX;
                @MouseDeltaX.performed -= m_Wrapper.m_MouseAxisActionsCallbackInterface.OnMouseDeltaX;
                @MouseDeltaX.canceled -= m_Wrapper.m_MouseAxisActionsCallbackInterface.OnMouseDeltaX;
                @MouseDeltaY.started -= m_Wrapper.m_MouseAxisActionsCallbackInterface.OnMouseDeltaY;
                @MouseDeltaY.performed -= m_Wrapper.m_MouseAxisActionsCallbackInterface.OnMouseDeltaY;
                @MouseDeltaY.canceled -= m_Wrapper.m_MouseAxisActionsCallbackInterface.OnMouseDeltaY;
            }
            m_Wrapper.m_MouseAxisActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseDeltaX.started += instance.OnMouseDeltaX;
                @MouseDeltaX.performed += instance.OnMouseDeltaX;
                @MouseDeltaX.canceled += instance.OnMouseDeltaX;
                @MouseDeltaY.started += instance.OnMouseDeltaY;
                @MouseDeltaY.performed += instance.OnMouseDeltaY;
                @MouseDeltaY.canceled += instance.OnMouseDeltaY;
            }
        }
    }
    public MouseAxisActions @MouseAxis => new MouseAxisActions(this);
    public interface IMovementAxisActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IMouseAxisActions
    {
        void OnMouseDeltaX(InputAction.CallbackContext context);
        void OnMouseDeltaY(InputAction.CallbackContext context);
    }
}
