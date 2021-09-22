using UnityEngine;
using UnityEngine.UI;

public class DialogResult : MonoBehaviour
{
    public Text dialogContentText;

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);    
    }

    public void SetDialogContent(string content)
    {
        dialogContentText.text = content;
    }
}
