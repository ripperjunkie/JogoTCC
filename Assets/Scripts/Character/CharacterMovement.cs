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
    [SerializeField] private bool debugParams;


    private float _turnSmoothVelocity;
    private Rigidbody _rigidbody;
    private PlayerMaster _playerMaster;    

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
        _playerMaster = GetComponent<PlayerMaster>();

    }

    private void Update()
    {
        Movement();
        if(debugParams)
        {
            print("Hit ground? " + HitGround());
            print("Hit bridge? " + HitBrige());
        }
        

    }



    private void Movement()
    {
        if (ClimbLadder.isClimbingLadder || _playerMaster.movementState == EMovementState.SWINGING) return;
        if(_rigidbody)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(horizontalAxis, 0f, verticalAxis);

            if(dir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotateSpeed);

                if(_playerMaster.movementState != EMovementState.SWINGING)
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
        if (!debugParams) return;
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, groundSphereRadius);
    }

}
