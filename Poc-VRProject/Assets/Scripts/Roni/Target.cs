using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ring"))
        {
            scoreManager.AddScore(1);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true; 
            collision.gameObject.transform.position = transform.position + new Vector3(0, 0.1f, 0);
        }
    }
}
