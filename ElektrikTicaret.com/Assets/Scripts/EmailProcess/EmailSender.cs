using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static Interfaces;

public class EmailSender
{
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly string _imagePath;

    public EmailSender(ITokenService tokenService, IEmailService emailService, string imagePath)
    {
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _imagePath = imagePath ?? throw new ArgumentNullException(nameof(imagePath));
    }

    public async Task SendEmailAsync(string recipientName, string recipientEmail, string subject, string htmlContent)
    {
        string token = await _tokenService.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            UnityEngine.Debug.Log("Token alýnamadý.");
            return;
        }

        if (!File.Exists(_imagePath))
        {
            UnityEngine.Debug.Log("Görsel dosyasý bulunamadý.");
            return;
        }
        byte[] imageBytes = await File.ReadAllBytesAsync(_imagePath);
        string base64Image = Convert.ToBase64String(imageBytes);

        var emailData = new
        {
            from = new
            {
                name = "ElektrikTicaret",
                email = "marketing@elektrikticaret.com"
            },
            to = new[]
            {
                new
                {
                    name = recipientName, // Alýcý ismi
                    email = recipientEmail // Alýcý e-posta adresi
                }
            },
            subject = "Fotoðraflarýnýz",
            templateId = "6530ee06e7a3260001a39c4e",
            attachments = new[]
            {
                new
                {
                    content = base64Image,
                    type = "image/jpeg", // Görselin MIME tipini doðru þekilde belirtin.
                    name = $"{recipientName}.jpg", // Görselin dosya adýný belirtin.
                }
            },
            htmlContent = "<html><body>Merhaba,<br><br>" +
                          "Yarýþmamýza katýldýðýnýz için teþekkür ederiz. Fotoðrafýnýza ekten ulaþabilirsiniz.<br>" +
                          "Ýyi günler dileriz.</body></html>"
        };

        bool isEmailSent = await _emailService.SendEmailAsync(token, emailData);
        if (isEmailSent)
        {
            UnityEngine.Debug.Log("Mail baþarýyla gönderildi!");
        }
        else
        {
            UnityEngine.Debug.Log("Mail gönderilemedi.");
        }
    }
}