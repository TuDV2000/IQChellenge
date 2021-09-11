using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogEndGame : MonoBehaviour
{
    public static DialogEndGame Ins;
    public Text scoreText;

    private void Awake()
    {
        MakeSingleton();
    }

    public void Show()
    {
        Time.timeScale = 0f;
        scoreText.text = GameController.ins.score.ToString();
        gameObject.SetActive(true);
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
