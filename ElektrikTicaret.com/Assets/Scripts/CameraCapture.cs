using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraCapture : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    public Image displayImageOnCanvas; // Canvas'taki Image bileşenini sürükleyin.

    private void Start()
    {
        webCamTexture = new WebCamTexture();

    }

    public void StartCapture()
    {
        webCamTexture.Play(); // kamera başlatılıyor.
        StartCoroutine(CapturePhotoAfterDelay(2.0f)); // 5 saniye bekleyip fotoğraf çekiliyor.
    }

    private IEnumerator CapturePhotoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Texture2D capturedTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        capturedTexture.SetPixels(webCamTexture.GetPixels());
        capturedTexture.Apply();

        webCamTexture.Stop(); // Kamera durduruluyor.

        Sprite sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), new Vector2(0.5f, 0.5f));
        displayImageOnCanvas.sprite = sprite; // Çekilen fotoğrafı canvas'ta gösteriyoruz.

        Debug.Log("foto çekildi");
    }
}
