using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerController movement;
    private PlayerDash dash;
    private PlayerDrag drag;
    private JinnAbilityController jinnAbilityController;
    [SerializeField] private JinnManager jinnManager ;

    public InputSystem_Actions Actions => actions; // Public property to access actions


    private void Awake()
    {
        actions = new InputSystem_Actions();
        movement = GetComponent<PlayerController>();
        dash = GetComponent<PlayerDash>();
        drag = GetComponent<PlayerDrag>();
        jinnAbilityController = GetComponent<JinnAbilityController>();

        if (actions == null)
        {
            jinnManager = GetComponent<JinnManager>();
            if (jinnManager == null)
            {
                Debug.LogWarning("JinnManager is not assigned in Inspector and not found on this GameObject.");
            }
        }
        else
        {
            Debug.Log("JinnManager successfully assigned via Inspector.");
        }

        if (actions == null)
        {
            Debug.LogError("Actions object is not initialized.");
        }
        else if (actions.Player.Move == null || actions.Player.Dash == null)
        {
            Debug.LogError("Some actions in the Player action map are not initialized.");
        }
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
        if (actions.Player.CycleAbility != null)
        {
            actions.Player.CycleAbility.Enable();
            actions.Player.CycleAbility.performed += ctx =>
            {
                Debug.Log("X key pressed - Cycling to next ability.");
                if (jinnManager != null)
                {
                    jinnManager.CycleToNextJinn(); // Cycle to next Jinn
                }
                else
                {
                    Debug.LogError("JinnManager is null, cannot cycle abilities.");
                }
            };
        }
        else
        {
            Debug.LogError("CycleAbility action is not properly set up or missing.");
        }
    }
    private void OnDisable()
    {
            actions.Player.Move.Disable();
            actions.Player.Dash.Disable();
            actions.Player.Drag.Disable();
            actions.Player.UseAbility.Disable();
            actions.Player.CycleAbility.Disable();
    }
}