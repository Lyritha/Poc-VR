using System.Collections.Generic;
using UnityEngine;

public class RotateTable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject duckTable;
    bool rotateTable = true;
    float rotateSpeed = 20f;
    List<GameObject> duckList;
    void Start()
    {
        duckList = new List<GameObject>();
        duckTable = gameObject;
        foreach(GameObject item in gameObject.transform.GetComponentsInChildren<GameObject>(true))
        {
            duckList.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateTable == true)
        {
            RotateDucksAroundTable(rotateSpeed, duckList);
        }
    }

    void RotateDucksAroundTable(float rotateSpeed, List<GameObject>ducklist)
    {
        foreach (GameObject duck in duckList)
        {
            duck.transform.Rotate(new Vector3(0,rotateSpeed,0));
        }
    }

    public void ChangeTableRotateStatus(bool status)
    {
        rotateTable = status;
    }
}
