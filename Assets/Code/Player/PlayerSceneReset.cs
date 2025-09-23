using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerTriggerTracker))]
public class PlayerSceneReset : MonoBehaviour
{
    private PlayerTriggerTracker triggerTracker;

    private void Awake()
    {
        triggerTracker = GetComponent<PlayerTriggerTracker>();
    }

    private void Update()
    {
        if (triggerTracker.IsTouching("Pits") && !triggerTracker.IsTouching("Platform"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}