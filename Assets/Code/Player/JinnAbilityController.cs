using UnityEngine;
using static JinnManager;

public class JinnAbilityController : MonoBehaviour
{
    private float abilityCooldown = 3f;
    private float lastAbilityTime = -Mathf.Infinity;

    public void UseJinnAbility()
    {
        Jinn currentJinn = JinnManager.Instance.ActiveJinn;

        if (currentJinn == null)
        {
            Debug.LogWarning("No active Jinn selected!");
            return;
        }

        if (currentJinn.jinnAbility != null)
        {
            if (currentJinn.jinnAbility.IsReady())
            {
                currentJinn.jinnAbility.Use();
                Debug.Log($"Using ability: {currentJinn.jinnAbility.abilityName}");
                ActivateAbilityEffect(currentJinn);
            }
            else
            {
                Debug.LogWarning($"Ability {currentJinn.jinnAbility.abilityName} is on cooldown.");
            }
        }
        else
        {
            Debug.LogWarning("Selected Jinn has no ability to use.");
        }
    }

    private void ActivateAbilityEffect(Jinn jinn)
    {
        Debug.Log($"Jinn PowerType: {jinn.powerType}");

        switch (jinn.powerType)
        {
            case Jinn.PowerType.Light:
                Debug.Log("Light: Reveal secrets.");
                // TODO: Implement light reveal logic (e.g., reveal hidden objects or areas)
                break;
            case Jinn.PowerType.Wind:
                Debug.Log("Wind: Gust activated.");
                // Implement Wind Gust logic (e.g., push objects or enemies)
                break;
            case Jinn.PowerType.Fire:
                Debug.Log("Fire: Burn obstacles.");
                // Implement Fire effect logic (e.g., burn objects)
                break;
            case Jinn.PowerType.Void:
                Debug.Log("Void: Time slowed.");
                Time.timeScale = 0.5f; // Slowing down time
                Invoke(nameof(ResetTimeScale), 2f); // Reset time after 2 seconds
                break;
            default:
                Debug.LogWarning("Unknown power type.");
                break;
        }
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1f; // Reset time to normal speed
    }
}
