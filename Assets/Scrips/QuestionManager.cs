using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Firebase;
using Firebase.Database;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Ins;

    public bool isNewRecord = false;
    public List<QuestionData> listQuestions = new List<QuestionData>();

    int rigrtAnswerCount = 0;    
    int countQuestions = 0;

    private void Awake() 
    {
        MakeSingleton();
    }

    private void Start()
    {
        if (listQuestions.Count == 0) 
            GameController.Ins.mDatabaseRef.ValueChanged += LoadQuestionDatas;
        else
            GameController.Ins.mDatabaseRef.ValueChanged -= LoadQuestionDatas;
    }

    /*
     * Hàm lấy danh sách câu hỏi từ database trên cloud firebase.
     */
    private void LoadQuestionDatas(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        foreach (DataSnapshot dsQuestions in e.Snapshot.Child("questions").Children)
        {
            QuestionData q = new QuestionData();
            q.content = dsQuestions.Child("content").Value.ToString();
            q.rightAnswer = dsQuestions.Child("rightAnswer").Value.ToString();
            foreach (DataSnapshot dsWrongAnswer in dsQuestions.Child("wrongAnswers").Children)
            {
                q.wrongAnswers.Add(dsWrongAnswer.Value.ToString());
            }
            listQuestions.Add(q);
        }
        countQuestions = listQuestions.Count;
    }

    /*
     * Hàm lấy câu hỏi ngẫu nhiên.
     * 1) Câu hỏi hiện tại sẽ được lấy từ listQuestions.
     * 2) Sau khi lấy thì ta remove câu hỏi đó ra khỏi listQuestions để tránh việc câu hỏi bị lặp lại.
     */
    public QuestionData GetRandomQuestion() {
        QuestionData curQuestion = new QuestionData();
        
        if (listQuestions != null && listQuestions.Count > 0) {
            int ranIndex = Random.Range(0, listQuestions.Count);

            curQuestion = listQuestions[ranIndex];

            listQuestions.RemoveAt(ranIndex);
        } else
        {
            return null;
        }
        
        return curQuestion;
    }

    /*
     * Hàm khởi tạo câu hỏi để chơi.
     * 1) Lấy ngẫu nhiên 1 câu hỏi từ danh sách câu hỏi.
     * 2) Hiển thị câu hỏi và các câu trả lời.
     *  2.1) Gán câu trả lời cho các button answer. Nội dung câu trả lời đúng được lấy từ rigthAnswer 
     *      của qs, nội dung các câu trả sai được lấy tuần tự trong mảng wrongAnswer.
     *  2.2) Với từng câu trả lời ta gán sự kiện kiểm tra câu trả lời.
     */
    public void CreateQuestion()
    {
        QuestionData qs = GetRandomQuestion();

        if (qs != null)
        {
            SetQuestionText(qs.content);
            ShuffleAnswer();

            var temp = UIManager.Ins.answerButtons;

            if (temp != null)
            {
                int wrongIndex = 0;

                for (int i = 0; i < temp.Length; i++)
                {
                    int answerId = i;

                    if (string.Compare(temp[i].btnComp.tag, "RightAnswer") == 0)
                    {
                        temp[i].SetAnswerText(qs.rightAnswer);
                    }
                    else
                    {
                        temp[i].SetAnswerText(qs.wrongAnswers[wrongIndex++]);
                    }

                    temp[i].btnComp.onClick.RemoveAllListeners();
                    temp[i].btnComp.onClick.AddListener(() => {
                        CheckAnswer(temp[answerId]);
                    });
                }
            }
        }
        else { Debug.Log("qs is null"); }
    }

    /*
     * Hàm trộn câu trả lời.
     * 1) Đầu tiên gán tag của tất cả các button answer = "Untagged".
     * 2) Sau đó ta chọn ngẫu nhiên 1 button answer để gán tag = "RightAnswer".
     */
    public void ShuffleAnswer()
    {
        if (UIManager.Ins.answerButtons != null && UIManager.Ins.answerButtons.Length > 0)
        {
            for (int i = 0; i < UIManager.Ins.answerButtons.Length; i++)
            {
                if (UIManager.Ins.answerButtons[i] != null)
                {
                    UIManager.Ins.answerButtons[i].btnComp.tag = "Untagged";
                }
            }

            int ranIndex = Random.Range(0, UIManager.Ins.answerButtons.Length);

            if (UIManager.Ins.answerButtons[ranIndex] != null)
            {
                UIManager.Ins.answerButtons[ranIndex].btnComp.tag = "RightAnswer";
            }
        }
    }

    public void SetQuestionText(string content)
    {
        if (UIManager.Ins.questionText)
        {
            UIManager.Ins.questionText.text = content;
        }
    }

    /*
     * Hàm kiểm tra câu trả lời.
     */
    public void CheckAnswer(AnswerButton answerButton)
    {
        if (answerButton.btnComp.tag == "RightAnswer")
        {
            GameController.Ins.score += 10;

            /*-----Luu thong tin gameplay-----------*/
            if (PlayerPrefs.GetFloat("score") == 0 && PlayerPrefs.GetFloat("timePlay") == 0)
            {
                PlayerPrefs.SetFloat("score", GameController.Ins.score);
                PlayerPrefs.SetFloat("timePlay", TimeController.Ins.time);
                isNewRecord = true;
            }
            else
            {
                if (GameController.Ins.score > PlayerPrefs.GetFloat("score"))
                {
                    PlayerPrefs.SetFloat("score", GameController.Ins.score);
                    PlayerPrefs.SetFloat("timePlay", TimeController.Ins.time);
                    isNewRecord = true;
                }
                else if (GameController.Ins.score == PlayerPrefs.GetFloat("score"))
                {
                    if (TimeController.Ins.time < PlayerPrefs.GetFloat("timePlay"))
                    {
                        PlayerPrefs.SetFloat("timePlay", TimeController.Ins.time);
                        isNewRecord = true;
                    }
                }
            } 
            PlayerPrefs.Save();
            /*--------------------------------------------*/

            if (++rigrtAnswerCount >= countQuestions)
            {
                AudioController.Ins.PlayWinSound();
                AudioController.Ins.StopMusic();
                Time.timeScale = 0f; //Để dừng trò chơi, đơn giản ta chỉ cần set timeScale = 0
                                
                UIManager.Ins.ShowDialogEndGame();
            }
            else
            {
                Debug.Log(rigrtAnswerCount);
                AudioController.Ins.PlayRightSound();
                CreateQuestion();
            }
        }
        else
        {
            AudioController.Ins.StopMusic();
            if (isNewRecord)
                AudioController.Ins.PlayWinSound();
            else
                AudioController.Ins.PlayLoseSound();
            Time.timeScale = 0f; //Để dừng trò chơi, đơn giản ta chỉ cần set timeScale = 0
            UIManager.Ins.ShowDialogEndGame();
        }
    }

    public void MakeSingleton() {
        if (Ins == null) {
            Ins = this;
        } else {
            Destroy(gameObject);
        }
    }
}
