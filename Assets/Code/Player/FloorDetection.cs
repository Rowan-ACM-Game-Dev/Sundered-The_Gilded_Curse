using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    Vector2 point1;
    Vector2 point2;
    Vector2 point3;
    Vector2 point4;
    Vector2 playerPos;

    //int includesLayer0 = 1; // in binary, this is ....0000001
    //int includesLayer4 = 1 << 4; //"shift these bits to the left 4 places". this is ....0001000
    LayerMask checkedLayers = Physics2D.AllLayers << 1;
    LayerMask currentLayer;
    LayerMask overallLayer;

    float playerSizex;
    float playerSizey;

    void Start()
    {
        playerSizex = transform.localScale.x/2;
        playerSizey = transform.localScale.y / 2;

        point1 = new Vector2(-playerSizex, -playerSizey);
        point2 = new Vector2(-playerSizex, playerSizey);
        point3 = new Vector2(playerSizex, -playerSizey);
        point4 = new Vector2(playerSizex, playerSizey);
        //int includesLayers0and4 = includesLayer0 & includesLayer4; //this is ...0001001
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform.localPosition;

        Collider2D obj1 = Physics2D.OverlapPoint(point1 + playerPos, checkedLayers);
        Collider2D obj2 = Physics2D.OverlapPoint(point2 + playerPos, checkedLayers);
        Collider2D obj3 = Physics2D.OverlapPoint(point3 + playerPos, checkedLayers);
        Collider2D obj4 = Physics2D.OverlapPoint(point4 + playerPos, checkedLayers);

        if (obj1 != null && obj2 != null && obj3 != null && obj4 != null)
        {
            LayerMask layer1 = obj1.gameObject.layer;
            LayerMask layer2 = obj2.gameObject.layer;
            LayerMask layer3 = obj3.gameObject.layer;
            LayerMask layer4 = obj4.gameObject.layer;

            overallLayer = layer1 | layer2 | layer3 | layer4;

            //Debug.Log("Layer1: " + LayerMask.LayerToName(layer1) + "   =    " + (point1 + playerPos));
            //Debug.Log("Layer2: " + LayerMask.LayerToName(layer2) + "   =    " + (point2 + playerPos));
            //Debug.Log("Layer3: " + LayerMask.LayerToName(layer3) + "   =    " + (point3 + playerPos));
            //Debug.Log("Layer4: " + LayerMask.LayerToName(layer4) + "   =    " + (point4 + playerPos));

            //Debug.Log(overallLayer.ToString());

            if (overallLayer == layer1)
            {
                currentLayer = overallLayer;
                Debug.Log(LayerMask.LayerToName(currentLayer));
            }
        }
    }
}
