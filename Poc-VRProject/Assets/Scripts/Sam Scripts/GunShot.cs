using System.Collections;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [Header("Shot Settings")]
    [SerializeField] private int pelletCount = 8; // how many pellets per shot
    [SerializeField] private float spreadAngle = 3f; // degrees of spread, small for long-range
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private float range = 100f;

    [Header("References")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject particles;

    private float lastShootTime;

    public void Shoot()
    {
        particles.SetActive(false);
        particles.SetActive(true);

        if (lastShootTime + shootDelay <= Time.time)
        {
            // Shoot multiple pellets
            for (int i = 0; i < pelletCount; i++)
            {
                Vector3 direction = GetSpreadDirection(bulletSpawnPoint);
                Vector3 targetPoint;

                if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, range, mask))
                {
                    targetPoint = hit.point;

                    // Check if we hit a GunTarget
                    GunTarget target = hit.collider.GetComponent<GunTarget>();
                    if (target != null)
                        target.Hit();
                }
                else
                {
                    // No hit → project forward
                    targetPoint = bulletSpawnPoint.position + direction * range;
                }

                // Spawn trail for each pellet
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
                StartCoroutine(SpawnTrail(trail, targetPoint));
            }

            lastShootTime = Time.time;
        }
    }

    // Generate a slightly randomized direction inside a cone for shotgun spread
    private Vector3 GetSpreadDirection(Transform spawnPoint)
    {
        // Base direction
        Vector3 direction = spawnPoint.forward;

        // Random rotation within cone
        float angleX = Random.Range(-spreadAngle, spreadAngle);
        float angleY = Random.Range(-spreadAngle, spreadAngle);

        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
        direction = rotation * direction;

        return direction.normalized;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 targetPoint)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, targetPoint, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = targetPoint;
        Destroy(trail.gameObject, trail.time);
    }
}
