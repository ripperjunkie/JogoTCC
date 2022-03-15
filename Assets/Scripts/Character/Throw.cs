using UnityEngine;
using UnityEngine.Events;

public class Throw : MonoBehaviour
{
    private PlayerMaster _playerMaster;
    private Animator _animator;
    private bool _canThrow;
    public GameObject ropePrefab;
    private RopePoint _ropePoint;

    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        TryThrow();
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
                //Disable collision
                //Add fixed joint
                //Get last point and attach to fixed joint rigidbody
            }
        }

    }

    public void SpawnRope()
    {
        if (!_ropePoint) return;
        print("SpawnRope");
        GameObject newNode = Instantiate(ropePrefab, _ropePoint.tether.transform.position, transform.rotation);
    }
}
