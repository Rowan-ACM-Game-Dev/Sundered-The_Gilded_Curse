using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string sceneName;
    public Vector2 respawnPosition;
    public bool weaponCleansed;

    // Key: altarID, Value: cleansed or not
    public Dictionary<string, bool> altarCleansedStates = new Dictionary<string, bool>();
}
