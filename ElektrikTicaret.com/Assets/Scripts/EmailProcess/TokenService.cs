using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Interfaces;

public class TokenService : ITokenService
{
    private readonly HttpClient _httpClient;
    private readonly string _tokenUrl = "https://useapi.useinbox.com/token/";

    public TokenService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> GetTokenAsync()
    {
        var tokenData = new { EmailAddress = "webadmin@eae.com.tr", Password = "eaeWeb*1973" };
        var response = await _httpClient.PostAsync(_tokenUrl, new StringContent(JsonConvert.SerializeObject(tokenData), Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            UnityEngine.Debug.Log($"Token hatasý: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return JObject.Parse(responseContent)["resultObject"]["access_token"].ToString();
    }
}
