using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager2 : MonoBehaviour
{
    public static UIManager2 Ins;

    public Slider timerBar;

    public Text questionText1;

    public Text questionText2;

    public Dialog2 dialog1;

    public Dialog2 dialog2;

    public AnswerButton2[] answerButtons1;

    public AnswerButton2[] answerButtons2;

    public GameObject dialogSetting;

    private void Awake()
    {
        MakeSingleton();        
    }
    private void Start()
    {
        
    }
    public void SetTime(float countDownTime)
    {
        if (timerBar)
            timerBar.value = countDownTime;
    }
    public void SetQuestTionText(string content) 
    {
        if (questionText1 && questionText2)
        {
            questionText1.text = content;
            questionText2.text = content;
        }
            
    }

    public void ShuffleAnswer() 
    { 
        if(answerButtons1 != null&& answerButtons2 != null && answerButtons1.Length > 0 && answerButtons2.Length >0)
        {
            for (int i = 0; i < answerButtons1.Length; i++)
            {
                if(answerButtons1[i])
                {
                    answerButtons1[i].tag = "WrongAnswer1";
                }
            }

            for (int i = 0; i < answerButtons2.Length; i++)
            {
                if (answerButtons2[i])
                {
                    answerButtons2[i].tag = "WrongAnswer2";
                }
            }

            int randIdx1 = Random.Range(0, 2);

            int randIdx2 = Random.Range(0, 2);

            if (answerButtons1[randIdx1])
            {
                answerButtons1[randIdx1].tag = "RightAnswer1";
            }
            if (answerButtons2[randIdx2])
            {
                answerButtons2[randIdx2].tag = "RightAnswer2";
            }
        }
    }


    public void MakeSingleton() 
    {
        if (Ins == null) 
        {
            Ins = this;
        }else
        {
            Destroy(gameObject);
        }
    }
}
