using UnityEngine;
using UnityEngine.InputSystem;
using InputActions;

public class ControlPers_InputHandler : MonoBehaviour
{
    public static ControlPers_InputHandler SingleOnScene { get; private set; }

    InputHandler inputActions_inputHandler;

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
        inputActions_inputHandler = new InputHandler();
        inputActions_inputHandler.VirtualStick.Enable();
    }

    void Update()
    {
        Screen_Position = inputActions_inputHandler.VirtualStick.Screen_Position.ReadValue<Vector2>();
    }
}
