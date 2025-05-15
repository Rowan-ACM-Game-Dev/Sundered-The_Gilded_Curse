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
            Debug.LogWarning("Multiple instances of JinnManager detected.");
            //Destroy(gameObject);
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
        public int upgradeLevel = 0;

        public Ability jinnAbility;
    }

    public class Ability
    {
        public string abilityName;
        public float cooldown = 5f;
        [HideInInspector] public float lastUsedTime = -Mathf.Infinity;

        public void Use()
        {
            lastUsedTime = Time.time;
        }

        public bool IsReady()
        {
            return Time.time >= lastUsedTime + cooldown;
        }
    }

    private Dictionary<string, Jinn> collectedJinns = new Dictionary<string, Jinn>();
    public IReadOnlyDictionary<string, Jinn> CollectedJinns => collectedJinns;

    private Jinn activeJinn; // Store the active Jinn
    public Jinn ActiveJinn => activeJinn; // Public getter for active Jinn

    private List<Jinn> jinnList = new List<Jinn>(); // A list to store jinns in order for cycling
    private int currentIndex = 0; // Track the current active Jinn

    public void CollectJinn(Jinn newJinn)
    {
        if (newJinn == null)
        {
            Debug.LogWarning("Attempted to collect a null Jinn.");
            return;
        }

        if (collectedJinns.ContainsKey(newJinn.jinnName))
        {
            Debug.LogWarning($"Jinn '{newJinn.jinnName}' has already been collected.");
            return;
        }

        collectedJinns.Add(newJinn.jinnName, newJinn);
        jinnList.Add(newJinn); // Add new Jinn to the cycling list
        newJinn.hasBeenEncountered = true;

        AssignAbilityBasedOnJinn(newJinn);

        if (newJinn.jinnAbility != null)
        {
            Debug.Log($"Jinn '{newJinn.jinnName}' collected with ability: {newJinn.jinnAbility.abilityName}");
        }
        else
        {
            Debug.LogWarning($"Jinn '{newJinn.jinnName}' collected, but has no ability assigned.");
        }

        // Auto set active Jinn if only one is collected
        if (collectedJinns.Count == 1)
        {
            SetActiveJinn(newJinn.jinnName);
        }
    }

    private void AssignAbilityBasedOnJinn(Jinn jinn)
    {
        switch (jinn.jinnName)
        {
            case "Noor":
                jinn.jinnAbility = new Ability { abilityName = "Light Reveal", cooldown = 5f };
                break;
            case "Riih":
                jinn.jinnAbility = new Ability { abilityName = "Wind Gust", cooldown = 5f };
                break;
            case "Naar":
                jinn.jinnAbility = new Ability { abilityName = "Fire Blast", cooldown = 5f };
                break;
            case "Nasiim":
                jinn.jinnAbility = new Ability { abilityName = "Void Time", cooldown = 5f };
                break;
            default:
                Debug.LogWarning($"No ability mapped for Jinn: {jinn.jinnName}");
                break;
        }
    }

    public void SetActiveJinn(string jinnName)
    {
        if (collectedJinns.ContainsKey(jinnName))
        {
            activeJinn = collectedJinns[jinnName];
            currentIndex = jinnList.IndexOf(activeJinn); // Update current index
            Debug.Log($"Active Jinn set to: {activeJinn.jinnName}");
        }
        else
        {
            Debug.LogWarning($"Jinn '{jinnName}' not found in the collection.");
        }
    }

    public void CycleToNextJinn()
    {
        if (jinnList == null || jinnList.Count == 0)
        {
            Debug.LogWarning("No jinns collected to cycle through.");
            return;
        }

        currentIndex = (currentIndex + 1) % jinnList.Count; // Move to the next Jinn in the list
        SetActiveJinn(jinnList[currentIndex].jinnName);

        Debug.Log($"Switched to Jinn: {ActiveJinn.jinnName}");
    }

    public bool HasCollected(string jinnName)
    {
        return collectedJinns.ContainsKey(jinnName);
    }

    public void ClearInventory() => collectedJinns.Clear(); // For resets, debugging
}
