using System.Collections.Generic;
using UnityEngine;

public class BalSpawner : MonoBehaviour
{
    [SerializeField] GameObject balPrefab;
    [SerializeField] Transform SpawnPos;
    private List<GameObject> bal = new List<GameObject>();

    private void SpawnBalls()
    {
        DeleteExistingBalls();
        for(int i = 0; i < 3; i++)
        {
           GameObject o = Instantiate(balPrefab, SpawnPos.position, Quaternion.identity);
            bal.Add(o);
        }
    }

    private void DeleteExistingBalls()
    {
        foreach(GameObject o in bal)
        {
            Destroy(o);
        }
    }

    public void RespawnBalls()
    {
        DeleteExistingBalls();
        SpawnBalls();
    }
}
