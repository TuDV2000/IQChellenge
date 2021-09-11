using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class QuestionManager : MonoBehaviour
{
<<<<<<< HEAD
    int rigrtAnswerCount = 0;
    public static QuestionManager ins;
    public List<QuestionData> listQuestions = new List<QuestionData>();
    DatabaseReference mDatabaseref;

=======
    public static QuestionManager Ins;
    public int rigrtAnswerCount = 0;    
    public List<QuestionData> listQuestions = new List<QuestionData>();
    DatabaseReference mDatabaseref;


>>>>>>> tu
    private void Awake() {
        MakeSingleton();
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
        mDatabaseref.ValueChanged += LoadQuestionDatas;
    }

    private void Start()
    {
<<<<<<< HEAD
        
=======

>>>>>>> tu
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
<<<<<<< HEAD
            UIManager.ins.SetQuestionText(qs.content);
            UIManager.ins.ShuffleAnswer();

            var temp = UIManager.ins.answerButtons;
=======
            UIManager.Ins.SetQuestionText(qs.content);
            UIManager.Ins.ShuffleAnswer();

            var temp = UIManager.Ins.answerButtons;
>>>>>>> tu

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
<<<<<<< HEAD
            if (++rigrtAnswerCount == 5)
            {
                TimeController.ins.enabled = false;
                UIManager.ins.dialogResult.SetDialogContent("Xin chúc mừng! Bạn đã chiến thắng!");
                UIManager.ins.dialogResult.Show(true);
            }
            else
            {
                CreateQuestion();
            }

        }
        else
        {
            TimeController.ins.enabled = false;
            UIManager.ins.dialogResult.SetDialogContent("Bạn đã sai! Trò chơi kết thúc!");
            UIManager.ins.dialogResult.Show(true);
=======
            
            GameController.ins.score += 100;
            if (++rigrtAnswerCount == MenuLevelManager.Ins.countQuestionLevel)
            {
                AudioController.Ins.PlayWinSound();
                AudioController.Ins.StopMusic();
                Time.timeScale = 0f; //Để dừng trò chơi, đơn giản ta chỉ cần set timeScale = 0
                Debug.Log("Hoan thanh!!");
                //UIManager.Ins.dialogResult.SetDialogContent("Xin chúc mừng! Bạn đã chiến thắng! Điểm " +
                //GameController.ins.score + "!");
                //UIManager.Ins.dialogResult.Show(true);
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
            Debug.Log("Sai! Ket thuc!");
            //UIManager.ins.dialogResult.SetDialogContent("Bạn đã sai! Trò chơi kết thúc! Điểm: " 
                //+ GameController.ins.score);
            //UIManager.ins.dialogResult.Show(true);
>>>>>>> tu
        }
    }

    public void MakeSingleton() {
<<<<<<< HEAD
        if (ins == null) {
            ins = this;
=======
        if (Ins == null) {
            Ins = this;
>>>>>>> tu
        } else {
            Destroy(gameObject);
        }
    }
}
