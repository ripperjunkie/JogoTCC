using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pendulum
{
    public Transform bobTransform;
    public Tether tether;
    public Arm arm;
    public Bob bob;

    public void Initialize()
    {
        bobTransform.transform.parent = tether.tetherTransform.transform;
        arm.length = Vector3.Distance(bobTransform.transform.localPosition, tether.position);
    }

    public Vector3 MoveBob(Vector3 pos, float time)
    {
        //bob.ApplyGravity();
        pos += bob.velocity * time;
        return pos;
    }

}
