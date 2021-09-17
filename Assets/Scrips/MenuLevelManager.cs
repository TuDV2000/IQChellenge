using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        Invoke("Load", 2f);
    }

    void Load()
    {
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
