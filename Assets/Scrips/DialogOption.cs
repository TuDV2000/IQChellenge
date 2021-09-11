using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DialogOption : MonoBehaviour
{
    public static DialogOption Ins;

    private void Awake()
    {
        MakeSingleton();    
    }

    public void Show()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void DoneSetting()
    {
        gameObject.SetActive(false);
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ////Load lai tat ca cac cau hoi

        ////Dat lai so cau hoi da tra loi tuoc do
        //QuestionManager.Ins.rigrtAnswerCount = 0;
        ////Dat lai thoi gian choi theo level da chon
        //TimeController.ins.time = MenuLevelManager.Ins.timeLevel;
        ////Tat dialog option
        //gameObject.SetActive(false);
        ////Khoi tao lai cau hoi
        //Invoke("LoadQuestion", 1.5f);
    }

    void LoadQuestion()
    {
        Debug.Log("play again");
        QuestionManager.Ins.CreateQuestion();
        Time.timeScale = 1f;
        TimeController.ins.enabled = true;
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
