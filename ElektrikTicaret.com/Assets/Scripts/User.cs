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

    public User(string _eMail, string _telephone, string _firmaName, string _name, int _score)
    {
        this.eMail = _eMail;
        this.telephone = _telephone;
        this.firmaName = _firmaName;
        this.name = _name;
        this.score = _score;
    }
    public User() { }

    public int Score { get { return score; } set { score = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string EMail { get { return eMail; } set { eMail = value; } }
    public string Telephone { get { return telephone; } set { telephone = value; } }
    public string Firma { get { return firmaName; } set { firmaName = value; } }

    public override string ToString()
    {
        return $"{Name},{Score},{EMail},{Telephone},{Firma}";
    }

    // Bir text satýrýný User nesnesine dönüþtüren bir metod.
    public static User FromText(string csvLine)
    {
        string[] values = csvLine.Split(',');
        return new User
        {
            Name = values[0],
            Score = int.Parse(values[1]),
            EMail = values[2],
            Telephone = values[3],
            Firma = values[4]
        };
    }
}
