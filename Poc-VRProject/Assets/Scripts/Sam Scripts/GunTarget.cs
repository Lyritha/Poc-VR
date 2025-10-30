using UnityEngine;
using UnityEngine.SceneManagement;

public class GunTarget : MonoBehaviour
{
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashTime = 0.1f; // flash for 0.1s

    private Renderer rend;
    private Color originalColor;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    // Call this when the target is hit
    public void Hit()
    {
        if (rend != null)
            StartCoroutine(FlashAndDestroy());

    }

    private System.Collections.IEnumerator FlashAndDestroy()
    {
        rend.material.color = hitColor;
        yield return new WaitForSeconds(flashTime);
        Destroy(gameObject); // destroy after flash
        ScoreManagerGun.Instance.AddScore(100);
    }
}
