using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;

public class DialogEndGame : MonoBehaviour
{
    public static DialogEndGame Ins;
    public Text scoreText;
    public Text contentText;
    public Text timePlayText; 
    public List<PlayerData> listPlayerDatas = new List<PlayerData>();
   
    DatabaseReference mDatabaseRef;
    
    private void Awake()
    {
        MakeSingleton();
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        mDatabaseRef.ValueChanged += LoadPlayerDatas;
    }

    public void Show()
    {
        Time.timeScale = 0f;
        float timeToDisplay = TimeController.ins.time;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (QuestionManager.Ins.isNewRecord)
        {
            contentText.text = "KỶ LỰC MỚI";

            PlayerData playerData = new PlayerData(PlayerPrefs.GetString("name")
                , PlayerPrefs.GetFloat("score").ToString()
                , PlayerPrefs.GetFloat("timePlay").ToString());
            string json = JsonUtility.ToJson(playerData);

            GameController.ins.mDatabaseRef.Child("players").Child(playerData.name).SetRawJsonValueAsync(json);
        }

        scoreText.text = GameController.ins.score.ToString();
        timePlayText.text = timeString;

        gameObject.SetActive(true);
    }

    public void ShowRankDialog()
    {
        UIManager.Ins.dialogRank.gameObject.SetActive(true);
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    private void LoadPlayerDatas(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        foreach (DataSnapshot player in e.Snapshot.Child("players").Children)
        {
            PlayerData p = new PlayerData();
            p.name = player.Child("name").Value.ToString();
            p.score = player.Child("score").Value.ToString();
            p.timePlay = player.Child("timePlay").Value.ToString();

            listPlayerDatas.Add(p);
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
