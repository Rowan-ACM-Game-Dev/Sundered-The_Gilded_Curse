using System.Collections;
using UnityEngine;
using static JinnManager;

public class JinnAbilityController : MonoBehaviour
{
    private float abilityCooldown = 3f;
    private float lastAbilityTime = -Mathf.Infinity;

    public void UseJinnAbility()
    {
        if(Time.time - lastAbilityTime < abilityCooldown)
        {
            Debug.LogWarning("Global cooldown in effect. Wait a bt");
            return;
        }

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
                lastAbilityTime = Time.time;
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
        Transform playerTransform = transform;
        Vector3 forward = playerTransform.forward;
        Vector3 origin = playerTransform.position + forward;
        float range = 5f;

        Debug.Log($"Jinn PowerType: {jinn.powerType}");

        switch (jinn.powerType)
        {
            case Jinn.PowerType.Light:
                Debug.Log("Light: Reveal secrets.");
                RevealInDarkness(origin, range);
                break;
            case Jinn.PowerType.Wind:
                Debug.Log("Wind: Gust activated.");
                ApplyWindForce(origin, forward, range);
                break;
            case Jinn.PowerType.Fire:
                Debug.Log("Fire: Burn obstacles.");
                IgniteFlammableObject(origin, range);
                break;
            case Jinn.PowerType.Void:
                Debug.Log("Void: Time slowed.");
                StartCoroutine(RewindTimeCoroutine());
                break;
            default:
                Debug.LogWarning("Unknown power type.");
                break;
        }
    }

    private void RevealInDarkness(Vector3 origin, float radius)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius);

        foreach (Collider2D hit in hits)
        {
            if (hit != null && hit.CompareTag("DarkArea"))
            {
                hit.GetComponent<DarkArea>()?.Reveal();
            }
        }
    }

    private void ApplyWindForce(Vector3 origin, Vector3 direction, float range)
    {
        float pushForce = 500f;
        Vector2 windDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, windDirection, range);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit != null && hit.collider != null && hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(windDirection * pushForce);
            }
        }
    }

    private void IgniteFlammableObject(Vector3 origin, float range)
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range);
        if (hit != null && hit.collider != null)
        {
            hit.collider.GetComponent<Flammable>()?.Ignite();
        }
    }

    private IEnumerator RewindTimeCoroutine()
    {
        PuzzleTimer timer = FindFirstObjectByType<PuzzleTimer>();
        if (timer != null)
        {
            timer.ResetTimer();
            Debug.Log("Timer reset by Nasiim");
        }

        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
    }
}
