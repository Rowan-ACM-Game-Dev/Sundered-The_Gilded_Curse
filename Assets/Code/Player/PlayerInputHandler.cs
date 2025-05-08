using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerController movement;
    private PlayerDash dash;
    private void Awake()
    {
        actions = new InputSystem_Actions();
        movement = GetComponent<PlayerController>();
        dash = GetComponent<PlayerDash>();

        if (movement == null)
        {
            Debug.LogError("PlayerController is not attached to this GameObject.");
        }

        if (dash == null)
        {
            Debug.LogError("PlayerDash is not attached to this GameObject.");
        }
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
    }
    private void OnDisable()
    {
        if (actions != null)
        {
            actions.Player.Move.Disable();
            actions.Player.Dash.Disable();
        }
    }
}