using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using System; 
using Image = UnityEngine.UI.Image;

public class CameraCapture : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    public Image displayImageOnCanvas; // Canvas'taki Image bileþenini sürükleyin.

    private void Start()
    {
        webCamTexture = new WebCamTexture();

    }

    public void StartCapture()
    {
        webCamTexture.Play(); // kamera baþlatýlýyor.
        StartCoroutine(CapturePhotoAfterDelay(2.0f)); // 5 saniye bekleyip fotoðraf çekiliyor.
    }

    private IEnumerator CapturePhotoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Texture2D capturedTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        capturedTexture.SetPixels(webCamTexture.GetPixels());
        capturedTexture.Apply();

        webCamTexture.Stop(); // Kamera durduruluyor.

        Sprite sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), new Vector2(0.5f, 0.5f));
        displayImageOnCanvas.sprite = sprite; // Çekilen fotoðrafý canvas'ta gösteriyoruz.

        // Texture2D'yi masaüstüne kaydet
        byte[] bytes = capturedTexture.EncodeToJPG();
        System.IO.File.WriteAllBytes(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + $"/{PlayerPrefs.GetString("playerName")}.jpg", bytes);

        PrintJPG();

        UnityEngine.Debug.Log("foto çekildi");
    }
    /// <summary>
    /// Oluþturulan jpg dosyasýný yazcýdan yazdýrýr.
    /// </summary>
    /// <param name="pdfFilePath"></param>
    void PrintJPG()
    {
        try
        {
            string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            string jpgFilePath = Path.Combine(desktopPath, $"{PlayerPrefs.GetString("playerName")}.jpg");

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = jpgFilePath;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yazdýrma hatasý: " + ex.Message);
        }
    }
}
