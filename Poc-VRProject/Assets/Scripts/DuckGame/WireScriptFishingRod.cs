using UnityEngine;

public class WireScriptFishingRod : MonoBehaviour
{
    GameObject wirePointFishingRod;
    [SerializeField] GameObject floater;
    int wireSegmentCount = 3;
    LineRenderer fishingLine;
    float LineLenght = 1f;

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
        KeepFloaterInDistance(LineLenght, posA, floater);
        posB = floater.transform.position;

        for (int i = 0; i < wireSegmentCount; i++)
        {
            Vector3 currentPoint = Vector3.Lerp(posA, posB, (i / (float)wireSegmentCount));
            if(i == wireSegmentCount-1) currentPoint = posB;
            fishingLine.SetPosition(i,currentPoint);
        }
    }


    void KeepFloaterInDistance(float radius, Vector3 posA, GameObject floater)
    { 
        //berekend hoever de dobber van de maximale waarde is
        Vector3 overshootOfFloater = floater.transform.position - posA;
        float distance = overshootOfFloater.magnitude;

        if(distance >= radius)
        {
            Vector3 dir = -overshootOfFloater;
            float actualOvershoot = distance - radius;

            float power = actualOvershoot * 8f;
            floater.GetComponent<Rigidbody>().AddForce(dir * power);
        }
    }
}
