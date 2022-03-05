using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    [Tooltip("Target to follow")]
    public GameObject target;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _sphereDebugRadius;
    [SerializeField] private GameObject _camGO;
    [SerializeField] private GameObject _targetToLookAt;
    private Camera _camera;

    [SerializeField] private float _yawAngle;
    [SerializeField] private float _pichAngle;
    [SerializeField] private float _camDistance;
    [SerializeField] private float _camControlSpeed;
    [SerializeField] private float _camSmoothControlSpeed;

    public float xClampMin;
    public float xClampMax;

    private float xAngleVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = GetComponent<Camera>();
        if(_camGO)
            _camGO = GetComponent<Camera>().gameObject;
    }

    private void Update()
    {
        FollowTarget();
        Rotation();
        CollisionDetection();
    }

    private void FixedUpdate()
    {
        
    }

    //smoothly follows target position
    private void FollowTarget()
    {
        if (!target) return;

        transform.position = target.transform.position + _offset;

        if(_camGO)
        {
            _camGO.transform.position = transform.position + transform.forward * _camDistance;
        }
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        float xAngle = transform.rotation.eulerAngles.y;
        if(mouseX != 0.1f)
            xAngle += mouseX * _camControlSpeed * Time.deltaTime;

        float yAngle = transform.rotation.eulerAngles.x;
        if (mouseY != 0.1f)
        {
            yAngle += mouseY * _camControlSpeed * Time.deltaTime * -1f;
        }

        print(transform.localEulerAngles);

        transform.localRotation = Quaternion.Euler(yAngle, xAngle, 0f);


    }

    private void CollisionDetection()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + _offset, _sphereDebugRadius);
    }

}
