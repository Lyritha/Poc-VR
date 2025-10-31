using UnityEngine;

public class CanCheck : MonoBehaviour
{
    [SerializeField] TextManager textManagerScript;

    private void Start()
    {
        textManagerScript = FindFirstObjectByType<TextManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.GetComponent<Floor>())
        {
            textManagerScript.AddScore();
            Destroy(gameObject);
        }
    }
}
