using UnityEngine;

public class ClearSave : MonoBehaviour
{
    void Update()
    {
        // Press P to clear saved data
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveSystem.ClearSave();
            Debug.Log("Save data cleared!");
        }

        // Optional: Press O to show current save path
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Save path: " + Application.persistentDataPath + "/save.dat");
        }
    }
}
