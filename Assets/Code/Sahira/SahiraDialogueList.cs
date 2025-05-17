using System.Collections.Generic;
using UnityEngine;

public class SahiraDialogueList : MonoBehaviour
{
    [TextArea(2, 5)]
    public List<string> teleportDialogues = new List<string>()
    {
        "The sands shift, and I return to your shadow",
        "Worry breeds carelessness. I remain where I must — behind you, unseen but unwavering.",
        "Stillness invites ruin. We move — before the past catches us.",
        "Did you think me so easily severed, thief? I am bound — by ash, by oath, by fate.",
        "Drawn back by ruin’s scent... or perhaps by concern. Either way, I’ve returned unharmed."
    };

    public string GetRandomTeleportDialogue()
    {
        if (teleportDialogues == null || teleportDialogues.Count == 0)
            return "No dialogue available.";

        int index = Random.Range(0, teleportDialogues.Count);
        return teleportDialogues[index];
    }
}
