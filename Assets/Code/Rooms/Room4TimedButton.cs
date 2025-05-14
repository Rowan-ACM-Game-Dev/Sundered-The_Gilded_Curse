using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;  

public class Room4TimedButton : MonoBehaviour
{
    [SerializeField]
    private Boolean activated = false;
    [SerializeField]
    private Transform upperDoor;
    [SerializeField]
    private Transform lowerDoor;

    [Header("Timer Settings")]
    [SerializeField]
    private float puzzleTime = 10f;
    private float timeRemaining;

    [Header("UI Elements")]
    [SerializeField]
    private Text timerText; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            StartCoroutine(ActivateDoorTimed());
        }
    }

    IEnumerator ActivateDoorTimed()
    {
        timeRemaining = puzzleTime;
        activated = true;
        print("Puzzle Started");

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);  
        }

        Vector3 velocity1 = Vector3.zero;
        Vector3 velocity2 = Vector3.zero;
        Vector3 upperDoorOpenPos = new Vector3(upperDoor.transform.localPosition.x, -1.35f, upperDoor.transform.localPosition.z);
        Vector3 lowerDoorOpenPos = new Vector3(lowerDoor.transform.localPosition.x, -6.8f, lowerDoor.transform.localPosition.z);
        Vector3 upperDoorClosePos = new Vector3(upperDoor.transform.localPosition.x, -3.069216f, upperDoor.transform.localPosition.z);
        Vector3 lowerDoorClosePos = new Vector3(lowerDoor.transform.localPosition.x, -5.05f, lowerDoor.transform.localPosition.z);

        // Open the door
        print("Opening Door");
        float endTime = Time.time + 1;

        while (Time.time < endTime)
        {
            upperDoor.localPosition = Vector3.SmoothDamp(upperDoor.localPosition, upperDoorOpenPos, ref velocity1, 0.5f);
            lowerDoor.localPosition = Vector3.SmoothDamp(lowerDoor.localPosition, lowerDoorOpenPos, ref velocity2, 0.5f);
            yield return null;
        }

        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timeRemaining <= 0f)
            {
                print("Time's up!");
                break;
            }

            yield return null;
        }

        StartCoroutine(CloseDoors()); 

        // Reset puzzle state after timer ends
        activated = false;
        print("Puzzle Stopped");
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString("0");  // Display the remaining time
        }
    }

    IEnumerator CloseDoors()
    {
        print("Closing Door");
        Vector3 velocity1 = Vector3.zero;
        Vector3 velocity2 = Vector3.zero;
        Vector3 upperDoorClosePos = new Vector3(upperDoor.transform.localPosition.x, -3.069216f, upperDoor.transform.localPosition.z);
        Vector3 lowerDoorClosePos = new Vector3(lowerDoor.transform.localPosition.x, -5.05f, lowerDoor.transform.localPosition.z);

        float endTime = Time.time + 8f;

        while (Time.time < endTime)
        {
            upperDoor.localPosition = Vector3.SmoothDamp(upperDoor.localPosition, upperDoorClosePos, ref velocity1, 2f);
            lowerDoor.localPosition = Vector3.SmoothDamp(lowerDoor.localPosition, lowerDoorClosePos, ref velocity2, 2f);
            yield return null;
        }
    }
}
