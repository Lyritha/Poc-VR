using UnityEngine;
using UnityEngine.SceneManagement;

public class GunTarget : MonoBehaviour
{
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashTime = 0.1f;
    [SerializeField] private AudioSource hitSound;

    private Renderer rend;
    private Color originalColor;

    private void Start()
    {
        Destroy(gameObject, 3f);
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
        hitSound.Play();
        rend.material.color = hitColor;
        yield return new WaitForSeconds(flashTime);
        Destroy(gameObject);
        ScoreManagerGun.Instance.AddScore(2);
    }
}
