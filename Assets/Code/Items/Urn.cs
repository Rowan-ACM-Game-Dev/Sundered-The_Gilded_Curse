using UnityEngine;

public class Urn : MonoBehaviour, IDragableItem
{
    public float Weight => 10f;
    Rigidbody2D rb;
    Transform transform;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }
    public void Drag(Vector2 direction)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(GameObject.FindWithTag("Player").transform);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("Player").GetComponent<Collider2D>());
    }

    public void StopDrag()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.SetParent(null);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("Player").GetComponent<Collider2D>(), false);
    }
}