using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{

    [Header("Objects Assignments")]
    [SerializeField] private Camera _camera;

    [Header("Movement Params")]
    public float movSpeed = 20f;
    [SerializeField] private float _rotateSpeed = 20f;
    [SerializeField] private float groundSphereRadius = 2f;
    [SerializeField] private LayerMask groudLayerMask;
    [SerializeField] private LayerMask brigeLayerMask;

    [Header("Debug")]
    [SerializeField] private bool bDebugGizmos;

    private Rigidbody _rigidbody;

    private float _turnSmoothVelocity;
    [SerializeField] private float swingForce = 10f;
    [SerializeField] private float lerpSwingRopeTime = 10f;

    [SerializeField] private bool activeSwing = true;
    [SerializeField] private bool rotationTest = false;

    [SerializeField] private Rigidbody swingRb;
    [SerializeField] private float desiredRopeLength;
    private float currentRopeLength; //we'll lerp the rope length
    private bool toggleRope;
    private ConfigurableJoint joint;
    private LineRenderer _yoyoRopeRenderer;
    private PlayerMaster _playerMaster;
    

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
        _playerMaster = GetComponent<PlayerMaster>();
        _yoyoRopeRenderer = GetComponent<LineRenderer>();
        _yoyoRopeRenderer.positionCount = 2;

    }

    private void Update()
    {
        if(rotationTest)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        Movement();
       
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        if(_rigidbody && activeSwing)
        {
            Vector3 force = new Vector3(horizontalAxis * swingForce, 0f, 0f);
            if(force.magnitude != 0.0f)
            _rigidbody.AddForce(force);
        }
        if (activeSwing)
        {
           //currentRopeLength = Mathf.Lerp(currentRopeLength, desiredRopeLength, Time.deltaTime * lerpSwingRopeTime);
           // joint.anchor = new Vector3(0f, currentRopeLength, 0f);

            _yoyoRopeRenderer.enabled = true;
            _yoyoRopeRenderer.SetPosition(0, transform.position);
            _yoyoRopeRenderer.SetPosition(1, swingRb.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !toggleRope)
        {

            float distance = Vector3.Distance(transform.position, swingRb.transform.position);
            currentRopeLength = distance;
            activeSwing = true;
            joint = gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedBody = swingRb;
            joint.anchor = new Vector3(0f, distance, 0f);
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
            toggleRope = true;
            return;


        }

        
        if (!activeSwing)
        {
           // GetComponent<ConfigurableJoint>().breakForce = 0f;
            //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            //_rigidbody.freezeRotation = true;
            //GetComponent<ConfigurableJoint>().connectedBody = null;
        }
        if(Input.GetKeyDown(KeyCode.Space) && activeSwing)
        {
            if(activeSwing)
            {
                activeSwing = false;
                GetComponent<ConfigurableJoint>().breakForce = 0f;
                _yoyoRopeRenderer.enabled = false;
                toggleRope = false;
            }
        }

    }

    private void Movement()
    {
        if (ClimbLadder.isClimbingLadder || activeSwing) return;
        if(_rigidbody)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(horizontalAxis, 0f, verticalAxis);

            if(dir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotateSpeed);

                if(_playerMaster.movementState != EMovementState.INAIR)
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
                Vector3 movVector = movDir * movSpeed;
                
                _rigidbody.velocity = new Vector3(movVector.x, movVector.y + _rigidbody.velocity.y, movVector.z);
            }
            else
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            }
        }
    }


    public bool HitGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.CheckSphere(transform.position, groundSphereRadius, groudLayerMask);
    }
    public bool HitBrige()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.CheckSphere(transform.position, groundSphereRadius, brigeLayerMask);
    }

    private void OnDrawGizmos()
    {
        if (!bDebugGizmos) return;
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, groundSphereRadius);
    }

}
