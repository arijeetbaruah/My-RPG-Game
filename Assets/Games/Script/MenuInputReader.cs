using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputReader : MonoBehaviour, Controls.IMenuActions
{
    public System.Action OnInventoryToggle;

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

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        OnInventoryToggle?.Invoke();
    }
}
