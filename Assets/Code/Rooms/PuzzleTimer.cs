using UnityEngine;

public class PuzzleTimer : MonoBehaviour
{
    private float timeRemaining;

    public void ResetTimer()
    {
        timeRemaining = 10f; 
        Debug.Log("Puzzle timer reset!");
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }
}
