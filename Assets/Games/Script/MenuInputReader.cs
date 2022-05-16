using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputReader : MonoBehaviour, Controls.IMenuActions
{    
    public System.Action<float> OnMenuMove;
    public System.Action OnSelectEvent;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Menu.SetCallbacks(this);

        controls.Menu.Enable();
    }

    private void OnDestroy()
    {
        controls.Menu.Disable();
    }

    public void SetMenuInput(bool active)
    {
        if (active)
        {
            controls.Player.Disable();
        }
        else
        {
            controls.Player.Enable();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        float val = context.ReadValue<float>();

        if (val != 0)
        {
            //OnMenuMove?.Invoke(val);
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnSelectEvent?.Invoke();
    }
}
