using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController pScript;

    // Update is called once per frame
    void Update()
    {
        if (pScript && pScript.health <= 0)
        {
            Destroy(pScript.gameObject);
            Debug.Log("Game Over");
        }
        
    }
}
