
using UnityEngine;
using System;


public class SwingMovement : MonoBehaviour
{
    public Transform target;
    private Rigidbody _rigidbody;
    public float armLength;
    public Vector3 swingDir;
    public float swingForce;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        swingDir = Vector3.Cross(Vector3.down, Vector3.Normalize(transform.position - target.position));

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _rigidbody.AddForce(swingDir);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target.position);
    }
}
