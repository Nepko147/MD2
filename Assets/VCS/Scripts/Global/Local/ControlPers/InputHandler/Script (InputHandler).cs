using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPers_InputHandler : MonoBehaviour
{
    public static ControlPers_InputHandler SingleOnScene { get; private set; }

    InputActions inputActions;

    public Vector2 Screen_Position { get; private set; }

    public bool Screen_Pressed { get; private set; }

    public void ScreenPress(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            Screen_Pressed = true;
        }
        else
        {
            if (_context.canceled)
            {
                Screen_Pressed = false;
                
            }
        }
    }

    private void Awake()
    {
        SingleOnScene = this;
    }

    void Start()
    {
        inputActions = new InputActions();
        inputActions.VirtualStick.Enable();
    }

    void Update()
    {
        Screen_Position = inputActions.VirtualStick.Screen_Position.ReadValue<Vector2>();
    }
}
