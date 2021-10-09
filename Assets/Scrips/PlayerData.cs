using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData : IComparable<PlayerData>
{
    public string id;
    public string name;
    public float score;
    public float timePlay;

    public PlayerData() { }

    public PlayerData(string name)
    {
        this.name = name;
    }

    public PlayerData(string name, float score, float timePlay)
    {
        this.name = name;
        this.score = score;
        this.timePlay = timePlay;
    }

    public PlayerData(string id, string name, float score, float timePlay)
    {
        this.id = id;
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

    public void UpdateRecord(float score, float timePlay)
    {
        this.score = score;
        this.timePlay = timePlay;
    }

    public int CompareTo(PlayerData other)
    {
        if (this.score < other.score)
            return 1;
        else if (this.score > other.score)
            return -1;
        else
        {
            if (this.timePlay > other.timePlay)
                return 1;
            else if (this.timePlay < other.timePlay)
                return -1;
            else
                return 0;
        }
    }
}
