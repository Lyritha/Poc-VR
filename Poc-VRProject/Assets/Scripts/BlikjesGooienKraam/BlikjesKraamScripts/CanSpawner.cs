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
            //Calculeer de spawnpositie tenopzichte van de groote en de breedte van de blikjes
            float startX = spawnPoint.position.x - ((cansInRow - 1) * canWidth / 2f);
            float y = spawnPoint.localScale.y + spawnPoint.position.y + canHeight + (row * (canHeight * 2));

            for (int i = 0; i < cansInRow; i++)
            {
                float x = startX + (i * canWidth);
                float yPos = y;

                // Offset t.o.v. spawnPoint
                Vector3 offset = new Vector3(x - spawnPoint.position.x, yPos - spawnPoint.position.y, 0f);

                // Roteer de offset met parentrotatie
                Vector3 rotatedOffset = spawnPoint.parent.rotation * offset;

                // Voeg spawnPoint positie toe
                Vector3 spawnPos = spawnPoint.position + rotatedOffset;

                GameObject canObject = Instantiate(can, spawnPos, Quaternion.identity);
                cans.Add(canObject);
            }
        }
    }

    public void DestroyExistingCans()
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
