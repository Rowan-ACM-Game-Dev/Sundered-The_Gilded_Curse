using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions actions;
    private PlayerController movement;
    private PlayerDash dash;
    private PlayerDrag drag;
    private JinnAbilityController jinnAbilityController;
    private AltarCleansing currentAltar;
    [SerializeField] private JinnManager jinnManager;

    public InputSystem_Actions Actions => actions; // Public property to access actions

    private void Awake()
    {
        actions = new InputSystem_Actions();

        // Get component references
        movement = GetComponent<PlayerController>();
        dash = GetComponent<PlayerDash>();
        drag = GetComponent<PlayerDrag>();
        jinnAbilityController = GetComponent<JinnAbilityController>();

        if (actions == null)
        {
            Debug.LogError("Actions object is not initialized.");
        }

        if (actions.Player.Move == null || actions.Player.Dash == null)
        {
            Debug.LogError("Some actions in the Player action map are not initialized.");
        }

        if (jinnManager == null)
        {
            jinnManager = GetComponent<JinnManager>();
            if (jinnManager == null)
                Debug.LogWarning("JinnManager is not assigned in Inspector or on this GameObject.");
        }
    }

    private void OnEnable()
    {
        // Movement
        actions.Player.Move.Enable();
        actions.Player.Move.performed += ctx => movement.SetDirection(ctx.ReadValue<Vector2>());
        actions.Player.Move.canceled += _ => movement.SetDirection(Vector2.zero);

        // Dash
        actions.Player.Dash.Enable();
        actions.Player.Dash.performed += ctx =>
        {
            Vector2 dir = actions.Player.Move.ReadValue<Vector2>();
            dash.TryDash(dir);
            movement.SetDashing(true); // Start dash animation
        };

        // Drag
        actions.Player.Drag.Enable();
        actions.Player.Drag.performed += _ => drag.Drag();
        actions.Player.Drag.canceled += _ => drag.StopDrag();

        // Interact
        actions.Player.Interact.Enable();
        actions.Player.Interact.performed += _ =>
        {
            if (currentAltar != null)
                currentAltar.Interact();
            else
                Debug.Log("No altar assigned to currentAltar.");
        };

        // Use Ability
        actions.Player.UseAbility.Enable();
        actions.Player.UseAbility.performed += _ =>
        {
            jinnAbilityController?.UseJinnAbility();
        };

        // Cycle Ability
        if (actions.Player.CycleAbility != null)
        {
            actions.Player.CycleAbility.Enable();
            actions.Player.CycleAbility.performed += _ =>
            {
                if (jinnManager != null)
                    jinnManager.CycleToNextJinn();
                else
                    Debug.LogError("JinnManager is null, cannot cycle abilities.");
            };
        }
    }

    private void OnDisable()
    {
        actions.Player.Move.Disable();
        actions.Player.Dash.Disable();
        actions.Player.Drag.Disable();
        actions.Player.UseAbility.Disable();
        actions.Player.CycleAbility?.Disable();
        actions.Player.Interact.Disable();
    }

    public void SetCurrentAltar(AltarCleansing altar)
    {
        currentAltar = altar;
    }
}
