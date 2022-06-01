using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScreamShot : MonoBehaviour
{
    public Image image;
    public bool takeScreenShotOnNextFrame;
    public Camera myCamera;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;
            Texture2D fotoTex = null;

            Texture2D rendeResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);

            rendeResult.ReadPixels(rect, 0, 0);
            byte[] bytes = rendeResult.EncodeToPNG();

            Debug.Log("foto salva");

            fotoTex = new Texture2D(10, 10);
            fotoTex.LoadImage(bytes);


                image.gameObject.SetActive(true);
                image.sprite = Sprite.Create(fotoTex, new Rect(0.0f, 0.0f, fotoTex.width, fotoTex.height), new Vector2(0.5f, 0.5f), 100.0f);

          
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
            //Debug.Log("gravou");

        }
    }

    public void TakeShot()
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);
        takeScreenShotOnNextFrame = true;
        //Debug.Log("bateu foto");
        StartCoroutine(RecordFrame());
    }

    private IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame();
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;
            Texture2D fotoTex = null;

            Texture2D rendeResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);

            rendeResult.ReadPixels(rect, 0, 0);
            byte[] bytes = rendeResult.EncodeToPNG();

            //Debug.Log("foto salva");

            fotoTex = new Texture2D(10, 10);
            fotoTex.LoadImage(bytes);


            image.gameObject.SetActive(true);
            image.sprite = Sprite.Create(fotoTex, new Rect(0.0f, 0.0f, fotoTex.width, fotoTex.height), new Vector2(0.5f, 0.5f), 100.0f);


            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
           // Debug.Log("gravou");

        }
    }


}
