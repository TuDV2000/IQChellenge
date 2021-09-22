using UnityEngine;
using UnityEngine.UI;

public class DialogInputName : MonoBehaviour
{
    public static DialogInputName Ins;
    public string playerName;
    public Text placeh;
    public Text nameText;
    public Text notificationText;

    private void Awake()
    {
        MakeSingleton();
    }

    public void Show()
    {        
        gameObject.SetActive(true);
    }

    public void SetPlayerName()
    {
        PlayerData player = new PlayerData(nameText.text);
        if (player.CheckPlayer(PlayerManager.Ins.listPlayer))
        {
            notificationText.text = "Tên đã được sử dụng";
            notificationText.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("name", nameText.text);
            PlayerPrefs.Save();
            gameObject.SetActive(false);
            UIHomeManager.Ins.homeManager.Show();
        }
    }

    public void HideNotification()
    {
        if (notificationText.gameObject.activeInHierarchy)
            notificationText.gameObject.SetActive(false);
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
