using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData2 
{
    public string question;
    public string rightAnswer;
    public string wrongAnswer;

    public QuestionData2() { }

    public QuestionData2(string q, string rAnswer, string wAnswer)
    {
        this.question = q;
        this.rightAnswer = rAnswer;
        this.wrongAnswer = wAnswer;
    }
}
