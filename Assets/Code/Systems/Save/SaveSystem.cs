using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    private const string SaveKey = "AltarSave";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
        Debug.Log("Game saved at altar.");
    }

    public static SaveData Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public static void ClearSave()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
