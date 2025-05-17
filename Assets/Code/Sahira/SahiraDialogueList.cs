using System.Collections.Generic;
using UnityEngine;

public class SahiraDialogueList : MonoBehaviour
{
    [TextArea(2, 5)]
    public List<string> teleportDialogues = new List<string>()
    {
        "I'm here!",
        "Don't worry, I'm right behind you.",
        "Let's keep moving!",
        "You can't get rid of me that easily!",
        "Back to you, safely."
    };

    public string GetRandomTeleportDialogue()
    {
        if (teleportDialogues == null || teleportDialogues.Count == 0)
            return "No dialogue available.";

        int index = Random.Range(0, teleportDialogues.Count);
        return teleportDialogues[index];
    }
}
