using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;

    [SerializeField] private float _minDistance = 2f;


    [ContextMenu("StartMove")]
    public void StartMove()
    {
        StartCoroutine(StartMoveCoroutine());
    }

    public IEnumerator StartMoveCoroutine()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

            yield return null;
        }
    }

}
