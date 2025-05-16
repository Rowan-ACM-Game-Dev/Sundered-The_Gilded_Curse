using UnityEngine;

public class BreakableWall : MonoBehaviour, ISlashable
{
    public void OnSlashed()
    {
        Debug.Log("Wall destroyed!");
        Destroy(gameObject);
    }
}
