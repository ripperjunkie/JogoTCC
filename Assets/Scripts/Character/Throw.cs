using UnityEngine;
using UnityEngine.Events;

public class Throw : MonoBehaviour
{
    public GameObject ropePrefab;
    private PlayerMaster _playerMaster;
    private Animator _animator;
    private RopePoint _ropePoint;
    private bool _canThrow;
    private bool _holdingRope;
    private GameObject newNode;
    [SerializeField] private float force = 100f;
    private Rigidbody _rb;
    private CapsuleCollider _capsuleCollider;
    

    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }


    private void Update()
    {
        TryThrow();
        if(_holdingRope)
        {
            RopeBehavior rope = newNode.GetComponent<RopeBehavior>();
            transform.position = rope.lastRb.transform.position;
        }
        RopeSwing();

        if(_holdingRope && Input.GetKeyUp(KeyCode.Mouse0))
        {
            _holdingRope = false;
            DetachToRope();
            Destroy(newNode);
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        RopePoint ropePoint = other.GetComponent<RopePoint>();
        if (ropePoint)
        {
            _canThrow = true;
            _ropePoint = ropePoint;
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
        if (!_playerMaster || !_animator || !_ropePoint) return;
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_playerMaster.GetIsYoyoActive && _canThrow )
            {
                _animator.SetTrigger("Throw");

                //Spawn rope
                newNode = Instantiate(ropePrefab, _ropePoint.tether.transform.position, transform.rotation);
                newNode.GetComponent<RopeBehavior>().player = this;
                AttachToRope();
            }
        }


    }

    public void AttachToRope()
    {
        _holdingRope = true;

        if (_capsuleCollider)
        {
            _capsuleCollider.enabled = false;
        }

        if (_rb)
        {
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
        }
    }

    public void DetachToRope()
    {
        _holdingRope = false;
        if (_rb)
        {
            _rb.useGravity = true;
        }
        if (_capsuleCollider)
        {
            _capsuleCollider.enabled = true;
        }
    }

    public void RopeSwing()
    {
        if(_holdingRope && newNode)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(horizontalAxis, 0f, verticalAxis);

            if (dir.magnitude >= 0.1f)
            {
               newNode.GetComponent<RopeBehavior>().Swing(transform.forward * force + (-transform.up * 10f));
            }
        }
    }

}
