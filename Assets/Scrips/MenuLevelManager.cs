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

    public void Level()
    {
        string level = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(level);
        switch (level)
        {
            case "LevelEasyBtuton":
                timeLevel = 300;
                countQuestionLevel = 10;
                
                break;
            case "LevelNomalBtuton":
                timeLevel = 240;
                countQuestionLevel = 10;
                break;
            case "LevelDifficultBtuton":
                timeLevel = 180;
                countQuestionLevel = 10;
                break;
        }

        SceneManager.LoadScene("GamePlay");
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
