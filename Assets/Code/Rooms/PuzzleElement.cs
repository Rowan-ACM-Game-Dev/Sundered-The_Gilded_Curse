using UnityEngine;

public class PuzzleElement : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public void ResetElement()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;

        // reset if it has velocity or an animation
        var rb = GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
