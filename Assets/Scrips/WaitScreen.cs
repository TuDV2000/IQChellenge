using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitScreen : MonoBehaviour
{
    public Text timeWaitText;
    public float timeWait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (timeWait > 0)
        {
            timeWait -= Time.deltaTime;
            SetTimeText(timeWait);
        }
        else
        {            
            gameObject.SetActive(false);
            if (GameController2.Ins != null)
            {
                GameController2.Ins.enabled = true;
                GameController2.Ins.CreateQuestion();
            }
            if (MenuLevelManager.Ins != null)
            {
                MenuLevelManager.Ins.Load();
            }
        }
        if (timeWait < 1)
        {
            timeWaitText.text = "Bắt đầu";
        }
    }

    public void SetTimeText(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeWaitText.text = seconds.ToString();
    }
}
