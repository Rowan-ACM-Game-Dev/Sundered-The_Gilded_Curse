using UnityEngine;

public class Door : MonoBehaviour, IActivatable
{
    private bool isOpen = false;

    public void Activate()
    {
        isOpen = !isOpen;
        gameObject.SetActive(!isOpen); 
        Debug.Log("Door toggled!");
    }
}
