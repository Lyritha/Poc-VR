using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnStateIdle;
    public UnityEvent OnStateHover;
    public UnityEvent OnStateSelect;
    public UnityEvent OnStateDisabled;

    [SerializeField, Interface(typeof(IInteractableView))]
    private Object _interactableView;

    private IInteractableView InteractableView;

    protected virtual void Awake() => InteractableView = _interactableView as IInteractableView;

    private void OnEnable() => InteractableView.WhenStateChanged += UpdateState;
    private void OnDisable() => InteractableView.WhenStateChanged -= UpdateState;

    private void UpdateState(InteractableStateChangeArgs args)
    {
        switch (InteractableView.State)
        {
            case InteractableState.Normal:
                OnStateIdle?.Invoke();
                break;
            case InteractableState.Hover:
                OnStateHover?.Invoke();
                Debug.Log("Interactable State: Hover");
                break;
            case InteractableState.Select:
                OnStateSelect?.Invoke();
                Debug.Log("Interactable State: Select");
                break;
            case InteractableState.Disabled:
                OnStateDisabled?.Invoke();
                break;
        }
    }

    [ContextMenu("Fake select")]
    public void TestButton()
    {
        OnStateSelect?.Invoke();
    }
}
