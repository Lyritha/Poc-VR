using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider col;

    private void FixedUpdate()
    {
        if (rb.isKinematic) return;

        if (rb.linearVelocity.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized) * Quaternion.Euler(0, -90, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        Destroy(col);
    }
}
