using UnityEngine;

public class WireScriptFishingRod : MonoBehaviour
{
    GameObject wirePointFishingRod;
    [SerializeField] GameObject wirePointEndPoint;
    float wireSegmentCount = 10;
    float wireDanglingEffectStrongness = 0.3f;
    LineRenderer fishingLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wirePointFishingRod = gameObject;
        fishingLine = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
