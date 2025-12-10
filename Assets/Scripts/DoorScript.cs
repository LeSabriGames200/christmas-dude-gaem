using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Material DoorOpen;
    public Material DoorClose;
    public MeshRenderer DoorIn;
    public MeshRenderer DoorOut;
    public AudioClip DoorOpenSound;
    public AudioClip DoorCloseSound;
    public AudioSource audio;
    public BoxCollider Trigger;

    private bool isDoorOpen = false;
    private bool isClosing = false;

    public void Start()
    {
        
    }

    public void OnTriggerStay()
    {
        if (Input.GetMouseButton(0) && !isDoorOpen && !isClosing)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        DoorIn.material = DoorOpen;
        DoorOut.material = DoorOpen;
        Trigger.isTrigger = true;
        audio.PlayOneShot(DoorOpenSound);
        StartCoroutine(CloseDoorAfterDelay());
        isDoorOpen = true;
    }

    public IEnumerator CloseDoorAfterDelay()
    {
        isClosing = true;
        yield return new WaitForSeconds(2);

        if (isDoorOpen) // Check if the door is still open before closing it
        {
            CloseDoor();
        }

        isClosing = false;
    }

    public void CloseDoor()
    {
        DoorIn.material = DoorClose;
        DoorOut.material = DoorClose;
        Trigger.isTrigger = false;
        audio.PlayOneShot(DoorCloseSound);
        isDoorOpen = false;
    }
}
