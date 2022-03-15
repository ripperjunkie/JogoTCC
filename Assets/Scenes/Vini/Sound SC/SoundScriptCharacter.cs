using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScriptCharacter : MonoBehaviour
{
    [SerializeField]Rigidbody ChRb;
    [SerializeField] AudioSource walk;
    [SerializeField] CharacterMovement mv;
    //[SerializeField] AudioClip walkClip;
    bool antInfiniteRepeat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((ChRb.velocity.x != 0 || ChRb.velocity.z != 0) && mv.HitGround())
        {
            if (!antInfiniteRepeat)
            walk.Play();
            walk.loop = true;
            //Debug.Log("Valor de X"+ChRb.velocity.x);
            //Debug.Log("Valor do Z"+ChRb.velocity.z);
            antInfiniteRepeat = true;
        }
        else if(ChRb.velocity.x == 0  && ChRb.velocity.z == 0 || !mv.HitGround())
        {
            walk.Stop();
            walk.loop = false;
            antInfiniteRepeat = false;
        }
    }
}
