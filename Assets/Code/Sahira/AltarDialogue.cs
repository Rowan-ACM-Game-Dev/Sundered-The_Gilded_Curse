using UnityEngine;

[CreateAssetMenu(fileName = "AltarDialogue", menuName = "Dialogue/AltarDialogue")]
public class AltarDialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string sahiraDialogue;
}
