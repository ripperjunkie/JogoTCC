using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private float swingForce = 100f;
    [SerializeField] private float lerpSwingRopeTime = 10f;
    [SerializeField] private float desiredRopeLength;
    [SerializeField] private float offsetRenderRope;
    [SerializeField] private float airSpeed;


    private PlayerMaster _playerMaster;
    private Animator _animator;
    private RopePoint _ropePoint;
    private Vector3 _ropePointLocation;
    private Rigidbody _rb;
    private bool _canThrow;
    private bool _holdingRope;
    private float currentRopeLength; //we'll lerp the rope length
    private ConfigurableJoint joint;
    private LineRenderer _yoyoRopeRenderer;


    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _yoyoRopeRenderer = GetComponent<LineRenderer>();
        _yoyoRopeRenderer.positionCount = 2;
    }

    private void Update()
    {
        TryThrow();
        RopeSwing();
        OnRope();
        DetachToRope();
        InAirControl();
    }

    private void OnTriggerEnter(Collider other)
    {        
        RopePoint ropePoint = other.GetComponent<RopePoint>();
        if (ropePoint)
        {
            _canThrow = true;
            _ropePoint = ropePoint;
            _ropePointLocation = ropePoint.rb.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RopePoint ropePoint = other.GetComponent<RopePoint>();
        if (ropePoint)
        {
            _canThrow = false;
            _ropePoint = null;
        }
    }

    public void TryThrow()
    {
        if (!_playerMaster || !_animator || !_ropePoint || _holdingRope) return;
        if(Input.GetKeyDown(KeyCode.Mouse0) && _playerMaster.movementState == EMovementState.INAIR)
        {
            if (_playerMaster.GetIsYoyoActive && _canThrow)
            {
                _animator.SetTrigger("Throw");
                _playerMaster.movementState = EMovementState.SWINGING;
                _holdingRope = true;

                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                _rb.freezeRotation = false;
                float distance = Vector3.Distance(transform.position, _ropePoint.rb.transform.position);
                currentRopeLength = distance;
                joint = gameObject.AddComponent<ConfigurableJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedBody = _ropePoint.rb;
                joint.anchor = new Vector3(0f, 5f, 0f);
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;

                SoftJointLimit softX = new SoftJointLimit();
                softX.limit = -86f;
                joint.lowAngularXLimit = softX;
                SoftJointLimit softY = new SoftJointLimit();
                softY.limit = 176f;
                joint.highAngularXLimit = softY;
                joint.enableCollision = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Reset rotation
        if(collision.gameObject.layer == 3 /*ground*/)
        {
            if (_playerMaster.movementState == EMovementState.SWINGING)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _rb.freezeRotation = true;
                _playerMaster.movementState = EMovementState.NONE;
            }
        }

    }

    public void DetachToRope()
    {
        if (_holdingRope && Input.GetKeyUp(KeyCode.Mouse0))
        {
            _holdingRope = false;
            GetComponent<ConfigurableJoint>().breakForce = 0f;
            _yoyoRopeRenderer.enabled = false;
        }
    }

    public void RopeSwing()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (_rb && _holdingRope)
        {
            Vector3 force = new Vector3(horizontalAxis * swingForce, 0f, 0f);
            if (force.magnitude != 0.0f)
                _rb.AddForce(force);
        }
    }

    public void OnRope()
    {
        if (_holdingRope)
        {
           // currentRopeLength = Mathf.Lerp(currentRopeLength, desiredRopeLength, Time.deltaTime * lerpSwingRopeTime);
           // joint.anchor = new Vector3(0f, currentRopeLength, 0f);
            _rb.freezeRotation = false;

            if(_yoyoRopeRenderer)
            {
                _yoyoRopeRenderer.enabled = true;
                _yoyoRopeRenderer.SetPosition(0, transform.position + transform.up * offsetRenderRope);
                _yoyoRopeRenderer.SetPosition(1, _ropePointLocation);
            }

        }
    }

    public void InAirControl()
    {
        if(_playerMaster.movementState == EMovementState.SWINGING && !_holdingRope)
        {
            if (_rb)
            {
                float horizontalAxis = Input.GetAxisRaw("Horizontal");
                float verticalAxis = Input.GetAxisRaw("Vertical");

                Vector3 dir = new Vector3(horizontalAxis, 0f, verticalAxis);

                if (dir.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

                    Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    Vector3 movVector = movDir * airSpeed;

                    _rb.velocity = new Vector3(movVector.x, movVector.y + _rb.velocity.y, movVector.z);
                }
            }
        }
    }

}
