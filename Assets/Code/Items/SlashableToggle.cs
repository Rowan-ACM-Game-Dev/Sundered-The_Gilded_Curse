using UnityEngine;

public class SlashableToggle : MonoBehaviour, ISlashable
{
    public GameObject target; 
    private bool isOn = false;

    public void OnSlashed()
    {
        Debug.Log("Slash detected on " + gameObject.name);

        if (target != null)
        {
            IActivatable activatable = target.GetComponent<IActivatable>();
            if (activatable != null)
            {
                isOn = !isOn;        
                activatable.Activate();

                Debug.Log("Slash toggled activatable: " + target.name + " | New State: " + isOn);
            }
            else
            {
                Debug.LogWarning(target.name + " does not implement IActivatable.");
            }
        }
        else
        {
            Debug.LogWarning("No target assigned to SlashableToggle.");
        }
    }
}
