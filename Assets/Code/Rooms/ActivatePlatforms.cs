using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class ActivatePlatforms : MonoBehaviour
{
    [System.Serializable]
    public class PlatformData
    {
        public Transform platform;
        public float onY = 0f;
        public float offY = 2.5f;

        [HideInInspector] public Vector3 onPos;
        [HideInInspector] public Vector3 offPos;
        [HideInInspector] public Vector3 velocity = Vector3.zero;
    }

    public bool Activated = false;
    public List<PlatformData> platforms = new List<PlatformData>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var p in platforms)
        {
            Vector3 basePos = p.platform.position;
            p.onPos = new Vector3(basePos.x, p.onY, basePos.z);
            p.offPos = new Vector3(basePos.x, p.offY, basePos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var p in platforms)
        {
            Vector3 targetPos = Activated ? p.onPos : p.offPos;
            p.platform.position = Vector3.SmoothDamp(p.platform.position, targetPos, ref p.velocity, 0.3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pressure Plate Activated!");
            Activated = !Activated;
        }
    }
}
