using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    

    private int trDoorOpen = Animator.StringToHash("DoorOpen");
    private int trDoorClose = Animator.StringToHash("DoorClose");
    private Animator animator;
    private AudioSource audioSource;
    public bool IsOpen = false;
    public bool IsClose = true;
	void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
    

    public void openDoor() {

            audioSource.Play();
            animator.SetTrigger(trDoorOpen);
            IsOpen = true;
            IsClose = false;

    }
    public void closeDoor()
    {
        IsClose = true;
        audioSource.Play();
        animator.SetTrigger(trDoorClose);
        IsOpen = false;
    }
    //bool IsTargetNear()
    //{
    //    var distanceDelta = distanceActivator.position - activator.position;
    //    if (distanceDelta.sqrMagnitude < distance)
    //    {


    //        return true;

    //    }
    //    return false;
    //}

}
