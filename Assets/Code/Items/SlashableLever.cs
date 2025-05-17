using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlashableLever : MonoBehaviour, ISlashable
{
    public bool isOn = false;
    public GameObject toToggle;
    public GameObject[] targets;

    //void Update()
    //{
    //    if (toToggle != null)
    //    {
    //        toToggle.SetActive(isOpen);
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isOpen = !isOpen;
    //    }        
    //}
    public void OnSlashed()
    {
        Debug.Log("Lever slashed!");
        ToggleTargets();
        isOn = !isOn;
    }

    void ToggleTargets()
    {
        foreach (var obj in targets)
        {
            IActivatable activatable = obj.GetComponent<IActivatable>();
            if (activatable != null)
            {
                activatable.Activate();
            }
            else
            {
                Debug.LogWarning($"{obj.name} does not have IActivatable component.");
            }
        }
    }
}
