using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            scoreManager.AddScore(20);
            //other.gameObject.transform.position = transform.position + new Vector3(0, 0.1f, 0);
        }
    }

}
