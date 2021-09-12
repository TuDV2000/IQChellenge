using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;

public class DialogInputName : MonoBehaviour
{
    public static DialogInputName Ins;
    public string playerName;
    public Text placeh;
    public Text nameText;
    public GameObject notificationText;

    DatabaseReference mDatabaseRef;
    List<PlayerData> list = new List<PlayerData>();

    private void Awake()
    {
        MakeSingleton();
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        mDatabaseRef.ValueChanged += LoadPlayerDatas;
    }

    private void LoadPlayerDatas(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        foreach (DataSnapshot dsPlayers in e.Snapshot.Child("players").Children)
        {
            Debug.Log("in:" + dsPlayers.Child("name").Value);
            list.Add(new PlayerData(dsPlayers.Child("name").Value.ToString()));
        }
    }

    public void Show()
    {        
        gameObject.SetActive(true);
    }

    public void SetPlayerName()
    {
        PlayerData player = new PlayerData(nameText.text);
        if (player.CheckPlayer(list))
        {
            notificationText.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("name", nameText.text);
            PlayerPrefs.Save();
            gameObject.SetActive(false);
            UIHomeManager.Ins.homeManager.Show();
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
