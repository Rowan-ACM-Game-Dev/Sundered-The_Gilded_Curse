using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerTracker : MonoBehaviour
{
    public List<string> CurrentTriggers { get; private set; } = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CurrentTriggers.Contains(collision.tag))
        {
            CurrentTriggers.Add(collision.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CurrentTriggers.Contains(collision.tag))
        {
            CurrentTriggers.Remove(collision.tag);
        }
    }

    public bool IsTouching(string tag)
    {
        return CurrentTriggers.Contains(tag);
    }
}