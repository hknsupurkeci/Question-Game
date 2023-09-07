using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using System; 
using Image = UnityEngine.UI.Image;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

public class CameraCapture : MonoBehaviour
{
    public UnityEngine.UI.Button FotoCekButton;
    private WebCamTexture webCamTexture;
    public Image displayImageOnCanvas; // Canvas'taki Image bileþenini sürükleyin.
    public GameObject kameraSayacObj;
    private Text kameraText;
    public int kameraCekimSayisi = 3;
    private int _kameraCekimSayisi = 0;
    private float timer = 0;
    private float countdownDuration = 1f;
    private bool flag = false;
    private void Start()
    {
        _kameraCekimSayisi = kameraCekimSayisi;
        kameraText = kameraSayacObj.GetComponent<Text>();
        kameraText.text = kameraCekimSayisi.ToString();
        webCamTexture = new WebCamTexture();
    }
    private void Update()
    {
        if (kameraCekimSayisi > 0 && flag)
        {
            timer += Time.deltaTime;
            if (timer >= countdownDuration)
            {
                kameraCekimSayisi -= 1;
                kameraText.text = kameraCekimSayisi.ToString();
                timer = 0f;
                if (kameraCekimSayisi == 0)
                {
                    kameraText.text = _kameraCekimSayisi.ToString();
                    kameraSayacObj.SetActive(false);
                    kameraCekimSayisi = _kameraCekimSayisi;
                    FotoCekButton.enabled = true;
                    flag = false;
                }
            }
        }
    }
    public void StartCapture()
    {
        webCamTexture.requestedWidth = 3840; // Örnek olarak 4K çözünürlük
        webCamTexture.requestedHeight = 2160;
        webCamTexture.Play();

        flag = true;
        kameraSayacObj.SetActive(true);
        FotoCekButton.enabled = false;

        StartCoroutine(CapturePhotoAfterDelay(3.0f)); // 5 saniye bekleyip fotoðraf çekiliyor.
    }

    private IEnumerator CapturePhotoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        try
        {
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
        }catch(Exception ex) { UnityEngine.Debug.LogException(ex); }
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
