using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public Transform cameratranform;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameratranform);
    }
}
