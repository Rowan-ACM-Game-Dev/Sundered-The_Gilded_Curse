using System.Collections;
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
            SaveData savedData = SaveSystem.Load();

            if (savedData != null && savedData.sceneName == SceneManager.GetActiveScene().name)
            {
                StartCoroutine(RespawnAtAltar(savedData.respawnPosition));
            }
            else
            {
                // Default fallback if no save
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private IEnumerator RespawnAtAltar(Vector2 position)
    {
        // Optional: fade out/in here
        yield return null;

        transform.position = position;
        Debug.Log("Respawned at last cleansed altar.");
    }
}
