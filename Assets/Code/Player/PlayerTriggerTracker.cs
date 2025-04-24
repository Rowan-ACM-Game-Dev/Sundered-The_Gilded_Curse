using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTriggerTracker : MonoBehaviour
{
    public List<string> CurrentTriggers { get; private set; } = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentTriggers.Add(collision.tag);
        //Debug.Log("Triggers [ENTER]: " + string.Join(", ", CurrentTriggers));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CurrentTriggers.Contains(collision.tag))
        {
            CurrentTriggers.Remove(collision.tag);
            //Debug.Log("Triggers [EXIT]: " + string.Join(", ", CurrentTriggers));
        }
    }

    public bool IsTouching(string tag)
    {
        return CurrentTriggers.Contains(tag);
    }
}