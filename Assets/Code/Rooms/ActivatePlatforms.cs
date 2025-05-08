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

    //public Transform Platform1;
    //public Transform Platform2;
    //public Transform Platform3;

    //private Vector3 velocity1 = Vector3.zero;
    //private Vector3 velocity2 = Vector3.zero;
    //private Vector3 velocity3 = Vector3.zero;

    //// Target posotions
    //// platform 1
    //private Vector3 platform10nOnPos;
    //private Vector3 platform10nOffPos;
    //// platform 2
    //private Vector3 platform20nOnPos;
    //private Vector3 platform20nOffPos;
    //// platform 3
    //private Vector3 platform30nOnPos;
    //private Vector3 platform30nOffPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var p in platforms)
        {
            Vector3 basePos = p.platform.position;
            p.onPos = new Vector3(basePos.x, p.onY, basePos.z);
            p.offPos = new Vector3(basePos.x, p.offY, basePos.z);
        }

        //platform10nOnPos = new Vector3(Platform1.position.x, 0f, Platform1.position.z);
        //platform10nOffPos = new Vector3(Platform1.position.x, 2.5f, Platform1.position.x);

        //platform20nOnPos = new Vector3(Platform1.position.x, 0f, Platform1.position.z);
        //platform20nOffPos = new Vector3(Platform1.position.x, -2.5f, Platform1.position.x);

        //platform30nOnPos = new Vector3(Platform1.position.x, 0f, Platform1.position.z);
        //platform30nOffPos = new Vector3(Platform1.position.x, 2.5f, Platform1.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var p in platforms)
        {
            Vector3 targetPos = Activated ? p.onPos : p.offPos;
            p.platform.position = Vector3.SmoothDamp(p.platform.position, targetPos, ref p.velocity, 0.3f);
        }
        //if (Activated)
        //{

        //    Platform1.position = Vector3.SmoothDamp(Platform1.position, platform10nOnPos, ref velocity1, 0.3F);
        //    Platform2.position = Vector3.SmoothDamp(Platform2.position, platform20nOnPos, ref velocity2, 0.3F);
        //    Platform3.position = Vector3.SmoothDamp(Platform3.position, platform30nOnPos, ref velocity3, 0.3F);
        //}
        //else
        //{

        //    Platform1.position = Vector3.SmoothDamp(Platform1.position, platform10nOffPos, ref velocity1, 0.3F);
        //    Platform2.position = Vector3.SmoothDamp(Platform2.position, platform20nOffPos, ref velocity2, 0.3F);
        //    Platform3.position = Vector3.SmoothDamp(Platform3.position, platform30nOffPos, ref velocity3, 0.3F);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Activated = !Activated;
        }
    }
}
