using UnityEngine;

public class SahiraSpin : MonoBehaviour
{
    public float rotationSpeed = 45f; 

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
