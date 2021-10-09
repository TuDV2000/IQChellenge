using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class QuestionManager2 : MonoBehaviour
{
    public static QuestionManager2 Ins;
    //public QuestionData2[] questions;
    public List<QuestionData2> listQuestions = new List<QuestionData2>();

    DatabaseReference databaseRef;
    //List<QuestionData2> m_questions;

    QuestionData2 m_curQuestion;

    public QuestionData2 CurQuestion { get => m_curQuestion; set => m_curQuestion = value; }

    private void Awake()
    {
        MakeSingleton();
        //m_questions = questions.ToList();
        databaseRef = FirebaseDatabase.DefaultInstance.GetReference("questions2");
        
    }

    private void Start()
    {
        if (listQuestions.Count == 0)
        {
            LoadQuestionDatas();
        }
    }

    public void LoadQuestionDatas()
    {
        databaseRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            try
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Error firebase get value");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot dsQuestions in snapshot.Children)
                    {
                        QuestionData2 q = new QuestionData2();
                        q.question = dsQuestions.Child("question").Value.ToString();
                        q.rightAnswer = dsQuestions.Child("rightAnswer").Value.ToString();
                        q.wrongAnswer = dsQuestions.Child("wrongAnswer").Value.ToString();
                        listQuestions.Add(q);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.StackTrace);
                Debug.Log(e.Message);
            }
        });
    }

    public QuestionData2 GetRandomQuestion() 
    {
        //if(m_questions != null && m_questions.Count > 0)
        //{
        //    int randIdx = Random.Range(0, m_questions.Count);

        //    m_curQuestion = m_questions[randIdx];

        //    m_questions.RemoveAt(randIdx);
        //}
        if (listQuestions != null && listQuestions.Count > 0)
        {
            int randIdx = Random.Range(0, listQuestions.Count);

            m_curQuestion = listQuestions[randIdx];

            listQuestions.RemoveAt(randIdx);
        }
        return m_curQuestion;
    }

    public void MakeSingleton()
    {
        if (Ins == null) {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
