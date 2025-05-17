using UnityEngine;
using UnityEngine.SceneManagement;

public class AltarCleansing : MonoBehaviour
{
    [Tooltip("Unique ID for this altar")]
    public string altarID;

    public bool isCleansed = false;

    [Tooltip("Drag the AltarDialogue asset here.")]
    public AltarDialogue altarDialogue;

    [Tooltip("Optional dialogue asset to show when the altar is already cleansed.")]
    public AltarDialogue alreadyCleansedDialogue;

    private void Start()
    {
        //SaveData loadedData = SaveSystem.Load();
        //if (loadedData != null)
        //{
        //    if (loadedData.weaponCleansed)
        //    {
        //        isCleansed = true;
        //        Debug.Log("Altar state loaded: Already cleansed.");
        //    }
        //}

        LoadAltarState();
    }

    public void Interact()
    {
        if (!isCleansed)
        {
            Cleanse();
        }
        else
        {
            ShowAlreadyCleansedDialogue();
            Debug.Log("Altar us already cleansed");
        }
    }

    private void Cleanse()
    {
        isCleansed = true;
        Debug.Log($"Altar {altarID} has been cleansed! Sword/Dagger purified.");

        // Show Sahira's dialogue for this altar
        if (altarDialogue != null)
        {
            SahiraDialogueSystem.Instance.TriggerDialogue("Altar", altarDialogue.sahiraDialogue);
        }
        else
        {
            Debug.LogWarning("No AltarDialogue asset assigned!");
        }

        SaveAltarState();
    }

    private void ShowAlreadyCleansedDialogue()
    {
        Debug.Log("Altar is already cleansed");

        if (alreadyCleansedDialogue != null)
        {
            SahiraDialogueSystem.Instance.TriggerDialogue("Altar", alreadyCleansedDialogue.sahiraDialogue);
        }
        else
        {
            SahiraDialogueSystem.Instance.TriggerDialogue("Altar", "This altar has already purified your weapon.");
        }
    }

    private void LoadAltarState()
    {
        SaveData data = SaveSystem.Load();

        if (data != null && data.altarCleansedStates != null)
        {
            if (data.altarCleansedStates.TryGetValue(altarID, out bool cleansed))
            {
                isCleansed = cleansed;
                Debug.Log($"Loaded altar {altarID} state: {isCleansed}");
            }
            else
            {
                isCleansed = false;
                Debug.Log($"No saved state for altar {altarID}. Defaulting to false.");
            }
        }
        else
        {
            isCleansed = false;
            Debug.Log("No save data found. Defaulting altar to not cleansed.");
        }
    }

    private void SaveAltarState()
    {
        SaveData data = SaveSystem.Load() ?? new SaveData();

        if (data.altarCleansedStates == null)
        {
            data.altarCleansedStates = new System.Collections.Generic.Dictionary<string, bool>();
        }

        data.altarCleansedStates[altarID] = isCleansed;

        data.sceneName = SceneManager.GetActiveScene().name;
        Vector3 savePosition = transform.position;
        data.respawnPosition = new Vector2(savePosition.x, savePosition.y);
        data.weaponCleansed = isCleansed; 

        SaveSystem.Save(data);
        Debug.Log($"Saved altar {altarID} state: {isCleansed}");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInputHandler playerInput = other.GetComponent<PlayerInputHandler>();
        if (playerInput != null)
        {
            playerInput.SetCurrentAltar(this);
            Debug.Log("Player entered altar area.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInputHandler playerInput = collision.GetComponent<PlayerInputHandler>();
        if (playerInput != null)
        {
            playerInput.SetCurrentAltar(null);
            Debug.Log("Player left alter area.");
        }
    }
}