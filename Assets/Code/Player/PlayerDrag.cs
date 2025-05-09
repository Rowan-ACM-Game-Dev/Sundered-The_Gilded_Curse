using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]

public class PlayerDrag : MonoBehaviour
{
    private GameObject currentDragable;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<IDragable>() != null)
            currentDragable = collision.gameObject;
    }
    public void Drag()
    {
        stickToPlayer();
        bool dragHorizontal = calculateDirection();
        lockPlayerMovement(dragHorizontal);
    }
    public void StopDrag()
    {
        if (currentDragable != null)
        {
            unstickFromPlayer();
            unlockPlayerMovement();
            currentDragable = null;
        }
    }
    private void lockPlayerMovement(bool dragHorizontal)
    {
        if (dragHorizontal)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        else
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }
    private void unlockPlayerMovement()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void stickToPlayer()
    {
        currentDragable.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        currentDragable.transform.SetParent(transform);
        Physics2D.IgnoreCollision(currentDragable.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    private void unstickFromPlayer()
    {
        currentDragable.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        currentDragable.transform.SetParent(null);
        Physics2D.IgnoreCollision(currentDragable.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
    private bool calculateDirection()
    {
        bool dragHorizontal = false;
        float deltaX = currentDragable.transform.position.x - transform.position.x;
        float deltaY = currentDragable.transform.position.y - transform.position.y;
        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            dragHorizontal = true;
        return dragHorizontal;
    }

}