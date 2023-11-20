using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using static Interfaces;

public class EmailService : IEmailService
{
    private readonly HttpClient _httpClient;
    private readonly string _sendEmailUrl = "https://useapi.useinbox.com/notify/v1/send";

    public EmailService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<bool> SendEmailAsync(string token, object emailContent)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync(_sendEmailUrl, new StringContent(JsonConvert.SerializeObject(emailContent), Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            UnityEngine.Debug.Log($"Mail gönderme hatasý: {await response.Content.ReadAsStringAsync()}");
            return false;
        }

        return true;
    }
}
