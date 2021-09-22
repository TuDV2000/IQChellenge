using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public static TimeController Ins;
    public Text timeText;

    public float time = 0;

    private void Awake()
    {
        MakeSingleton();
    }

    /*
     * Hàm format time text được hiển thị lên giao diện
     */
    public void SetTimeText(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        time += Time.deltaTime;
        SetTimeText(time);
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

