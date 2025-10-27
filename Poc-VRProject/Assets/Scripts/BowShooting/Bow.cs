using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] 
    private Transform bowTransform;
    [SerializeField] 
    private Transform stringTransform;
    [SerializeField] 
    private float maxDrawDistance = 0.5f;
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private HandGrabInteractable grabbableString;

    private Vector3 initialLocalStringPosition;
    private bool isGrabbed = false;

    private void Awake()
    {
        if (grabbableString == null) return;

        grabbableString.WhenSelectingInteractorAdded.Action += OnGrabBegin;
        grabbableString.WhenSelectingInteractorRemoved.Action += OnGrabEnd;
    }

    private void OnDestroy()
    {
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

    [ContextMenu("Test Grab Begin")]
    private void TestGrabBegin() => isGrabbed = true;

    [ContextMenu("Test Grab End")]
    private void TestGrabEnd()
    {
        isGrabbed = false;
        FireArrow();
    }

    private void Start()
    {
        initialLocalStringPosition = bowTransform.InverseTransformPoint(stringTransform.position);
    }

    private void Update()
    {
        if (isGrabbed) 
        {
            Vector3 bowToString = stringTransform.position - bowTransform.position;
            float distance = bowToString.magnitude;

            if (distance > maxDrawDistance)
                stringTransform.position = bowTransform.position + bowToString.normalized * maxDrawDistance;
        }
    }

    private void FireArrow()
    {
        float distance = Vector3.Distance(stringTransform.position, bowTransform.position);

        // fire arrow logic would go here
        GameObject gameObject = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Vector3 shootDirection = (bowTransform.position - stringTransform.position).normalized;
        float shootForce = distance * 100f;
        rb.AddForce(shootDirection * shootForce);


        stringTransform.position = bowTransform.TransformPoint(initialLocalStringPosition);
    }

}
