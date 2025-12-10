using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public GameController gc;
    public Transform entrance;

    public void ExitDown()
    {
        entrance.position = new Vector3(entrance.position.x, -10, entrance.position.z);
    }

    public void ExitTop()
    {
        entrance.position = new Vector3(entrance.position.x, 0, entrance.position.z);
    }
}
