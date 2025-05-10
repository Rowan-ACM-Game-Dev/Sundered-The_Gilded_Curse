using UnityEngine;
using System.Collections.Generic;

public class JinnManager : MonoBehaviour
{
    public static JinnManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [System.Serializable]
    public class Jinn
    {
        public string jinnName;
        public Color jinnColor;
        public Sprite icon;
        public enum PowerType { Light, Wind, Fire, Void }
        public PowerType powerType;

        public JinnDialogue dialogue;

        [HideInInspector]
        public bool hasBeenEncountered = false; // prevents repeated dialogue
    }

    public List<Jinn> jinns;

    public void AddJinn(Jinn newJinn)
    {
        jinns.Add(newJinn);
        Debug.Log("Jinn added: " + newJinn.jinnName);
    }

    public void CheckForJinnsNearby(JinnRoom jinnRoom)
    {
        if (jinnRoom == null || jinnRoom.jinnData == null) return;

        foreach (var jinn in jinns)
        {
            if (jinn == jinnRoom.jinnData)
            {
                TriggerSahiraDialogue(jinn);
                break;
            }
        }
    }

    private void TriggerSahiraDialogue(Jinn jinn)
    {
        Debug.Log("Sahira speaks: Encountered " + jinn.jinnName);
    }
}