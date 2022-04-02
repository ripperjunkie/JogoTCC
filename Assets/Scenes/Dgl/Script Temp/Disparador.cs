using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    public GameObject gancho;
    private GameObject auxGancho;

    public Camera m_Camera;

    public Transform dirDoClique;
    private Transform auxDirDoClique;

    private Vector3 localDoClique;
    private Vector3 posMouse;
    private Quaternion olharParaDir;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posMouse = Input.mousePosition;
        posMouse.z = Vector3.Distance(m_Camera.transform.position, transform.position);
        posMouse = m_Camera.ScreenToWorldPoint(posMouse);

        if (auxGancho == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                auxDirDoClique = Instantiate(dirDoClique, posMouse, Quaternion.identity) as Transform;
                localDoClique = (auxDirDoClique.transform.position - transform.position).normalized;
                olharParaDir = Quaternion.LookRotation(localDoClique);

                auxGancho = Instantiate(gancho, transform.position, olharParaDir) as GameObject;
                Destroy(auxDirDoClique.gameObject);
            }
        }

    }
}
