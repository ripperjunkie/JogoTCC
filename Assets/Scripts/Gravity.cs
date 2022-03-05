using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Gravity : MonoBehaviour
{
    [SerializeField] private float _gravityStrength;

    private CharacterController _charController;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(_charController && !_charController.isGrounded)
        {
            float newYPos = _charController.transform.position.y;
            _charController.Move(new Vector3(0f, newYPos, 0f));
        }
    }




}
