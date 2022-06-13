using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    public ScreamShot screamShot;
    public GameObject canvasImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            screamShot.TakeShot();
            if (canvasImage)
            {
                canvasImage.SetActive(true);
            }
            Destroy(this);
        }
    }

    IEnumerator OpenAndClose()
    {
        GetComponent<Collider>().enabled = false;
        canvasImage.transform.LeanMove(Vector3.zero, 1.5f).setEaseInOutQuart();//setOnComplete(CanvasOff);
        yield return new WaitForSeconds(1f);
    }
}
