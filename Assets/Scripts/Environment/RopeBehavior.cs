using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour
{
    public int nodeAmount;
    public float distanceBetweenNodes;
    public float lastNodeMass = 10f;
    public Rigidbody lastRb;
    private Transform _node;
    private void Start()
    {
        _node = transform.GetChild(0);
        Vector3 position = transform.position;
        lastRb = _node.GetComponent<Rigidbody>();
        for (int i = 1; i < nodeAmount; i++)
        {
            position.y -= distanceBetweenNodes;
            GameObject newNode = Instantiate(_node.gameObject, position, transform.rotation, transform);
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
