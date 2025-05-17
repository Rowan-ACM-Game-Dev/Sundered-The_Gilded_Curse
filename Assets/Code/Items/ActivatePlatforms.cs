using UnityEngine;
using System.Collections.Generic;

public class ActivatePlatforms : MonoBehaviour, IActivatable
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

    public bool activated = false;
    public bool activateOnPlayerEnter = true;
    public List<PlatformData> platforms = new();

    void Start()
    {
        foreach (var p in platforms)
        {
            Vector3 basePos = p.platform.position;
            p.onPos = new Vector3(basePos.x, p.onY, basePos.z);
            p.offPos = new Vector3(basePos.x, p.offY, basePos.z);
        }
    }

    void Update()
    {
        foreach (var p in platforms)
        {
            Vector3 targetPos = activated ? p.onPos : p.offPos;
            p.platform.position = Vector3.SmoothDamp(p.platform.position, targetPos, ref p.velocity, 0.3f);
        }
    }

    public void Activate()
    {
        activated = !activated;
        Debug.Log("Platforms toggled via IActivatable!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activateOnPlayerEnter && collision.CompareTag("Player"))
        {
            Debug.Log("Pressure Plate triggered ActivatePlatforms");
            Activate();
        }
    }
}
