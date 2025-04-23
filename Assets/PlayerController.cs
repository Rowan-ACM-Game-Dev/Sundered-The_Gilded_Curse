using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Stats
    public int health;
    public float speed;
    public Vector2 direc;

    // Input
    public InputSystem_Actions actions;
    public InputAction move;

    // Components
    public Rigidbody2D rb;

    // Camera
    public Camera m_MainCamera;
    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;
    public Vector3 targetCameraPosition;
    public Vector3 targetCameraScale;

    // Dash variables
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTime = 0f;
    private float lastDashTime = -Mathf.Infinity;
    private Vector2 dashDirection;

    // List of Colliders currently inside player
    public List<string> TriggerList;

    private void Start()
    {
        m_MainCamera = Camera.main;
        targetCameraPosition = m_MainCamera.transform.position;
        TriggerList = new List<string>();
    }

    private void Awake()
    {
        actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = actions.Player.Move;
        move.Enable();
        move.performed += i => direc = move.ReadValue<Vector2>();
        move.canceled += i => direc = Vector2.zero;

        // DASH INPUT SETUP
        actions.Player.Dash.Enable();
        actions.Player.Dash.performed += ctx => TryDash();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        if (TriggerList.Contains("Pits") && !TriggerList.Contains("Platform"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                EndDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = direc * speed;
        }

        Vector3 mousePosition = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - rb.transform.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 180);
        rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, targetRotation, 5F * Time.deltaTime);
    }

    private void LateUpdate()
    {
        m_MainCamera.transform.position = Vector3.SmoothDamp(m_MainCamera.transform.position, targetCameraPosition, ref velocity, smoothTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerList.Add(collision.tag);

        if (collision.gameObject.CompareTag("MainCamera"))
        {
            targetCameraPosition = new Vector3(collision.transform.position.x, collision.transform.position.y, m_MainCamera.transform.position.z); //collision.transform.position
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerList.Remove(collision.tag);
    }

    private void TryDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && !isDashing && direc != Vector2.zero)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = 0f;
        lastDashTime = Time.time;

        dashDirection = direc.normalized;
        rb.linearVelocity = dashDirection * dashSpeed;

        // Make player invulnerable or disable collisions
        GetComponent<Collider2D>().enabled = false;
    }

    private void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;

        // Re-enable collider
        GetComponent<Collider2D>().enabled = true;
    }


}
