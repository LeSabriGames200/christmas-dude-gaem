using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Transform Camera;
    Quaternion Rotation;

    // Start is called before the first frame update
    void Start()
    {
        Rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.rotation * Rotation;
    }
}
