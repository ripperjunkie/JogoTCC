using UnityEngine;

public class Rappel : MonoBehaviour
{
    [SerializeField] private float _verticalSpeed = 10f;
    [SerializeField] private float _ropeLength;
    private Swing _swing;
    private PlayerMaster _playerMaster;

    private void Awake()
    {
        _swing = GetComponent<Swing>();
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        RopeControl();
    }

    private void RopeControl()
    {
        if (!_swing || _playerMaster.movementState != EMovementState.RAPPEL) return;
        float verticalAxis = Input.GetAxisRaw("Vertical");      


        if (verticalAxis != 0f)
        {
            _ropeLength -= (Time.deltaTime * verticalAxis) * _verticalSpeed;
        }
        else
        {
            if(_swing.configurableJoint)
                _ropeLength = _swing.configurableJoint.anchor.y;
        }

        if (_swing.configurableJoint)
        {
            _swing.configurableJoint.anchor = new Vector3(_swing.configurableJoint.anchor.x, _ropeLength, _swing.configurableJoint.anchor.z);
        }
    }
}
