using UnityEngine;

public class SahiraFollow : MonoBehaviour
{
    public Transform player;

    public float followSpeed = 5f;
    public float followDistance = 1.5f;

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
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 delta = player.position - lastPlayerPos;
        if (delta.magnitude > 0.01f)
        {
            moveDir = delta.normalized;
        }

        Vector3 offset = -moveDir * followDistance;

        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        lastPlayerPos = player.position;
    }
}
