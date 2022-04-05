using UnityEngine;

public class Swing : MonoBehaviour
{
    public bool holdingRope;
    public ConfigurableJoint configurableJoint;

    [SerializeField] private float swingForce = 100f;
    [SerializeField] private float lerpSwingRopeTime = 10f;
    [SerializeField] private float desiredRopeLength;
    [SerializeField] private float offsetRenderRope;
    [SerializeField] private float airSpeed;
    [SerializeField] private bool _debug;


    private PlayerMaster _playerMaster;
    private Animator _animator;
    private RopePoint _ropePoint;
    private Vector3 _ropePointLocation;
    private Rigidbody _rb;
    private bool _canThrow;
    private float currentRopeLength; //we'll lerp the rope length
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
        RopeControl();
        OnRope();
        if(Input.GetButtonDown("Jump"))
        {
            DetachRope();
        }

        InAirControl();
        if(_debug)
        {
            print("_canThrow: " + _canThrow);
            print("holdingRope: " + holdingRope);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {        
        RopePoint ropePoint = other.GetComponent<RopePoint>();
        if (ropePoint && ropePoint.pointType != EPointType.NONE)
        {
            _canThrow = true;
            _ropePoint = ropePoint;
            _ropePointLocation = ropePoint.rb.transform.position;
        }

        ExitRappel exitRappel = other.GetComponent<ExitRappel>();
        if(exitRappel)
        {
            DetachRope();
            if(exitRappel.teleportPoint)
                transform.position = exitRappel.teleportPoint.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RopePoint ropePoint = other.GetComponent<RopePoint>();
        if (ropePoint && ropePoint.pointType != EPointType.NONE)
        {
            _canThrow = false;
            _ropePoint = null;
        }
    }

    public void TryThrow()
    {
        if (!_playerMaster || !_animator || !_ropePoint || holdingRope) return;
        if(Input.GetKeyDown(KeyCode.Mouse0) && _playerMaster.movementState == EMovementState.INAIR)
        {
            if (_playerMaster.GetIsYoyoActive && _canThrow)
            {
                _animator.SetTrigger("Throw");
                
                holdingRope = true;

                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                _rb.freezeRotation = false;
                float distance = Vector3.Distance(transform.position, _ropePoint.rb.transform.position);
                currentRopeLength = distance;
                configurableJoint = gameObject.AddComponent<ConfigurableJoint>();
                configurableJoint.autoConfigureConnectedAnchor = false;
                configurableJoint.connectedBody = _ropePoint.rb;
                configurableJoint.anchor = new Vector3(0f, 5f, 0f);
                configurableJoint.xMotion = ConfigurableJointMotion.Limited;
                configurableJoint.yMotion = ConfigurableJointMotion.Limited;
                configurableJoint.zMotion = ConfigurableJointMotion.Limited;

                switch(_ropePoint.pointType)
                {
                    case EPointType.SWING:
                        {
                            _playerMaster.movementState = EMovementState.SWINGING;
                            configurableJoint.angularXMotion = ConfigurableJointMotion.Free;
                            configurableJoint.angularYMotion = ConfigurableJointMotion.Limited;
                            configurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
                        }


                        break;
                    case EPointType.RAPPEl:
                        {
                            _playerMaster.movementState = EMovementState.RAPPEL;
                            configurableJoint.angularXMotion = ConfigurableJointMotion.Locked;
                            configurableJoint.angularYMotion = ConfigurableJointMotion.Locked;
                            configurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
                        }

                        break;
                }


                SoftJointLimit softX = new SoftJointLimit();
                softX.limit = -86f;
                configurableJoint.lowAngularXLimit = softX;
                SoftJointLimit softY = new SoftJointLimit();
                softY.limit = 176f;
                configurableJoint.highAngularXLimit = softY;
                configurableJoint.enableCollision = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Reset rotation
        if(collision.gameObject.layer == 3 /* 3 = ground */)
        {
            if (_playerMaster.movementState == EMovementState.SWINGING || _playerMaster.movementState == EMovementState.RAPPEL)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _rb.freezeRotation = true;
                _playerMaster.movementState = EMovementState.NONE;
            }
        }

    }

    public void DetachRope()
    {
        if (holdingRope)
        {
            holdingRope = false;
            if(GetComponent<ConfigurableJoint>())
            {
                Destroy(GetComponent<ConfigurableJoint>());
            }            
            _yoyoRopeRenderer.enabled = false;
            _rb.freezeRotation = true;
        }
        if(_playerMaster.movementState == EMovementState.RAPPEL)
        {
            _playerMaster.ResetMovementState();
        }
    }

    public void RopeControl()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (_rb && holdingRope)
        {
            Vector3 force = new Vector3(horizontalAxis * swingForce, 0f, 0f);
            if (force.magnitude != 0.0f)
                _rb.AddForce(force);
        }
    }

    public void OnRope()
    {
        if (holdingRope)
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
        if(_playerMaster.movementState == EMovementState.SWINGING && !holdingRope)
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
