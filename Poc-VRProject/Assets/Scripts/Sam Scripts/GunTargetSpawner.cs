using UnityEngine;
using System.Collections;

public class GunTargetSpawner : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float spawnInterval = 2f; // time between spawns
    [SerializeField] private float targetLifetime = 10f; // destroy if not hit
    [SerializeField] private float launchForce = 10f;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnAreaCenter = Vector3.zero; // center of spawn area
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(2f, 0f, 2f); // size in X,Z

    [Header("Launch Settings")]
    [SerializeField] private float launchAngle = 30f; // degrees sideways from forward

    [SerializeField] private ScoreManagerGun scoreManagerGun;
    public bool gameStarted = false;
    public int spawnCount = 10;

    private void Update()
    {
        if (gameStarted == true)
        {
            scoreManagerGun.score = 0;
            spawnCount = 10;
            gameStarted = false;
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2);
        scoreManagerGun.scoreText.text = "Go!";
        while (spawnCount >= 0)
        {
            if (spawnCount <= 0)
            {
                if (scoreManagerGun.scoreText != null)
                {
                    scoreManagerGun.scoreText.text = "Game Over Score: " + scoreManagerGun.score;
                    scoreManagerGun.EndGame();
                }
                gameStarted = false;
                break;
            }
            SpawnTarget();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnTarget()
    {
        spawnCount--;

        // Random spawn position inside spawn area
        Vector3 spawnPos = new Vector3(
            spawnAreaCenter.x + Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            spawnAreaCenter.y,
            spawnAreaCenter.z + Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        );

        GameObject target = Instantiate(targetPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb == null) rb = target.AddComponent<Rigidbody>();

        // Launch sideways at an angle
        float radians = launchAngle * Mathf.Deg2Rad;
        Vector3 launchDir = new Vector3(Mathf.Sin(radians), 0.7f, Mathf.Cos(radians)).normalized;
        // 0.5 in Y to still give a bit of lift

        rb.AddForce(launchDir * launchForce, ForceMode.Impulse);

        Destroy(target, targetLifetime);
    }

}
