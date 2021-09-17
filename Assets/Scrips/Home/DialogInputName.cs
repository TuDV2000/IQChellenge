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
    public Text notificationText;

    private void Awake()
    {
        MakeSingleton();
    }

    public void Show()
    {        
        gameObject.SetActive(true);
    }

    public void SetPlayerName()
    {
        PlayerData player = new PlayerData(nameText.text);
        if (player.CheckPlayer(PlayerManager.Ins.listPlayer))
        {
            notificationText.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("name", nameText.text);
            PlayerPrefs.Save();
            gameObject.SetActive(false);
            UIHomeManager.Ins.homeManager.Show();
        }
    }

    public void HideNotification()
    {
        notificationText.gameObject.SetActive(false);
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
