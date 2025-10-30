using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject duckTable;
    bool rotateTable = false;
    float rotateSpeed = 40f;
    void Start()
    {
        duckTable = gameObject;
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

    public IEnumerator WaitRotateTable()
    {
        yield return new WaitForSeconds(1.5f);
        ChangeTableRotateStatusTo(true);
    }
}
