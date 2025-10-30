using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    private float force = 50;

    [SerializeField] 
    private Transform bowTransform;
    [SerializeField] 
    private Transform stringTransform;
    [SerializeField]
    private Arrow arrowPrefab;
    [SerializeField]
    private Animator bowAnimator;
    [SerializeField]
    private TMP_Text arrowCount;
    [SerializeField]
    private RectTransform uiArrowCount;


    [SerializeField]
    private HandGrabInteractable grabbableString;

    private Vector3 initialLocalStringPosition;
    private float startDistance;
    private bool isGrabbed = false;
    private List<Arrow> arrows = new();
    private int currentArrowCount = 0;


    private BowShooting parent;
    public void Initialize(BowShooting parent, int count)
    {
        this.parent = parent;
        currentArrowCount = count;

        bool activate = currentArrowCount > 0;
        uiArrowCount.gameObject.SetActive(activate);
    }

    private void Awake()
    {
        if (grabbableString == null) return;

        grabbableString.WhenSelectingInteractorAdded.Action += OnGrabBegin;
        grabbableString.WhenSelectingInteractorRemoved.Action += OnGrabEnd;
    }

    private void OnDestroy()
    {
        foreach (Arrow arrow in arrows)
            if (arrow != null) Destroy(arrow.gameObject);

        if (grabbableString == null) return;

        grabbableString.WhenSelectingInteractorAdded.Action -= OnGrabBegin;
        grabbableString.WhenSelectingInteractorRemoved.Action -= OnGrabEnd;
    }

    private void OnGrabBegin(IInteractor interactor) => isGrabbed = true;
    private void OnGrabEnd(IInteractor interactor)
    {
        isGrabbed = false;
        FireArrow();
    }



    private void Start()
    {
        startDistance = Vector3.Distance(stringTransform.position, bowTransform.position);
        initialLocalStringPosition = bowTransform.InverseTransformPoint(stringTransform.position);
    }

    private void Update()
    {
        float distance = Vector3.Distance(stringTransform.position, bowTransform.position);
        float pullAmount = Mathf.InverseLerp(startDistance, startDistance + 0.5f, distance);
        pullAmount = Mathf.Clamp01(pullAmount);

        bowAnimator.SetFloat("PullAmount", pullAmount);
    }

    private void FireArrow()
    {
        // Disable all interactables
        StartCoroutine(FrameDelay());

        currentArrowCount--;
        if (currentArrowCount <= 0)
        {
            StartCoroutine(EndDelay());
            return;
        }

        float distance = Vector3.Distance(stringTransform.position, bowTransform.position);
        distance -= startDistance;

        Vector3 shootDirection = (bowTransform.position - stringTransform.position).normalized;

        // fire arrow logic would go here
        Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(shootDirection));
        arrows.Add(arrow);

        if (arrows.Count > 10)
        {
            Arrow oldest = arrows[0];
            if (oldest != null) Destroy(oldest.gameObject);

            arrows.RemoveAt(0);
        }

        arrowCount.text = $"Arrows Left: {currentArrowCount}";

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(shootDirection * (distance * 50f));
    }

    private IEnumerator FrameDelay()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        stringTransform.position = bowTransform.TransformPoint(initialLocalStringPosition);
    }

    private IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(2);
        parent.EndGame();
    }
}
