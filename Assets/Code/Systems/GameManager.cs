using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats pStats;

    void Update()
    {
        if (pStats && pStats.health <= 0)
        {
            HandleGameOver();
        }
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over");

        // Disable player control instead of destroying the GameObject
        var controller = pStats.GetComponent<PlayerController>();
        var inputHandler = pStats.GetComponent<PlayerInputHandler>(); // If used
        var dash = pStats.GetComponent<PlayerDash>();

        if (controller) controller.enabled = false;
        if (dash) dash.enabled = false;
        if (inputHandler) inputHandler.enabled = false;

        // Optional: play death animation or fade effect here
        // Optional: disable collider or Rigidbody
    }
}
