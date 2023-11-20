using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class CameraCapture : MonoBehaviour
{
    public UnityEngine.UI.Button FotoCekButton;
    private WebCamTexture webCamTexture;
    public RawImage displayImageOnCanvas; // Canvas'taki Image bileþenini sürükleyin.
    public GameObject kameraSayacObj;
    private Text kameraText;
    public int kameraCekimSayisi = 3;
    private int _kameraCekimSayisi = 0;
    private float timer = 0;
    private float countdownDuration = 1f;
    private bool flag = false;
    private AudioSource audioSource;

    private void Start()
    {
        UnityEngine.Debug.Log(PlayerPrefs.GetString("playerEmail"));
        // Kamera baþlatýlýyor ve RawImage bileþenine atanýyor.
        webCamTexture = new WebCamTexture();
        webCamTexture.requestedWidth = 3840; // Örnek olarak 4K çözünürlük
        webCamTexture.requestedHeight = 2160;

        audioSource = GetComponent<AudioSource>();
        //
        _kameraCekimSayisi = kameraCekimSayisi;
        kameraText = kameraSayacObj.GetComponent<Text>();
        kameraText.text = kameraCekimSayisi.ToString();

        
        //rawImage.texture = webCamTexture;
        displayImageOnCanvas.texture = webCamTexture;
        webCamTexture.Play();
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
    // Sahne kapanýrken çaðrýlýr
    void OnDestroy()
    {
        UnityEngine.Debug.Log("Camera captura");
        // Kamerayý durdur
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            webCamTexture = null;
        }
    }
    public void StartCapture()
    {
        
        //webCamTexture.Play();

        flag = true;
        kameraSayacObj.SetActive(true);
        FotoCekButton.enabled = false;

        StartCoroutine(CapturePhotoAfterDelay(3.0f)); // 5 saniye bekleyip fotoðraf çekiliyor.
    }
    private IEnumerator CapturePhotoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        audioSource.Play();
        //
        Texture2D capturedTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        capturedTexture.SetPixels(webCamTexture.GetPixels());
        capturedTexture.Apply();

        //webCamTexture.Stop(); // Kamera durduruluyor.

        //Sprite sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), new Vector2(0.5f, 0.5f));
        //displayImageOnCanvas.sprite = sprite; // Çekilen fotoðrafý canvas'ta gösteriyoruz.

        // Texture2D'yi masaüstüne kaydet
        byte[] bytes = capturedTexture.EncodeToJPG();
        string imagePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + $"/{PlayerPrefs.GetString("playerName")}.jpg";
        System.IO.File.WriteAllBytes(imagePath, bytes);

        // Email gönderme iþlemini baþlat
        yield return EmailSender(PlayerPrefs.GetString("playerName"), PlayerPrefs.GetString("playerEmail"), imagePath);

        PrintJPG();

        UnityEngine.Debug.Log("foto çekildi");
        //try
        //{


        //}
        //catch (Exception ex) { UnityEngine.Debug.LogException(ex); }
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

    IEnumerator EmailSender(string recipientName, string recipientEmail, string imagePath)
    {
        // Connect to service
        var httpClient = new HttpClient();
        var tokenService = new TokenService(httpClient);
        var emailService = new EmailService(httpClient);

        // Create the email
        var emailSender = new EmailSender(tokenService, emailService, imagePath);

        string subject = "Fotoðraflarýnýz";
        string htmlContent = "<html><body>Merhaba,<br><br>Yarýþmamýza katýldýðýnýz için teþekkür ederiz. Fotoðrafýnýza ekten ulaþabilirsiniz.<br>Ýyi günler dileriz.</body></html>";

        // Send to email
        var task = emailSender.SendEmailAsync(recipientName, recipientEmail, subject, htmlContent);

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            UnityEngine.Debug.LogError("Error sending email: " + task.Exception.ToString());
        }
    }
}
