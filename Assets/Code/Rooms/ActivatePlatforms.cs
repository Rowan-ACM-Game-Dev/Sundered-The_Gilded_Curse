using Unity.VisualScripting;
using UnityEngine;

public class ActivatePlatforms : MonoBehaviour
{
    public bool Activated;
    public Transform Platform1;
    public Transform Platform2;
    public Transform Platform3;

    private Vector3 velocity1 = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;
    private Vector3 velocity3 = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Activated)
        {
            Vector3 Platform1Pos = new Vector3(Platform1.position.x, 0, Platform1.position.z);
            Vector3 Platform2Pos = new Vector3(Platform2.position.x, 0, Platform2.position.z);
            Vector3 Platform3Pos = new Vector3(Platform3.position.x, 0, Platform3.position.z);

            Platform1.position = Vector3.SmoothDamp(Platform1.position, Platform1Pos, ref velocity1, 0.3F);
            Platform2.position = Vector3.SmoothDamp(Platform2.position, Platform2Pos, ref velocity2, 0.3F);
            Platform3.position = Vector3.SmoothDamp(Platform3.position, Platform3Pos, ref velocity3, 0.3F);
        }
        else
        {
            Vector3 Platform1Pos = new Vector3(Platform1.position.x, 2.5F, Platform1.position.z);
            Vector3 Platform2Pos = new Vector3(Platform2.position.x, -2.5F, Platform2.position.z);
            Vector3 Platform3Pos = new Vector3(Platform3.position.x, 2.5F, Platform3.position.z);

            Platform1.position = Vector3.SmoothDamp(Platform1.position, Platform1Pos, ref velocity1, 0.3F);
            Platform2.position = Vector3.SmoothDamp(Platform2.position, Platform2Pos, ref velocity2, 0.3F);
            Platform3.position = Vector3.SmoothDamp(Platform3.position, Platform3Pos, ref velocity3, 0.3F);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Activated = !Activated;
        }
        
    }
}
