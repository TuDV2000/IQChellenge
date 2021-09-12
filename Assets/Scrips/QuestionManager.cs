using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Ins;
    public int rigrtAnswerCount = 0;    
    public List<QuestionData> listQuestions = new List<QuestionData>();
    DatabaseReference mDatabaseref;

    private void Awake() {
        MakeSingleton();
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
        mDatabaseref.ValueChanged += LoadQuestionDatas;
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
            UIManager.Ins.SetQuestionText(qs.content);
            UIManager.Ins.ShuffleAnswer();

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
     * Hàm kiểm tra câu trả lời.
     */
    public void CheckAnswer(AnswerButton answerButton)
    {
        if (answerButton.btnComp.tag == "RightAnswer")
        {
            GameController.ins.score += 10;
            if (++rigrtAnswerCount == 19)
            {
                AudioController.Ins.PlayWinSound();
                AudioController.Ins.StopMusic();
                Time.timeScale = 0f; //Để dừng trò chơi, đơn giản ta chỉ cần set timeScale = 0

                /*-----Luu thong tin gameplay-----------*/
                //if (PlayerPrefs.GetFloat("score") == 0)
                //{
                //    PlayerPrefs.SetFloat("score", GameController.ins.score);
                //}
                //else
                //{
                //    if (GameController.ins.score > PlayerPrefs.GetFloat("score"))
                //        PlayerPrefs.SetFloat("score", GameController.ins.score);
                //}
                //if (PlayerPrefs.GetFloat("timePlay") == 0)
                //{
                //    PlayerPrefs.SetFloat("timePlay", TimeController.ins.time);
                //}
                //else
                //{
                //    if (TimeController.ins.time > PlayerPrefs.GetFloat("timePlay"))
                //        PlayerPrefs.SetFloat("timePlay", TimeController.ins.time);
                //}
                //PlayerPrefs.Save();
                //Debug.Log("score: " + PlayerPrefs.GetFloat("score"));
                //Debug.Log("time play: " + PlayerPrefs.GetFloat("timePlay"));
                /*--------------------------------------------*/

                //float timeToDisplay = TimeController.ins.time;
                //float minutes = Mathf.FloorToInt(timeToDisplay / 60);
                //float seconds = Mathf.FloorToInt(timeToDisplay % 60);
                //string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

                //Debug.Log("Finish: " + timeString + " with score = " + GameController.ins.score);
                UIManager.Ins.ShowDialogEndGame();
            }
            else
            {
                AudioController.Ins.PlayRightSound();
                CreateQuestion();
            }
        }
        else
        {
            AudioController.Ins.StopMusic();
            AudioController.Ins.PlayLoseSound();
            Time.timeScale = 0f; //Để dừng trò chơi, đơn giản ta chỉ cần set timeScale = 0

            /*-----Luu thong tin gameplay-----------*/
            //if (PlayerPrefs.GetFloat("score") == 0)
            //{
            //    PlayerPrefs.SetFloat("score", GameController.ins.score);
            //}
            //else
            //{
            //    if (GameController.ins.score > PlayerPrefs.GetFloat("score"))
            //        PlayerPrefs.SetFloat("score", GameController.ins.score);
            //}
            //if (PlayerPrefs.GetFloat("timePlay") == 0)
            //{
            //    PlayerPrefs.SetFloat("timePlay", TimeController.ins.time);
            //}
            //else
            //{
            //    if (TimeController.ins.time > PlayerPrefs.GetFloat("timePlay"))
            //        PlayerPrefs.SetFloat("timePlay", TimeController.ins.time);
            //}
            //PlayerPrefs.Save();
            //Debug.Log("score: " + PlayerPrefs.GetFloat("score"));
            //Debug.Log("time play: " + PlayerPrefs.GetFloat("timePlay"));
            /*--------------------------------------------*/

            //float timeToDisplay = TimeController.ins.time;
            //float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            //float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            //string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

            //Debug.Log("Fail: " + timeString + " with score = " + GameController.ins.score);
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
