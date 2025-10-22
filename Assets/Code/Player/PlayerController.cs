using UnityEngine;

/// <summary>
/// Handles player movement, physics, and animation.
/// Works with PlayerInputHandler and PlayerDash.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // State
    private Vector2 direction;
    private Vector2 lastFacingDir = Vector2.down;
    private bool isMoving;
    private bool isDashing;

    // Animator hashes
    private static readonly int AnimMoveX = Animator.StringToHash("moveX");
    private static readonly int AnimMoveY = Animator.StringToHash("moveY");
    private static readonly int AnimIsMoving = Animator.StringToHash("isMoving");
    private static readonly int AnimIsDashing = Animator.StringToHash("isDashing");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimator();
        HandleSpriteFlip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    /// <summary>
    /// Called by PlayerInputHandler each frame.
    /// </summary>
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        isMoving = direction.sqrMagnitude > 0.01f;

        if (isMoving)
            lastFacingDir = direction;
    }

    /// <summary>
    /// Called by PlayerDash when starting/stopping a dash.
    /// </summary>
    public void SetDashing(bool dashing)
    {
        isDashing = dashing;
    }

    private void UpdateAnimator()
    {
        animator.SetBool(AnimIsMoving, isMoving);
        animator.SetBool(AnimIsDashing, isDashing);
        animator.SetFloat(AnimMoveX, lastFacingDir.x);
        animator.SetFloat(AnimMoveY, lastFacingDir.y);
    }

    private void HandleSpriteFlip()
    {
        if (Mathf.Abs(lastFacingDir.x) > 0.1f)
            spriteRenderer.flipX = lastFacingDir.x > 0;
    }
}
