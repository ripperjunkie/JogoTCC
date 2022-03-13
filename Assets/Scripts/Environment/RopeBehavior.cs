using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour
{
    public int nodeAmount;
    public float distanceBetweenNodes;
    public float lastNodeMass = 10f;
    private Transform node;

    private void Start()
    {
        node = transform.GetChild(0);
        Vector3 position = transform.position;
        Rigidbody lastRb = node.GetComponent<Rigidbody>();
        for (int i = 1; i < nodeAmount; i++)
        {
            position.y -= distanceBetweenNodes;
            GameObject newNode = Instantiate(node.gameObject, position, transform.rotation, transform);
            newNode.transform.SetParent(transform);

            FixedJoint fixedJoint = newNode.GetComponent<FixedJoint>();            
            if(fixedJoint)
            {
                fixedJoint.connectedBody = lastRb;
                lastRb = newNode.GetComponent<Rigidbody>();
            }
        }

        lastRb.GetComponent<FixedJoint>().massScale = lastNodeMass;
        
        
    }

    private void Update()
    {
        
    }

}
