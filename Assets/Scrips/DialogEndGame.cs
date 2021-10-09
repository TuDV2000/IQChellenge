using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            string json;

            if (player == null)
            {
                player = new PlayerData(PlayerPrefs.GetString("name")
                    , PlayerPrefs.GetFloat("score")
                    , PlayerPrefs.GetFloat("timePlay"));
                PlayerManager.Ins.listPlayer.Add(player);
                json = JsonUtility.ToJson(player);
                GameController.Ins.mDatabaseRef.Child("players").Push().SetRawJsonValueAsync(json);
            }
            else
            {
                player.UpdateRecord(PlayerPrefs.GetFloat("score")
                    , PlayerPrefs.GetFloat("timePlay"));
                json = JsonUtility.ToJson(player);
                GameController.Ins.mDatabaseRef.Child("players").Child(player.id).SetRawJsonValueAsync(json);
            }
            //GameController.Ins.mDatabaseRef.ValueChanged -= PlayerManager.Ins.LoadPlayerDatas;
            //GameController.Ins.mDatabaseRef.Child("players").Push().SetRawJsonValueAsync(json);
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
