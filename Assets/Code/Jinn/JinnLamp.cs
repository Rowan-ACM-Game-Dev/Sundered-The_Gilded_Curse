using UnityEngine;
using UnityEngine.InputSystem;

public class JinnLamp : MonoBehaviour
{
    public JinnManager.Jinn jinnData;

    private bool isBroken = false;
    private bool isPlayerNearby = false;
    private InputAction interactAction;

    [SerializeField]
    private GameObject jinn;

    private void Awake()
    {
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.performed += ctx => AttemptBreakLamp();
        interactAction.Enable();
    }

    private void OnDestroy()
    {
        interactAction.Disable();
    }

    private void AttemptBreakLamp()
    {
        if (isPlayerNearby && !isBroken)
        {
            BreakLamp();
        }
    }

    private void BreakLamp()
    {
        isBroken = true;
        Debug.Log("Lamp broken for: " + jinnData.jinnName);

        SahiraDialogueSystem.Instance.TriggerDialogue(jinnData.dialogue.jinnName, jinnData.dialogue.freedDialogue);

        if (!jinnData.hasBeenEncountered)
        {
            jinnData.hasBeenEncountered = true;
            JinnManager.Instance?.CollectJinn(jinnData);
        }

        Destroy(gameObject, 0.5f);

        if (jinn != null)
            jinn.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isBroken)
        {
            Debug.Log("Player Touched the Lamp.");
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Left the Lamp.");
            isPlayerNearby = false;
        }
    }
}
