using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPointType
{
    NONE,
    SWING,
    RAPPEl
}


public class RopePoint : MonoBehaviour
{
    public Transform tether;
    public Rigidbody rb;
    public EPointType pointType;
}
