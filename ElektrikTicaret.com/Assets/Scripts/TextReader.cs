using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextReader
{
    private string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/data.txt";

    public void SaveUser(User user)
    {
        File.AppendAllText(path, user.ToString() + "\n");
    }

    public List<User> LoadAllUsers()
    {
        List<User> users = new List<User>();
        if (File.Exists(path))
        {
            string[] allLines = File.ReadAllLines(path);
            foreach (string line in allLines)
            {
                users.Add(User.FromText(line));
            }
        }
        else
        {
            Debug.LogError("Data file not found!");
        }
        return users;
    }

    public int UsersCount { 
        get 
        {
            if (File.Exists(path))
                return File.ReadAllLines(path).Length;
            else
                return 0;
        } 
    }
}
