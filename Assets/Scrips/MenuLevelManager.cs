using UnityEngine;
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

    public void OnePlayerMode()
    {
        gameObject.SetActive(false);
        UIManager.Ins.qAP.SetActive(true);
        UIManager.Ins.pauseButton.gameObject.SetActive(true);
        UIManager.Ins.waitScreen.gameObject.SetActive(true);
        Time.timeScale = 1f;
        //Load();
        //Invoke("Load", 2f);
    }

    public void TwoPlayerMode()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Load()
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
