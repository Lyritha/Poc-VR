using Meta.Voice;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class DuckGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject duckTable;
    [SerializeField] GameObject duckPrefab;
    [SerializeField] GameObject fishingRod;
    GameObject thisButtonObject;
    GameObject ActiveFishingRod;

    List<GameObject> ducksOnTable = new List<GameObject>();

    RotateTable tableRotationScript;

    public int amountOfDucksOnTable = 20;
    public int timePlayerGets = 30;
    public bool testSpawns = false;
    public bool gameIsActive = false;

    public float currentAmountOfTimeLeft = 0;


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
            //or if button gets pressed
            PressStartButton();
            testSpawns = false;
        }

        if (gameIsActive == true)
        {
            RemoveTimeFromTimer();
        }
    }

    public void PressStartButton()
    {
        RemoveAllDucksFromTable();
        PlaceDucksOnTable();
        InstantiateFishingRod();
        StartCoroutine(tableRotationScript.WaitRotateTable());
        ActivateTimer();
    }

    public void StopTableGame()
    {
        tableRotationScript.ChangeTableRotateStatusTo(false);
        RemoveAllDucksFromTable();
    }

    void PlaceDucksOnTable()
    {
        float degreesPerDuck = 360 / amountOfDucksOnTable;
        float currentDegree = 0;
        float mainOffset = 2.60f;

        for (int y = 0; y < amountOfDucksOnTable; y++)
        {
            Vector3 calculatedPosition = CalculateVectorAccordingToAngle(duckTable.transform.position, currentDegree, mainOffset);

            //calc (calc is slang for calculator) the position where duck should be placed twin
            GameObject thisDuck = Instantiate(duckPrefab, calculatedPosition, Quaternion.identity, duckTable.transform);
            currentDegree += degreesPerDuck;
            ducksOnTable.Add(thisDuck);
        }
    }

    Vector3 CalculateVectorAccordingToAngle(Vector3 beginPoint, float angle, float distance)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 calculatedPosition = beginPoint + rotation * (Vector3.forward * distance);
        return calculatedPosition;
    }

    void RemoveAllDucksFromTable()
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

    void InstantiateFishingRod()
    {
        if (ActiveFishingRod == null)
        {
            Vector3 offsetSpawnPosition = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 0.25f), (gameObject.transform.position.z - 1));
            //create the object
            GameObject newRod = GameObject.Instantiate(fishingRod, offsetSpawnPosition, Quaternion.identity);
            // Instantiate(Object original, Vector3 position, Quaternion rotation);
            //get script references from referenceScript.GiveReferences and pick index 1 (floaterscript)
            GameObject floater = newRod.GetComponent<ReferenceScripts>().GiveReferencesToMainScript()[1];
            //give the floater script this gameobject 
            floater.GetComponent<FloaterDetection>().SetMinigameManagerObject(gameObject);
            ActiveFishingRod = newRod;
        }
    }

    void ActivateTimer()
    {
        currentAmountOfTimeLeft = timePlayerGets;
        gameIsActive = true;
    }

    void RemoveTimeFromTimer()
    {
        if (currentAmountOfTimeLeft > 0)
        {
            currentAmountOfTimeLeft -= Time.deltaTime;
        }
        else
        {
            gameIsActive = false;
            RemoveAllDucksFromTable();
        }
    }
}
