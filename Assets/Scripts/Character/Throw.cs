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

    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        TryThrow();
        if(_holdingRope && newNode)
        {
            //Rigidbody rb = newNode.GetComponent<RopeBehavior>().lastRb;
            //if(rb)
            //{
            //    rb.AddForce((transform.position - rb.transform.position) * force);
            //}
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
                SpawnRope();
                _holdingRope = true;


                //Disable collision
                //Add fixed joint
                //Get last point and attach to fixed joint rigidbody
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            _holdingRope = false;
            Destroy(newNode);
        }

    }

    public void SpawnRope()
    {
        if (!_ropePoint) return;
        print("SpawnRope");
        newNode = Instantiate(ropePrefab, _ropePoint.tether.transform.position, transform.rotation);

        //FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        //Rigidbody rb = newNode.GetComponent<RopeBehavior>().lastRb;
        //if (joint && rb)
        //{
        //    joint.connectedBody = rb;
        //}
    }
}
