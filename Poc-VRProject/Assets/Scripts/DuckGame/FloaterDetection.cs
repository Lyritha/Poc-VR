using System.Collections.Generic;
using UnityEngine;

public class FloaterDetection : MonoBehaviour
{
    GameObject floater;
    [SerializeField] GameObject minigameManager;
    StartDuckGame gameScript;

    public GameObject currentCatch;

    public bool canCatch = true;

    private void Start()
    {
        floater = gameObject;
        gameScript = minigameManager.GetComponent<StartDuckGame>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter(Collision collision)
    {
        List<GameObject> ducksOnTable = gameScript.TellAllDucksOnTable();
        foreach (GameObject duck in ducksOnTable)
        {
            if (collision.gameObject == duck.gameObject)
            {
                TryCatchGameobject(duck);
            }
        }
    }

    void TryCatchGameobject(GameObject toCatch)
    {
        if (canCatch == true)
        {
            toCatch.transform.parent = floater.transform;
            currentCatch = toCatch;
            toCatch.transform.localPosition = floater.transform.localPosition;
            canCatch = false;
        }
    }

}
