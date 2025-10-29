using UnityEngine;

public class WireScriptFishingRod : MonoBehaviour
{
    GameObject wirePointFishingRod;
    [SerializeField] GameObject wirePointEndPoint;
    int wireSegmentCount = 3;
    float wireDanglingEffectStrongness = 0.3f;
    LineRenderer fishingLine;

    public Vector3 posA;
    public Vector3 posB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wirePointFishingRod = gameObject;
        fishingLine = gameObject.GetComponent<LineRenderer>();
        fishingLine.positionCount = wireSegmentCount;
        fishingLine.SetWidth(0.05f, 0.05f);
        fishingLine.SetColors(Color.white, Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        posA = wirePointFishingRod.transform.position;
        posB = wirePointEndPoint.transform.position;

        for (int i = 0; i < wireSegmentCount; i++)
        {
            Vector3 currentPoint = Vector3.Lerp(posA, posB, (i / (float)wireSegmentCount));
            if(i == wireSegmentCount-1) currentPoint = posB;
            fishingLine.SetPosition(i,currentPoint);
        }
        KeepFloaterInDistance(1.5f, posA,wirePointEndPoint);
    }


    void KeepFloaterInDistance(float radius, Vector3 posA, GameObject floater)
    {
        float distance = Vector3.Distance(posA, floater.transform.position);
        if(distance >= radius)
        {
            Vector3 richting = (floater.transform.position- posA).normalized;
            floater.transform.position = posA + richting * radius;
        }
    }
}
