using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public static TimeController ins;
    public Text timeText;
    
    public float time;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        time = MenuLevelManager.Ins.timeLevel;
    }

    /*
     * Hàm format time text được hiển thị lên giao diện
     */
    public void SetTimeText(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            SetTimeText(time);
            //Debug.Log(time);
        }
        else
        {
            enabled = false;
            Debug.Log("ban da het thoi gian");
            //UIManager.ins.dialogResult.SetDialogContent("Bạn đã hết thời gian! Trò chơi kết thúc!");
            //UIManager.ins.dialogResult.Show(true);
        }
    }

    public void MakeSingleton()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

