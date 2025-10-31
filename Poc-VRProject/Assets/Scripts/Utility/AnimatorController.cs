using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBool(bool value)
    {
        animator.SetBool("ButtonHover", value);
    }
}
