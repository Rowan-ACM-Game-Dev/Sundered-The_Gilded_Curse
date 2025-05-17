using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 Direction => direction;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Vector2 FacingDirection { get; private set; } = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        if (dir != Vector2.zero)
        {
            FacingDirection = dir.normalized;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SaveData savedData = SaveSystem.Load();
        if (savedData != null && savedData.sceneName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            transform.position = savedData.respawnPosition;
            Debug.Log("Player loaded at saved altar position.");
        }
    }

    private void FixedUpdate()
    {
        var dash = GetComponent<PlayerDash>();
        if (dash == null || !dash.IsDashing)
        {
            rb.linearVelocity = direction * speed;
        }
        if (direction.x != 0 || direction.y != 0)
        {
            UpdateSpriteDirection();
        }
    }

    private void UpdateSpriteDirection()
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
            //Debug.Log("Player is facing Right");
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            //Debug.Log("Player is facing Left");
        }
        else if (direction.y > 0)
        {
            spriteRenderer.flipX = false;
            //Debug.Log("Player is facing Up");
        }
        else if (direction.y < 0)
        {
            spriteRenderer.flipX = false;
            //Debug.Log("Player is facing Down");
        }
    }
}
