using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public float slashRange = 2f;
    public LayerMask slashableLayers;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Slash()
    {
        Vector2 direction = controller.FacingDirection;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, slashRange, slashableLayers);

        Debug.DrawRay(transform.position, direction * slashRange, Color.red, 0.5f);
        Debug.Log($"Slash raycast direction: {direction}");

        if (hit.collider != null)
        {
            Debug.Log("Slash hit: " + hit.collider.name + " | Tag: " + hit.collider.tag + " | Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            if (hit.collider.CompareTag("Slashable"))
            {
                Debug.Log("Hit is valid slashable target!");

                ISlashable slashable = hit.collider.GetComponent<ISlashable>();
                if (slashable != null)
                {
                    slashable.OnSlashed();
                }
                else
                {
                    Debug.LogWarning("Hit object has 'Slashable' tag but no ISlashable component.");
                }
            }
            else
            {
                Debug.Log("Hit something, but it's not tagged 'Slashable'");
            }
        }
        else
        {
            Debug.Log("Slash missed.");
        }
    }
}