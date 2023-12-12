using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiParent : MonoBehaviour
{
    public Animator RigAnimator;

    public void Start()
    {
        RigAnimator.Play("umarm");
    }

}
