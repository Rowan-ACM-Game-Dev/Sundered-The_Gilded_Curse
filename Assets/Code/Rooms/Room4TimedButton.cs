using System;
using System.Collections;
using UnityEngine;

public class Room4TimedButton : MonoBehaviour
{
    [SerializeField]
    public Boolean activated = false;
    [SerializeField]
    public Transform upperDoor;
    [SerializeField]
    public Transform lowerDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            StartCoroutine(ActivateDoorTimed());
        }
    }

    IEnumerator ActivateDoorTimed()
    {
        print("Activated");
        activated = true;

        Vector3 velocity1 = Vector3.zero;
        Vector3 velocity2 = Vector3.zero;
        Vector3 upperDoorOpenPos = new Vector3(upperDoor.transform.localPosition.x, -1.35f, upperDoor.transform.localPosition.z);
        Vector3 lowerDoorOpenPos = new Vector3(lowerDoor.transform.localPosition.x, -6.8f, lowerDoor.transform.localPosition.z);
        Vector3 upperDoorClosePos = new Vector3(upperDoor.transform.localPosition.x, -3.069216f, upperDoor.transform.localPosition.z);
        Vector3 lowerDoorClosePos = new Vector3(lowerDoor.transform.localPosition.x, -5.05f, lowerDoor.transform.localPosition.z);

        // Open Door
        print("Opening Door");
        float endTime = Time.time + 1;

        while (true)
        {
            upperDoor.localPosition = Vector3.SmoothDamp(upperDoor.localPosition, upperDoorOpenPos, ref velocity1, 0.5F);
            lowerDoor.localPosition = Vector3.SmoothDamp(lowerDoor.localPosition, lowerDoorOpenPos, ref velocity2, 0.5F);

            yield return null; // Executes every frame until door is done moving

            if ((Time.time > endTime)) { break; }
        }

        // Close door

        yield return new WaitForSeconds(3f);

        print("Closing Door");
        endTime = Time.time + 8;
        velocity1 = Vector3.zero;
        velocity2 = Vector3.zero;

        while (true)
        {
            upperDoor.localPosition = Vector3.SmoothDamp(upperDoor.localPosition, upperDoorClosePos, ref velocity1, 2F);
            lowerDoor.localPosition = Vector3.SmoothDamp(lowerDoor.localPosition, lowerDoorClosePos, ref velocity2, 2F);

            yield return null; // Executes every frame until door is done moving

            if ((Time.time > endTime)) { break; }
        }

        activated = false;
        print("Stopped");
    }
}
