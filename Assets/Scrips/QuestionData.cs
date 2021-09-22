using System.Collections.Generic;

[System.Serializable]
public class QuestionData
{
    public string content;
    public string rightAnswer;
    public List<string> wrongAnswers = new List<string>();

    public QuestionData()
    {
        
    }
    public QuestionData(string content, string rightAnswer, List<string> wrongAnswers)
    {
        this.content = content;
        this.rightAnswer = rightAnswer;
        this.wrongAnswers = wrongAnswers;
    }
}

