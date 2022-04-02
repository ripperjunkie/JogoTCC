using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riding : MonoBehaviour
{
    private PlayerMaster playerMaster;
    private Rigidbody rigidbody;
    public MovingBox movingBox;
    public float forceImpulse;
    private void Start()
    {
        playerMaster = GetComponent<PlayerMaster>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //GetRiding();
    }

    private void GetRiding()
    {
        if(playerMaster.GetIsYoyoActive && Input.GetMouseButtonDown(0))
        {
            rigidbody.AddForce(transform.up * forceImpulse, ForceMode.Impulse);
            transform.parent = movingBox.transform;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //rigidbody.AddForce(transform.up * forceImpulse);
            transform.parent = null;
        }
    }
}
