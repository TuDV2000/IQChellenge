using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager ins;
    public DialogResult dialogResult;
    public Text questionText;
    public AnswerButton[] answerButtons;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        
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

    public void MakeSingleton() {
        if (ins == null) {
            ins = this;
        } else {
            Destroy(gameObject);
        }
    }    
}

