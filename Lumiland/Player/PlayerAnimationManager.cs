using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    //private bool isShotMode;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void ShotModeON()
    {
        animator.SetBool("isShotMode", true);
        //isShotMode = true;
    }

    public void ShotModeOFF()
    {
        animator.SetBool("isShotMode", false);
        //isShotMode = false;
    }

}
