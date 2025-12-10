using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearExitTrigger : MonoBehaviour
{
    public Entrance entrance;
    public GameController gc;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.activateFinaleMode && gc.exitReached < 3)
        {
            entrance.ExitDown();
            gc.exitReached++;
        }
    }
}
