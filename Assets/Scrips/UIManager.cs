using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;
    public DatabaseReference mDatabaseRef;

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
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SetQuestionText(string content) {
        if (questionText) {
            questionText.text = content;
        }
    }

    /*
     * Hàm trộn câu trả lời.
     * 1) Đầu tiên gán tag của tất cả các button answer = "Untagged".
     * 2) Sau đó ta chọn ngẫu nhiên 1 button answer để gán tag = "RightAnswer".
     */
    public void ShuffleAnswer() {
        if (answerButtons != null && answerButtons.Length > 0) {
            for (int i = 0; i < answerButtons.Length; i++) {
                if (answerButtons[i] != null) {
                    answerButtons[i].btnComp.tag = "Untagged"; 
                }
            }

            int ranIndex = Random.Range(0, answerButtons.Length);

            if (answerButtons[ranIndex] != null) {
                answerButtons[ranIndex].btnComp.tag = "RightAnswer";
            }
        }
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

