using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class PlayerDrag : MonoBehaviour
{
    private IDragableItem draggingObject;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<IDragableItem>() != null)
        {
            draggingObject = collision.gameObject.GetComponent<IDragableItem>();
            draggingObject.Drag(Vector2.right);
            //Debug.Log("Dragging: " + collision.collider.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<IDragableItem>() != null)
        {
            draggingObject = null;
            //Debug.Log("Stoped dragging: " + collision.gameObject.name);
        }
    }
}