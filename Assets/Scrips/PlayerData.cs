using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public string email;

    public PlayerData(string name, string email)
    {
        this.name = name;
        this.email = email;
    }
}
