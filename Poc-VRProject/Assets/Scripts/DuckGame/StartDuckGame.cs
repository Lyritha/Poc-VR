using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StartDuckGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject duckTable;
    [SerializeField] GameObject duckPrefab;
    [SerializeField] GameObject fishingRod;

    GameObject ActiveFishingRod;

    RotateTable tableRotationScript;
    List<GameObject> ducksOnTable = new List<GameObject>();
    public int amountOfDucksOnTable = 10;
    public bool testSpawns = false;

    GameObject thisButtonObject;
    void Start()
    {
        thisButtonObject = gameObject;
        tableRotationScript = duckTable.GetComponent<RotateTable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testSpawns == true)
        {
            pressStartButton();
            testSpawns = false;
        }
    }

    void pressStartButton()
    {
        removeAllDucksFromTable();
        placeDucksOnTable();
        instantiateFishingRod();
        StartCoroutine(tableRotationScript.waitRotateTable());
    }

    public void stopTableGame()
    {
        tableRotationScript.ChangeTableRotateStatusTo(false);
        removeAllDucksFromTable();
    }

    void placeDucksOnTable()
    {
        float degreesPerDuck = 360 / amountOfDucksOnTable;
        float currentDegree = 0;
        float mainOffset = 2.60f;

        for (int y = 0; y < amountOfDucksOnTable; y++)
        {
            Vector3 calculatedPosition = calculateVectorAccordingToAngle(duckTable.transform.position, currentDegree, mainOffset);

            //calc (calc is slang for calculator) the position where duck should be placed twin
            GameObject thisDuck = Instantiate(duckPrefab, calculatedPosition, Quaternion.identity, duckTable.transform);
            currentDegree += degreesPerDuck;
            ducksOnTable.Add(thisDuck);
        }
    }

    Vector3 calculateVectorAccordingToAngle(Vector3 beginPoint, float angle, float distance)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 calculatedPosition = beginPoint + rotation * (Vector3.forward * distance);
        return calculatedPosition;
    }

    void removeAllDucksFromTable()
    {
        foreach (GameObject duck in ducksOnTable)
        {
            GameObject.Destroy(duck);
        }
        tableRotationScript.ChangeTableRotateStatusTo(false);
    }

    public List<GameObject> TellAllDucksOnTable()
    {
        return ducksOnTable;
    }

    void instantiateFishingRod()
    {
        if (ActiveFishingRod == null)
        {
            //create the object
            GameObject newRod = GameObject.Instantiate(fishingRod);
            //get script references from referenceScript.GiveReferences and pick index 1 (floaterscript)
            GameObject floater = newRod.GetComponent<ReferenceScripts>().giveReferencesToMainScript()[1];
            //give the floater script this gameobject 
            floater.GetComponent<FloaterDetection>().setMinigameManagerObject(gameObject);
            ActiveFishingRod = newRod;
        }
    }
}
