using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerController movement;
    private PlayerDash dash;
    private PlayerDrag drag;
    private void Awake()
    {
        actions = new InputSystem_Actions();
        movement = GetComponent<PlayerController>();
        dash = GetComponent<PlayerDash>();
        drag = GetComponent<PlayerDrag>();
    }

    private void OnEnable()
    {
        if (actions == null)
        {
            actions = new InputSystem_Actions();
        }

        actions.Player.Move.Enable();
        actions.Player.Move.performed += ctx => movement.SetDirection(ctx.ReadValue<Vector2>());
        actions.Player.Move.canceled += _ => movement.SetDirection(Vector2.zero);

        actions.Player.Dash.Enable();
        actions.Player.Dash.performed += _ => dash.TryDash(actions.Player.Move.ReadValue<Vector2>());

        actions.Player.Drag.Enable();
        actions.Player.Drag.performed += _ => drag.Drag();
        actions.Player.Drag.canceled += _ => drag.StopDrag();
    }
    private void OnDisable()
    {
        actions.Player.Move.Disable();
        actions.Player.Dash.Disable();
        actions.Player.Drag.Disable();
    }
}