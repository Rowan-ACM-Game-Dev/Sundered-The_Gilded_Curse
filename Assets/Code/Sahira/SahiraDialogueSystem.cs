using UnityEngine;

public class SahiraDialogueSystem : MonoBehaviour
{
    public static SahiraDialogueSystem Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerDialogue(string jinnName, string dialogue)
    {
        Debug.Log($"[Sahira senses {jinnName}]: {dialogue}");
    }
}
