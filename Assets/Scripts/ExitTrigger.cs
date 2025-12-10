using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public GameController gc;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.activateFinaleMode)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
