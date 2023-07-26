using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private string name;
    private int score = 0;

    public int Score { get { return score; } set { score = value; } }
    public string Name { get { return name; } set { name = value; } }
}
