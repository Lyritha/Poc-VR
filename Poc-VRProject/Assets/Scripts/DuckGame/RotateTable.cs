using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject duckTable;
    bool rotateTable = false;
    float rotateSpeed = 20f;
    List<GameObject> duckList;
    void Start()
    {
        duckList = new List<GameObject>();
        duckTable = gameObject;
        StartCoroutine(waitRotateTable());
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateTable == true)
        {
            RotateTheTable(rotateSpeed, duckTable);
        }
    }

    void RotateTheTable(float rotateSpeed, GameObject table)
    {

        table.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }

    public void ChangeTableRotateStatusTo(bool status)
    {
        rotateTable = status;
    }

    public IEnumerator waitRotateTable()
    {
        yield return new WaitForSeconds(2f);
        ChangeTableRotateStatusTo(true);
    }
}
