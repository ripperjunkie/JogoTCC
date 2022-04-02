using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gancho : MonoBehaviour
{
    public float velLancar;
    public float tamanhoCorda;
    public float forcaCorda;
    public float peso;

    private GameObject player;
    private Rigidbody corpoRigido;
    private SpringJoint efeitoCorda;

    private float distanciaDoPlayer;

    private bool atirarCorda;
    private bool cordaColidiu;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        corpoRigido = GetComponent<Rigidbody>();
        efeitoCorda = player.GetComponent<SpringJoint>();

        atirarCorda = true;
        cordaColidiu = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanciaDoPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            atirarCorda = false;
        }
        if (atirarCorda)
        {

        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag != "Player")
        {
            cordaColidiu = true;
        }
    }

    public void AtirarGancho()
    {
        if(distanciaDoPlayer <= tamanhoCorda)
        {
            if (!cordaColidiu)
            {
                transform.Translate(0, 0, velLancar * Time.deltaTime);
            }
            else
            {
                efeitoCorda.connectedBody = corpoRigido;
                efeitoCorda.spring = forcaCorda;
                efeitoCorda.damper = peso;
            }
        }
        if (distanciaDoPlayer > tamanhoCorda)
        {
            atirarCorda = false;
        }
    }
    public void RecolherGancho()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 25 * Time.deltaTime);
        if (distanciaDoPlayer <= 2)
        {
            Destroy(gameObject);
        }
    }
}
