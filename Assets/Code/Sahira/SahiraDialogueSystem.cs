using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SahiraDialogueSystem : MonoBehaviour
{
    public static SahiraDialogueSystem Instance;

    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public ScrollRect scrollRect;

    public float typeSpeed = 0.02f;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerDialogue(string jinnName, string dialogue)
    {
        string fullDialogue = $"Sahira: {dialogue}";

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueCanvas.gameObject.SetActive(true);
        typingCoroutine = StartCoroutine(TypeText(fullDialogue));

        Debug.Log($"[Sahira senses {jinnName}]: {dialogue}");
    }

    private IEnumerator TypeText(string fullText)
    {
        dialogueCanvas.gameObject.SetActive(true);

        dialogueText.text = "";
        yield return null;

        for (int i = 0; i < fullText.Length; i++)
        {
            dialogueText.text += fullText[i];
            yield return new WaitForSeconds(typeSpeed);
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        yield return new WaitForSeconds(5f);
        dialogueCanvas.gameObject.SetActive(false);
    }
}
