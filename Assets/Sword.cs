using UnityEngine;

public class Sword : MonoBehaviour
{
    public Sword()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(collision.gameObject);
        }
    }
}
