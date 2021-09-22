using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData : IComparable<PlayerData>
{
    public string name;
    public string score;
    public string timePlay;

    public PlayerData() { }

    public PlayerData(string name)
    {
        this.name = name;
    }

    public PlayerData(string name, string score, string timePlay)
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

    public static PlayerData getPlayerByName(List<PlayerData> list, string name)
    {
        foreach (PlayerData p in list)
        {
            if (p.name.Equals(name))
                return p;
        }

        return null;
    }

    public void UpdateRecord(string score, string timePlay)
    {
        this.score = score;
        this.timePlay = timePlay;
    }

    public int CompareTo(PlayerData other)
    {
        if (float.Parse(this.score) < float.Parse(other.score))
            return 1;
        else if (float.Parse(this.score) > float.Parse(other.score))
            return -1;
        else
        {
            if (float.Parse(this.timePlay) > float.Parse(other.timePlay))
                return 1;
            else if (float.Parse(this.timePlay) < float.Parse(other.timePlay))
                return -1;
            else
                return 0;
        }
    }
}
