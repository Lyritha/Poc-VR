using Oculus.Interaction;
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


    private void OnTriggerEnter(Collider other)
    {
        List<GameObject> ducksOnTable = gameScript.TellAllDucksOnTable();
        foreach (GameObject duck in ducksOnTable)
        {
            if (other.gameObject == duck.gameObject)
            {
                TryCatchGameobject(duck);
            }
        }
    }

    void TryCatchGameobject(GameObject toCatch)
    {
        if (canCatch == true)
        {
            toCatch.GetComponent<Rigidbody>().isKinematic = true;
            toCatch.transform.SetParent(floater.transform, true);
            currentCatch = toCatch;
            toCatch.transform.position = floater.transform.position;
            canCatch = false;
        }
    }

}
