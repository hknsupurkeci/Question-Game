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
    public RawImage displayImageOnCanvas; // Canvas'taki Image bile�enini s�r�kleyin.
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
        // Kamera ba�lat�l�yor ve RawImage bile�enine atan�yor.
        webCamTexture = new WebCamTexture();
        webCamTexture.requestedWidth = 3840; // �rnek olarak 4K ��z�n�rl�k
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
    // Sahne kapan�rken �a�r�l�r
    void OnDestroy()
    {
        UnityEngine.Debug.Log("Camera captura");
        // Kameray� durdur
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

        StartCoroutine(CapturePhotoAfterDelay(3.0f)); // 5 saniye bekleyip foto�raf �ekiliyor.
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
        //displayImageOnCanvas.sprite = sprite; // �ekilen foto�raf� canvas'ta g�steriyoruz.

        // Texture2D'yi masa�st�ne kaydet
        byte[] bytes = capturedTexture.EncodeToJPG();
        string imagePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + $"/{PlayerPrefs.GetString("playerName")}.jpg";
        System.IO.File.WriteAllBytes(imagePath, bytes);

        // Email g�nderme i�lemini ba�lat
        yield return EmailSender(PlayerPrefs.GetString("playerName"), PlayerPrefs.GetString("playerEmail"), imagePath);

        PrintJPG();

        UnityEngine.Debug.Log("foto �ekildi");
        //try
        //{


        //}
        //catch (Exception ex) { UnityEngine.Debug.LogException(ex); }
    }
    /// <summary>
    /// Olu�turulan jpg dosyas�n� yazc�dan yazd�r�r.
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
            Console.WriteLine("Yazd�rma hatas�: " + ex.Message);
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

        string subject = "Foto�raflar�n�z";
        string htmlContent = "<html><body>Merhaba,<br><br>Yar��mam�za kat�ld���n�z i�in te�ekk�r ederiz. Foto�raf�n�za ekten ula�abilirsiniz.<br>�yi g�nler dileriz.</body></html>";

        // Send to email
        var task = emailSender.SendEmailAsync(recipientName, recipientEmail, subject, htmlContent);

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            UnityEngine.Debug.LogError("Error sending email: " + task.Exception.ToString());
        }
    }
}
