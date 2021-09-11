using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuLevelManager : MonoBehaviour
{
    public static MenuLevelManager Ins;
    public float timeLevel;
    public float countQuestionLevel;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        TimeController.ins.enabled = false;        
    }

    public void Level()
    {
        string level = EventSystem.current.currentSelectedGameObject.name;

        switch (level)
        {
            case "LevelEasyButton":
                timeLevel = 300;
                countQuestionLevel = 10;           
                break;
            case "LevelNomalButton":
                timeLevel = 240;
                countQuestionLevel = 10;
                break;
            case "LevelDifficultButton":
                timeLevel = 180;
                countQuestionLevel = 10;
                break;
        }

        gameObject.SetActive(false);
        UIManager.Ins.qAP.SetActive(true);

        Time.timeScale = 1f;
        Invoke("Load", 1.5f);
    }

    void Load()
    {
        QuestionManager.Ins.CreateQuestion();
        TimeController.ins.enabled = true;
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
