using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMouseRotation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - rb.transform.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 180); // flip to face away
        rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, targetRotation, 5f * Time.deltaTime);
    }
}