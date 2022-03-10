using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public bool bDetectedLadder;
    public static bool bIsClimbingLadder;

    [SerializeField] private float _climbSpeed;
    [SerializeField] private float _offset = 10f;

    private Ladder _ladder;
    private Rigidbody _rb;
    private PlayerMaster _playerMaster;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _ladder = other.GetComponent<Ladder>();        
        if (_ladder)
        {
            bDetectedLadder = true;
        }

        if(other.gameObject.CompareTag("TopLadder"))
        {
            StopClimbing();
        }

        if (other.gameObject.CompareTag("BottomLadder") && bIsClimbingLadder)
        {
            StopClimbing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _ladder = other.GetComponent<Ladder>();
        if (_ladder)
        {
            bDetectedLadder = false;
        }
    }


    private void Update()
    {
        StartClimbing();
        ClimbMovement();
        if(Input.GetButtonDown("Jump") && bIsClimbingLadder)
        {
            StopClimbing();
        }
    }

    private void ClimbMovement()
    {
        if(bIsClimbingLadder)
        {
            float verticalAxis = Input.GetAxisRaw("Vertical");
            _rb.velocity = Vector3.up * _climbSpeed * verticalAxis;
            _playerMaster.SetClimbing();
        }
    }

    private void StartClimbing()
    {
        if (!_ladder || bIsClimbingLadder) return;
        if(/*Input.GetKey(KeyCode.D)*/ true)
        {
            if (bDetectedLadder)
            {
                bIsClimbingLadder = true;
                transform.rotation = Quaternion.LookRotation(_ladder.transform.right, Vector3.up);
                transform.position = new Vector3(_ladder.bottomLadderTransform.position.x, transform.position.y, _ladder.bottomLadderTransform.position.z);
                _rb.useGravity = false;
            }
        }
    }



    private void StopClimbing()
    {
        bIsClimbingLadder = false;
        _rb.useGravity = true;
        _playerMaster.ResetMovementState();
    }
}
