using UnityEngine;

public class JinnRoom : MonoBehaviour
{
    private bool hasSpoken = false;

    public string jinnName; 
    [TextArea] public string sahiraDialogue;

    public JinnManager.Jinn jinnData;

    private void Start()
    {
        if (jinnData != null)
        {
            JinnManager.Instance?.AddJinn(jinnData);
        }
        else
        {
            Debug.LogWarning($"JinnRoom '{gameObject.name}' is missing jinnData!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpoken && other.CompareTag("Player") && jinnData != null && !jinnData.hasBeenEncountered)
        {
            SahiraDialogueSystem.Instance.TriggerDialogue(jinnData.dialogue.jinnName, jinnData.dialogue.sensedDialogue);
            jinnData.hasBeenEncountered = true;
            hasSpoken = true;
        }
    }
}
