using UnityEngine;

public class CuttableRope : MonoBehaviour, ISlashable
{
    public Rigidbody2D objectToDrop;

    public void OnSlashed()
    {
        if (objectToDrop != null)
        {
            objectToDrop.bodyType = RigidbodyType2D.Dynamic;
        }
        Debug.Log("Rope destroyed!");
        Destroy(gameObject); // Destroy the rope visually
    }
}
