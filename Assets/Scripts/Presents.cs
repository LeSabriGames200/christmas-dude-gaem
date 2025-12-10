using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presents : MonoBehaviour
{

    public GameController gc;
    public GameObject present;
    
    public void OnTriggerStay()
    {
        if (Input.GetMouseButton(0))
        {
            present.SetActive(false);
            gc.CollectPresent();
        }
    }
}
