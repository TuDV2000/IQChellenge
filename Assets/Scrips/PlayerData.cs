using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public float score;
    public float timePlay;

    public PlayerData() { }

    public PlayerData(string name)
    {
        this.name = name;
        this.score = 0;
        this.timePlay = 0;
    }

    public PlayerData(string name, float score, float timePlay)
    {
        this.name = name;
        this.score = score;
        this.timePlay = timePlay;
    }

    public bool CheckPlayer(List<PlayerData> list)
    {
        foreach (PlayerData p in list)
        {
            if (p.name.Equals(this.name))
                return true;
        }

        return false;
    }
}
