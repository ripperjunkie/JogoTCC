using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScriptCharacter : MonoBehaviour
{
    [SerializeField]Rigidbody _ChRb;
    [SerializeField] AudioSource _walk;
    [SerializeField] CharacterMovement _mv;
    //[SerializeField] AudioClip walkClip;
    bool antInfiniteRepeat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((_ChRb.velocity.x != 0 || _ChRb.velocity.z != 0) && _mv.HitGround())
        {
            if (!antInfiniteRepeat)
            _walk.Play();
            _walk.loop = true;
            //Debug.Log("Valor de X"+ChRb.velocity.x);
            //Debug.Log("Valor do Z"+ChRb.velocity.z);
            antInfiniteRepeat = true;
        }
        else if(_ChRb.velocity.x == 0  && _ChRb.velocity.z == 0 || !_mv.HitGround())
        {
            _walk.Stop();
            _walk.loop = false;
            antInfiniteRepeat = false;
        }
    }
}
