using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Firebase.Database;

public class DialogEndGame : MonoBehaviour
{
    public static DialogEndGame Ins;
    public Text scoreText;
    public Text contentText;
    public Text timePlayText; 
    
    private void Awake()
    {
        MakeSingleton();
    }

    public void Show()
    {
        Time.timeScale = 0f;
        float timeToDisplay = TimeController.Ins.time;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (QuestionManager.Ins.isNewRecord)
        {
            contentText.text = "KỶ LỰC MỚI";

            PlayerData player = PlayerData.getPlayerByName(PlayerManager.Ins.listPlayer
                , PlayerPrefs.GetString("name"));

            if (player == null)
            {
                player = new PlayerData(PlayerPrefs.GetString("name")
                    , PlayerPrefs.GetFloat("score").ToString()
                    , PlayerPrefs.GetFloat("timePlay").ToString());
                PlayerManager.Ins.listPlayer.Add(player);
            }
            else
            {
                player.UpdateRecord(PlayerPrefs.GetFloat("score").ToString()
                    , PlayerPrefs.GetFloat("timePlay").ToString());
            }

            string json = JsonUtility.ToJson(player);

            GameController.Ins.mDatabaseRef.ValueChanged -= PlayerManager.Ins.LoadPlayerDatas;
            GameController.Ins.mDatabaseRef.Child("players").Child(player.name).SetRawJsonValueAsync(json);
        }

        scoreText.text = GameController.Ins.score.ToString();
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
