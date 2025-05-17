using UnityEngine;

public class ClearSave : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveSystem.ClearSave();
            Debug.Log("Save data cleared!");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Save path: " + Application.persistentDataPath + "/save.dat");
        }
    }
}
