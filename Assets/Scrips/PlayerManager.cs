using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Ins;

    public List<PlayerData> listPlayer = new List<PlayerData>();

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        if (GameController.Ins != null)
        {
            if (listPlayer.Count == 0)
                GameController.Ins.mDatabaseRef.ValueChanged += LoadPlayerDatas;
            else
                GameController.Ins.mDatabaseRef.ValueChanged -= LoadPlayerDatas;
        }
        if (UIHomeManager.Ins != null)
        {
            if (listPlayer.Count == 0)
                UIHomeManager.Ins.mDatabaseRef.ValueChanged += LoadPlayerDatas;
            else
                UIHomeManager.Ins.mDatabaseRef.ValueChanged -= LoadPlayerDatas;
        }
    }

    public void LoadPlayerDatas(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        foreach (DataSnapshot dsPlayers in e.Snapshot.Child("players").Children)
        {
            listPlayer.Add(new PlayerData(dsPlayers.Child("name").Value.ToString()
                , dsPlayers.Child("score").Value.ToString()
                , dsPlayers.Child("timePlay").Value.ToString()));
        }
    }

    public void MakeSingleton()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
