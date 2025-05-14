using UnityEngine;

public class DarkArea : MonoBehaviour
{
    public void Reveal()
    {
        Debug.Log($"{gameObject.name} has been revealed!");
        gameObject.SetActive(true); 
    }
}
