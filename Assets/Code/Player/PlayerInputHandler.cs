using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerController movement;
    private PlayerDash dash;
    private PlayerDrag drag;
    private JinnAbilityController jinnAbilityController;
    public InputSystem_Actions Actions => actions; // Public property to access actions

    private void Awake()
    {
        actions = new InputSystem_Actions();
        movement = GetComponent<PlayerController>();
        dash = GetComponent<PlayerDash>();
        drag = GetComponent<PlayerDrag>();
        jinnAbilityController = GetComponent<JinnAbilityController>();
    }
    private void OnEnable()
    {
        actions.Player.Move.Enable();
        actions.Player.Move.performed += ctx => movement.SetDirection(ctx.ReadValue<Vector2>());
        actions.Player.Move.canceled += _ => movement.SetDirection(Vector2.zero);

        actions.Player.Dash.Enable();
        actions.Player.Dash.performed += _ => dash.TryDash(actions.Player.Move.ReadValue<Vector2>());

        actions.Player.Drag.Enable();
        actions.Player.Drag.performed += _ => drag.Drag();
        actions.Player.Drag.canceled += _ => drag.StopDrag();

        actions.Player.UseAbility.Enable();
        actions.Player.UseAbility.performed += ctx => {
            Debug.Log("Z key pressed - UseAbility triggered!");
            jinnAbilityController?.UseJinnAbility();
        };

    }
    private void OnDisable()
    {
        actions.Player.Move.Disable();
        actions.Player.Dash.Disable();
        actions.Player.Drag.Disable();
        actions.Player.UseAbility.Disable();
    }
}