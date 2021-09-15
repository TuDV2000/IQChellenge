using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHomeManager : MonoBehaviour
{
    public static UIHomeManager Ins;
    public DialogInputName dialogInputName;
    public HomeManager homeManager;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("name") == "")
            dialogInputName.Show();
        else
            homeManager.Show();
    }

    public void ClearPlayerPref()
    {
        PlayerPrefs.DeleteAll();
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
