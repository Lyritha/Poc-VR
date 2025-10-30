using UnityEngine;

public class ReferenceScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject rodEnd;
    [SerializeField] GameObject floater;
    public GameObject[] GiveReferencesToMainScript()
    {
        return new GameObject[] { rodEnd, floater };
    }


}
