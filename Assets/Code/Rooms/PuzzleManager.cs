using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleElement[] puzzleElements;

    public void ResetPuzzle()
    {
        foreach (var element in puzzleElements)
        {
            element.ResetElement();
        }
        Debug.Log("Puzzle Reset");
    }

    public void PuzzleFailed()
    {
        Debug.Log("Puzzle Failed!");
        ResetPuzzle();
    }
}
