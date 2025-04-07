using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isOpen;
    public GameObject toOpen;

    void Update()
    {
        toOpen.SetActive(!isOpen);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            isOpen = !isOpen;
        }        
    }
}
