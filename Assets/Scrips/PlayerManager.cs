using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Ins;

    public List<PlayerData> listPlayer = new List<PlayerData>();

    DatabaseReference databaseRef;

    private void Awake()
    {
        MakeSingleton();

        //databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        databaseRef = FirebaseDatabase.DefaultInstance.GetReference("players");
        databaseRef.KeepSynced(true);
    }

    private void Start()
    {
        //if (GameController.Ins != null)
        //{
        //    if (listPlayer.Count == 0)
        //        GameController.Ins.mDatabaseRef.ValueChanged += LoadPlayerDatas;
        //    else
        //        GameController.Ins.mDatabaseRef.ValueChanged -= LoadPlayerDatas;
        //}
        //if (UIHomeManager.Ins != null)
        //{
        //    if (listPlayer.Count == 0)
        //        UIHomeManager.Ins.mDatabaseRef.ValueChanged += LoadPlayerDatas;
        //    else
        //        UIHomeManager.Ins.mDatabaseRef.ValueChanged -= LoadPlayerDatas;
        //}
        //if (UIHomeManager.Ins != null)
        //{
        //    if (listPlayer.Count == 0)
        //    {
        //        LoadPlayerDatas();
        //    }
        //}
        //if (GameController.Ins != null)
        //{
        //    if (listPlayer.Count == 0)
        //    {
        //        LoadPlayerDatas();
        //    }
        //}
        if (listPlayer.Count == 0)
            LoadPlayerDatas();
    }
    public void LoadPlayerDatas()
    {
        //UIHomeManager.Ins.mDatabaseRef.GetValueAsync().ContinueWithOnMainThread(task =>
        databaseRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error firebase getvalue");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                //foreach (DataSnapshot dsPlayers in snapshot.Child("players").Children)
                foreach (DataSnapshot dsPlayers in snapshot.Children)
                {
                    listPlayer.Add(new PlayerData(dsPlayers.Key, dsPlayers.Child("name").Value.ToString()
                        , float.Parse(dsPlayers.Child("score").Value.ToString())
                        , float.Parse(dsPlayers.Child("timePlay").Value.ToString())));
                }
            }
        });
    }
    //public void LoadPlayerDatas(object sender, ValueChangedEventArgs e)
    //{
    //    if (e.DatabaseError != null)
    //    {
    //        Debug.LogError(e.DatabaseError.Message);
    //        return;
    //    }
    //    foreach (DataSnapshot dsPlayers in e.Snapshot.Child("players").Children)
    //    {
    //        listPlayer.Add(new PlayerData(dsPlayers.Child("name").Value.ToString()
    //            , dsPlayers.Child("score").Value.ToString()
    //            , dsPlayers.Child("timePlay").Value.ToString()));
    //    }
    //}

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
