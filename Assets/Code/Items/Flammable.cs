using UnityEngine;

public class Flammable : MonoBehaviour
{
    public void Ignite()
    {
        Debug.Log($"{gameObject.name} has been ignited!");
        Destroy(gameObject, 2f); 
    }
}
