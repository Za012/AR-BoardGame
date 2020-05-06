using UnityEngine;

public class GooseAnimator : MonoBehaviour
{
    private Animator animator;
    private bool isWalking = false;
    private bool isWiggle = false;
    private bool isTurning = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ToggleWalk()
    {
        isWalking = !isWalking;
        animator.SetBool("isWalking", isWalking);
    }
}
