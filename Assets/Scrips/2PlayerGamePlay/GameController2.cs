using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController2 : MonoBehaviour
{
    public static GameController2 Ins;

    public bool endGame = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        MakeSingleton();
        //CreateQuestion();
        enabled = false;
        PlayerManager2.Ins.SetSilderValue();
    }

    // Update is called once per frame
    void Update()
    {
        //Timer.Ins.SetupTime();
        Timer2.Ins.UpdateTime();
        if (!endGame)
            PlayerDie();
    }
    public void CreateQuestion()
    {
        QuestionData2 qs = QuestionManager2.Ins.GetRandomQuestion();
        if (qs != null)
        {
            UIManager2.Ins.SetQuestTionText(qs.question);

            UIManager2.Ins.ShuffleAnswer();

            var temp = UIManager2.Ins.answerButtons1;

            if (temp != null)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    int answerId = i;

                    if (string.Compare(temp[i].btnComp.tag, "RightAnswer1") == 0)
                    {
                        temp[i].SetAnswerText(qs.rightAnswer);
                    }
                    else
                    {
                        temp[i].SetAnswerText(qs.wrongAnswer);
                    }
                    temp[answerId].btnComp.onClick.RemoveAllListeners();
                    temp[answerId].btnComp.onClick.AddListener(() => 
                        CheckRightAnswerEvent(temp[answerId])
                    );
                }
            }

            var temp2 = UIManager2.Ins.answerButtons2;

            if (temp2 != null)
            {
                for (int i = 0; i < temp2.Length; i++)
                {
                    int answerId = i;

                    if (string.Compare(temp2[i].btnComp.tag, "RightAnswer2") == 0)
                    {
                        temp2[i].SetAnswerText(qs.rightAnswer);
                    }
                    else
                    {
                        temp2[i].SetAnswerText(qs.wrongAnswer);
                    }
                    temp2[answerId].btnComp.onClick.RemoveAllListeners();
                    temp2[answerId].btnComp.onClick.AddListener(() => CheckRightAnswerEvent(temp2[answerId]));
                }
            }

        }
    }

    public IEnumerator WaitCreateQuestion()
    {
        yield return new WaitForSeconds(0.5F);
        CreateQuestion();
        Timer2.Ins.ResetTime();
        TurnOnAndOffAllButton(true);
    }    

    public void CheckRightAnswerEvent(AnswerButton2 answerButton)
    {
        if(answerButton.CompareTag("RightAnswer1"))
        {
            Debug.Log("Nguoi choi 1 tra loi dung");
            TurnOnAndOffAllButton(false);
            PlayerManager2.Ins.TakeDamageP2();
            PlayerManager2.Ins.ChangeHitText1("Đúng");
            PlayerManager2.Ins.ChangeHitText2("-2");
            PlayerMovement2.Ins.AttackMove1();
            StartCoroutine(WaitCreateQuestion());
        }
        if (answerButton.CompareTag("WrongAnswer1"))
        {
            Debug.Log("Nguoi choi 1 tra loi sai");
            TurnOnAndOffAllButton(false);
            PlayerManager2.Ins.TakeDamageP1();
            PlayerManager2.Ins.ChangeHitText1("Sai");
            PlayerMovement2.Ins.AttackMove2();
            StartCoroutine(WaitCreateQuestion());
        }
        if (answerButton.CompareTag("RightAnswer2"))
        {
            Debug.Log("Nguoi choi 2 tra loi dung");
            TurnOnAndOffAllButton(false);
            PlayerManager2.Ins.TakeDamageP1();
            PlayerManager2.Ins.ChangeHitText2("Đúng");
            PlayerManager2.Ins.ChangeHitText1("-2");
            PlayerMovement2.Ins.AttackMove2();
            StartCoroutine(WaitCreateQuestion());
        }
        if (answerButton.CompareTag("WrongAnswer2"))
        {
            Debug.Log("Nguoi choi 2 tra loi sai");
            TurnOnAndOffAllButton(false);
            PlayerManager2.Ins.TakeDamageP2();
            PlayerManager2.Ins.ChangeHitText2("Sai");
            PlayerMovement2.Ins.AttackMove1();
            StartCoroutine(WaitCreateQuestion());
        }
    }

    public void PlayerDie()
    {
        if(PlayerManager2.Ins.P1Health == 0)
        {
            UIManager2.Ins.dialog2.Show(true);
            endGame = true;
        }
        else if(PlayerManager2.Ins.P2Health == 0)
        {
            UIManager2.Ins.dialog1.Show(true);
            endGame = true;
        }
        if (endGame)
        {
            Time.timeScale = 0f;
            AudioController.Ins.StopMusic();
            AudioController.Ins.PlayWinSound();            
        }
    }

    public void TurnOnAndOffAllButton(bool a)
    {
        var temp = UIManager2.Ins.answerButtons1;

        if (temp != null)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].btnComp.interactable = a;
            }
        }

        var temp2 = UIManager2.Ins.answerButtons2;

        if (temp2 != null)
        {
            for (int i = 0; i < temp2.Length; i++)
            {
                temp2[i].btnComp.interactable = a;
            }
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("2PlayerGamePlay");
    }
    public void ExitToHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Setting()
    {
        UIManager2.Ins.dialogSetting.SetActive(true);
    }

    public void DoneSetting()
    {
        UIManager2.Ins.dialogSetting.SetActive(false);
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
