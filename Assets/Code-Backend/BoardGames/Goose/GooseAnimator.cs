using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {
        if ((Input.touchCount > 0))
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    isWiggle = !isWiggle;
        //    animator.SetBool("isWiggle", isWiggle);
        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    isTurning = !isTurning;
        //    animator.SetBool("isTurning", isTurning);
        //}
    }
}
