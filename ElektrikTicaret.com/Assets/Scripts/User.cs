using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    private string eMail;
    private string telephone;
    private string firmaName;
    private string name;
    private int score = 0;

    public int Score { get { return score; } set { score = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string EMail { get { return eMail; } set { eMail = value; } }
    public string Telephone { get { return telephone; } set { telephone = value; } }
    public string Firma { get { return firmaName; } set { firmaName = value; } }

}
