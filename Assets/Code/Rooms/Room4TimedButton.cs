using System;
using System.Collections;
using UnityEngine;

public class Room4TimedButton : MonoBehaviour
{
    public GameObject door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && door.activeSelf)
        {
            StartCoroutine(ActivateDoorTimed(5));
        }

    }

    IEnumerator ActivateDoorTimed(int waitSeconds)
    {
        door.SetActive(false);
        yield return new WaitForSeconds(waitSeconds);
        door.SetActive(true);
    }
}
