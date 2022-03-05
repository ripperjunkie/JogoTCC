using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bob
{
    public Vector3 velocity;
    public float gravity;
    public Vector3 gravityDir = new Vector3(0f, 1f, 0f);

    public void ApplyGravity()
    {
        velocity -= gravityDir * gravity * Time.deltaTime;
    }

}
