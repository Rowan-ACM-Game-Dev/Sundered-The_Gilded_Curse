using UnityEngine;

public class AltarCleansing : MonoBehaviour
{
    public bool isCleansed = false;

    public void Interact()
    {
        if (!isCleansed)
        {
            Cleanse();
        }
        else
        {
            Debug.Log("Altar us already cleansed");
        }
    }

    private void Cleanse()
    {
        isCleansed = true;
        Debug.Log("The altar has been cleansed! Sword/Dagger purisfied.");
        //TODO: Add save syste ad weapon cleansing logic here later
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