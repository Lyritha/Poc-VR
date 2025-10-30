using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    public GameObject ringPrefab; 
    public int ringCount = 5; 
    public Transform spawnPoint; 

    public float spacing = 0.3f; 

    void Start()
    {
        for (int i = 0; i < ringCount; i++)
        {
            Vector3 position = spawnPoint.position + new Vector3(i * spacing, 0, 0);
            Instantiate(ringPrefab, position, Quaternion.identity);
        }
    }
}
