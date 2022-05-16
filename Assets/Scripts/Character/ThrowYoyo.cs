using UnityEngine;

public class ThrowYoyo : MonoBehaviour
{
    [SerializeField] private bool _debugCollider;
    private GameObject _objectToInteract;
    private Animator _animator;
    private MeshRenderer _meshRenderer;
    private PlayerMaster _playerMaster;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _animator = GetComponentInParent<Animator>();
        _playerMaster = GetComponentInParent<PlayerMaster>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interaction"))
        {
            Throw();
        }

        if(_meshRenderer)
            GetComponent<MeshRenderer>().enabled = _debugCollider;
    }

    public void Throw()
    {
        if (!_playerMaster) return;

        if(_playerMaster.GetIsYoyoActive)
        {
            if (_objectToInteract)
            {
                _objectToInteract.GetComponent<IInteractable<PlayerMaster>>().OnInteract(_playerMaster);
                if (_animator)
                {
                    _animator.SetTrigger("Throw");
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<IInteractable<PlayerMaster>>() != null)
        {
            _objectToInteract = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<IInteractable<PlayerMaster>>() != null)
        {
            _objectToInteract = null;
        }
    }


}
