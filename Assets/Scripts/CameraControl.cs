using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private TargetToFollow _targetToFollow;

    private void Start()
    {
        _targetToFollow = GameObject.FindObjectOfType<TargetToFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("UpdateCamera"))
            _targetToFollow.UpdateHeight();
    }

}