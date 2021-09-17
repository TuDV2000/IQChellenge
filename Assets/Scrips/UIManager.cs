using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;

    public Text questionText;
    public AnswerButton[] answerButtons;

    public GameObject qAP; //Question-answer-pause
    public DialogEndGame dialogEndGame;
    public DialogOption dialogOption;
    public DialogRank dialogRank;
    public Canvas canvas;
    public Button pauseButton;

    private void Awake()
    {
        MakeSingleton();
    }

    public void ShowDialogEndGame()
    {
        var rectTransform = dialogEndGame.GetComponent<RectTransform>();
        float x = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float y = canvas.GetComponent<RectTransform>().sizeDelta.y;
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(x, y);
        }
        pauseButton.gameObject.SetActive(false);
        dialogEndGame.Show();
    }

    public void ShowDialogOption()
    {
        var rectTransform = dialogOption.GetComponent<RectTransform>();
        float x = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float y = canvas.GetComponent<RectTransform>().sizeDelta.y;
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(x, y);
        }
        pauseButton.gameObject.SetActive(false);
        dialogOption.Show();
    }

    public void MakeSingleton() {
        if (Ins == null) {
            Ins = this;
        } else {
            Destroy(gameObject);
        }
    }    
}

