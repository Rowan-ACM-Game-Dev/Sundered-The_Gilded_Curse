using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private float lastDashTime = -Mathf.Infinity;
    private float dashTime = 0f;
    private Vector2 dashDirection;
    private bool isDashing = false;

    private Rigidbody2D rb;
    private Collider2D col;

    public bool IsDashing => isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                EndDash();
            }
        }
    }
    public void TryDash(Vector2 direction)
    {
        if (Time.time >= lastDashTime + dashCooldown && !isDashing && direction != Vector2.zero)
        {
            StartDash(direction);
        }
    }
    private void StartDash(Vector2 direction)
    {
        isDashing = true;
        dashTime = 0f;
        lastDashTime = Time.time;

        dashDirection = direction.normalized;
        rb.linearVelocity = dashDirection * dashSpeed;
        col.enabled = false;
    }
    private void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
        col.enabled = true;
    }
}