using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class SkeeBall : MonoBehaviour
{
    [SerializeField] HandGrabInteractable grabbableString;
 
    private void Awake()
    {
        if (grabbableString == null) return;
 
        grabbableString.WhenSelectingInteractorRemoved.Action += OnGrabEnd;
    }
 
    private void OnDestroy()
    {
        if (grabbableString == null) return;
 
        grabbableString.WhenSelectingInteractorRemoved.Action -= OnGrabEnd;
    }
 
    private void OnGrabEnd(IInteractor interactor)
    {
        SkeeBallMachine.Instance.RemoveBall();

        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Points>(out Points points))
        {
            SkeeBallMachine.Instance.AddTotalPoints(points.GetPoints());
            print(points.GetPoints());
        }
    }
}
