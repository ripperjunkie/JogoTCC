using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    public float _velocidade;
    public float _xMaxDistance;
    public float _xMinDistance;

    public bool indo;

    void Update()
    {
        if (transform.position.x < _xMinDistance)
        {
            indo = true;

        }
        if (transform.position.x > _xMaxDistance)
        {
            indo = false;

        }
        if (indo)
        {
            transform.Translate(_velocidade * Time.deltaTime, 0, 0);
        }
        if (!indo)
        {
            transform.Translate(-_velocidade * Time.deltaTime, 0, 0);
        }
    }
}
