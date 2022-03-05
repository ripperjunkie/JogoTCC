using UnityEngine;

public class TargetToFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [SerializeField] private float _minHeight = 20f;

    private void Start()
    {
        transform.position = target.position;
    }
    private void Update()
    {
        Vector3 finalPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = finalPos;
    }

    public void UpdateHeight()
    {
        Vector3 finalPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = finalPos;
    }
}
