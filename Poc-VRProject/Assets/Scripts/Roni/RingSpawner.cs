using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    public GameObject ringPrefab; // link hier je Ring prefab
    public int ringCount = 5; // aantal ringen
    public Transform spawnPoint; // waar de ringen verschijnen

    public float spacing = 0.3f; // afstand tussen ringen

    void Start()
    {
        for (int i = 0; i < ringCount; i++)
        {
            Vector3 position = spawnPoint.position + new Vector3(i * spacing, 0, 0);
            Instantiate(ringPrefab, position, Quaternion.identity);
        }
    }
}
