using UnityEngine;

[CreateAssetMenu(fileName = "JinnDialogue", menuName = "Jinn/Dialogues", order = 1)]
public class JinnDialogue : ScriptableObject
{
    public string jinnName;
    [TextArea] public string sensedDialogue;
    [TextArea] public string freedDialogue;
}
