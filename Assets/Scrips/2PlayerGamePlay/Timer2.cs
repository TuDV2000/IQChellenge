using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer2 : MonoBehaviour
{
    public static Timer2 Ins;

    public Slider timerSlider;
    public float gameTime;

    public bool stopTimer;

    private float time;

    public void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        //enabled = false;
        time = gameTime;
    }

    public void SetupTime()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = time;
    }

    // Update is called once per frame
    public void UpdateTime()
    {
        //Debug.Log(time);

        if (time <= 0)
        {
            stopTimer = true;
            EndTime();
            time = gameTime;
        }
        else
        {
            stopTimer = false;
            time -= Time.deltaTime;
        }

        SetupTime();
    }

    public void EndTime()
    {
        PlayerManager2.Ins.TakeDamageP1();
        PlayerManager2.Ins.TakeDamageP2();
        PlayerManager2.Ins.ChangeHitText1("-2");
        PlayerManager2.Ins.ChangeHitText2("-2");
    }

    public void ResetTime()
    {
        time = gameTime;
    }

    private IEnumerator WaitCreateQuestion()
    {
        yield return new WaitForSeconds(0.5F);
        GameController2.Ins.CreateQuestion();
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
