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
            UnityEngine.Debug.Log("Token al�namad�.");
            return;
        }

        if (!File.Exists(_imagePath))
        {
            UnityEngine.Debug.Log("G�rsel dosyas� bulunamad�.");
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
                    name = recipientName, // Al�c� ismi
                    email = recipientEmail // Al�c� e-posta adresi
                }
            },
            subject = "Foto�raflar�n�z",
            templateId = "6530ee06e7a3260001a39c4e",
            attachments = new[]
            {
                new
                {
                    content = base64Image,
                    type = "image/jpeg", // G�rselin MIME tipini do�ru �ekilde belirtin.
                    name = $"{recipientName}.jpg", // G�rselin dosya ad�n� belirtin.
                }
            },
            htmlContent = "<html><body>Merhaba,<br><br>" +
                          "Yar��mam�za kat�ld���n�z i�in te�ekk�r ederiz. Foto�raf�n�za ekten ula�abilirsiniz.<br>" +
                          "�yi g�nler dileriz.</body></html>"
        };

        bool isEmailSent = await _emailService.SendEmailAsync(token, emailData);
        if (isEmailSent)
        {
            UnityEngine.Debug.Log("Mail ba�ar�yla g�nderildi!");
        }
        else
        {
            UnityEngine.Debug.Log("Mail g�nderilemedi.");
        }
    }
}