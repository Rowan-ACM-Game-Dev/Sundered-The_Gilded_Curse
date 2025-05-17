using System.Collections.Generic;
using UnityEngine;

public class SahiraFollow : MonoBehaviour
{
    public Transform player;

    public SahiraDialogueSystem sahiraDialogueSystem;
    public SahiraDialogueList sahiraDialogueList;

    public float followSpeed = 5f;
    public float followDistance = 1.5f;

    public float maxDistanceBeforeTeleport = 10f;

    public float teleportCooldown = 3f;
    private float teleportTimer = 0f;

    public LayerMask wallLayerMask;
    public float wallCheckRadius = 0.2f;

    private Vector3 lastPlayerPos;
    private Vector3 moveDir = Vector3.down;

    private void Start()
    {
        if (player == null)
        {
            Debug.Log("Sahira: Player not assigned!");
            enabled = false;
            return;
        }
        lastPlayerPos = player.position;

        if (sahiraDialogueSystem == null)
            Debug.LogWarning("SahiraDialogueSystem not assigned in SahiraFollow!");
        if (sahiraDialogueList == null)
            Debug.LogWarning("SahiraDialogueList not assigned in SahiraFollow!");
    }

    void LateUpdate()
    {
        if (player == null) return;

        teleportTimer -= Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > maxDistanceBeforeTeleport && teleportTimer <= 0f)
        {
            TeleportToPlayer();
            TriggerRandomTeleportDialogue();
            teleportTimer = teleportCooldown; // reset cooldown
            return;
        }

        Vector3 delta = player.position - lastPlayerPos;
        if (delta.magnitude > 0.01f)
        {
            moveDir = delta.normalized;
        }

        Vector3 offset = -moveDir * followDistance;
        Vector3 targetPos = player.position + offset;

        bool wallHit = Physics2D.OverlapCircle(targetPos, wallCheckRadius, wallLayerMask);

        if (!wallHit)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }

        lastPlayerPos = player.position;
    }

    private void TeleportToPlayer()
    {
        Vector3 offset = -moveDir * followDistance;
        Vector3 teleportPos = player.position + offset;

        bool wallHit = Physics2D.OverlapCircle(teleportPos, wallCheckRadius, wallLayerMask);

        if (wallHit)
        {
            teleportPos = player.position;
        }

        transform.position = teleportPos;
        Debug.Log("Sahira teleported to player to avoid soft lock.");
    }

    private void TriggerRandomTeleportDialogue()
    {
        if (sahiraDialogueSystem != null && sahiraDialogueList != null)
        {
            string dialogue = sahiraDialogueList.GetRandomTeleportDialogue();
            sahiraDialogueSystem.TriggerDialogue("Sahira", dialogue);
        }
        else
        {
            Debug.LogWarning("Dialogue system or library instance missing!");
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Vector3 offset = -moveDir * followDistance;
            Vector3 targetPos = player.position + offset;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetPos, wallCheckRadius);
        }
    }
}