using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Interfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
    }
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string token, object emailContent);
    }
}
