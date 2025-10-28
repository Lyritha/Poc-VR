using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StartDuckGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject duckTable;
    [SerializeField] GameObject duckPrefab;
    RotateTable tableRotationScript;
    List<GameObject> ducksOnTable = new List<GameObject>();
    int amountOfDucksOnTable = 6;
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
        for (int i = 0; i < amountOfDucksOnTable; i++)
        {
            Vector3 calculatedPosition = calculateVectorAccordingToAngle(duckTable.transform.position, currentDegree, 2f);
            //calc (calc is slang for calculator) the position where duck should be placed twin
            Instantiate(duckPrefab, calculatedPosition, Quaternion.identity);
            currentDegree += degreesPerDuck;
        }
        //place ducks on table in a circle or something.
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
    }
}
