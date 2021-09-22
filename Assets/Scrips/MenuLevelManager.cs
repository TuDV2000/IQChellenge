using UnityEngine;

public class MenuLevelManager : MonoBehaviour
{
    public static MenuLevelManager Ins;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        TimeController.Ins.enabled = false;
    }

    public void Mode()
    {
        gameObject.SetActive(false);
        UIManager.Ins.qAP.SetActive(true);
        UIManager.Ins.pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
        //StartCoroutine(Load());
        //if (QuestionManager.Ins.listQuestions.Count == 0)
        //{
        //    Invoke("Load", 2f);
        //    UIManager.Ins.questionText.text = "Khong load duoc cau hoi! loi roi";
        //}
        //else
        //    Load();
        Invoke("Load", 2f);

    }

    void Load()
    {
        //yield return new WaitForSeconds(2f);
        //UIManager.Ins.questionText.text = "Khong load duoc cau hoi! loi roi";
        QuestionManager.Ins.CreateQuestion();
        TimeController.Ins.enabled = true;
    }

    public void MakeSingleton()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
