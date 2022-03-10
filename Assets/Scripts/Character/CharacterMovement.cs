using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{

    [Header("Objects Assignments")]
    [SerializeField] private Camera _camera;

    [Header("Movement Params")]
    [SerializeField] private float _movSpeed = 20f;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private float _rotateSpeed = 20f;
    [SerializeField] private float groundSphereRadius = 2f;
    [SerializeField] private LayerMask groudLayerMask;

    [Header("Debug")]
    [SerializeField] private bool bDebugGizmos;

    private Rigidbody _rigidbody;

    private float _turnSmoothVelocity;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        Jump();

    }

    private void Movement()
    {
        if (ClimbLadder.bIsClimbingLadder) return;
        if(_rigidbody)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(horizontalAxis, 0f, verticalAxis);

            if(dir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotateSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
                Vector3 movVector = movDir * _movSpeed;
                
                _rigidbody.velocity = new Vector3(movVector.x, movVector.y + _rigidbody.velocity.y, movVector.z);
            }
            else
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            }
        }
    }

    [ContextMenu("Jump")]
    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && HitGround())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);            
        }
    }

    private void StartSprint()
    {
        print("StartSprint");
    }

    private void StopSprint()
    {
        print("StopSprint");
    }

    private void StartCrouch()
    {
        print("StartCrouch");
    }

    private void StopCrouch()
    {
        print("StopCrouch");
    }

    public bool HitGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.CheckSphere(transform.position, groundSphereRadius, groudLayerMask);
    }

    private void OnDrawGizmos()
    {
        if (!bDebugGizmos) return;
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, groundSphereRadius);
    }

}
