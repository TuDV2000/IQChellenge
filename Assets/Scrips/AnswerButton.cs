using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    public Button btnComp;

    private void Start() 
    {

    }

    public void SetAnswerText(string content)
    {
        answerText.text = content;
    }
}
