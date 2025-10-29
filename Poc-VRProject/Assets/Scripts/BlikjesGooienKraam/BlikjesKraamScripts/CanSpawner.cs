using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{
    [SerializeField] int rows = 4;        
    [SerializeField] GameObject can;      
    [SerializeField] Transform spawnPoint;
    private List<GameObject> cans = new List<GameObject>();

    private void GenerateCans()
    {
        float canWidth = can.transform.localScale.x;
        float canHeight = can.transform.localScale.y;

        for (int row = 0; row < rows; row++)
        {
            int cansInRow = rows - row;

            float startX = spawnPoint.position.x - ((cansInRow - 1) * canWidth / 2f);
            float y = spawnPoint.localScale.y + spawnPoint.position.y + canHeight + (row * (canHeight * 2));

            for (int i = 0; i < cansInRow; i++)
            {
                float x = startX + (i * canWidth);
                Vector3 spawnPos = new Vector3(x, y, spawnPoint.position.z);

                GameObject canObject =Instantiate(can, spawnPos, Quaternion.identity);
                cans.Add(canObject);
            }
        }
    }

    private void DestroyExistingCans()
    {
        foreach(GameObject o in cans)
        {
            Destroy(o);
        }
    }
    public void RespawnCans()
    {
        DestroyExistingCans();
        GenerateCans();
    }
}
