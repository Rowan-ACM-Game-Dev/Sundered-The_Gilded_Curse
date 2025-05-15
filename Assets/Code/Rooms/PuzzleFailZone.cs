using UnityEngine;

public class PuzzleFailZone : MonoBehaviour
{
    public PuzzleManager manager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Pits"))
            {
                Debug.Log("Player entered the fail zone!");

                if (manager != null)
                {
                    Debug.Log("Calling PuzzleFailed()");
                    manager.PuzzleFailed();
                }
                else
                {
                    Debug.Log("PuzzleManager reference is not assigned!");
                }
            }
        }
    }
}