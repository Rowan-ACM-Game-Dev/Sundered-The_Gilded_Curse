using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Tooltip("Assign the GameObject that has the ActivatePlatforms script on it.")]
    public GameObject targetToActivate;

    private IActivatable activatable;

    private void Start()
    {
        if (targetToActivate != null)
        {
            activatable = targetToActivate.GetComponent<IActivatable>();
            if (activatable == null)
            {
                Debug.LogWarning($"{targetToActivate.name} does not implement IActivatable!");
            }
        }
        else
        {
            Debug.LogWarning("No target assigned to PressurePlate.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activatable != null)
        {
            Debug.Log("Pressure Plate triggered!");
            activatable.Activate();
        }
    }
}
