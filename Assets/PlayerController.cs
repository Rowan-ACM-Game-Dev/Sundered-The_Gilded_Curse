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
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direc * speed;

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
}
