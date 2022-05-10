using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectsDetection : MonoBehaviour
{
    [SerializeField] private MoveObjects _moveObjects;
    private PlayerMaster _master;

    private void Awake()
    {
        _master = GetComponentInParent<PlayerMaster>();
    }

    void OnTriggerStay(Collider col)
    {
        MoveObjects other = col.GetComponent<MoveObjects>();  

        if (other && !_moveObjects && _master.movementState != EMovementState.PUSHING)
        {
            _moveObjects = other;
        }
    }
    void OnTriggerExit(Collider col)
    {
        MoveObjects other = col.GetComponent<MoveObjects>();

        if (other && _moveObjects && _master.movementState != EMovementState.PUSHING)
        {
            _moveObjects = null;
        }
    }

    private void Update()
    {
        TryMovingBox();
    }

    private void TryMovingBox()
    {
        if(!_moveObjects)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _moveObjects.active = !_moveObjects.active;

            if (_moveObjects.active)
            {
                _master.movementState = EMovementState.PUSHING;
                return;
            }

            _master.ResetMovementState();
        }
    }

}
