using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager2 : MonoBehaviour
{
    public static PlayerManager2 Ins;

    public Slider P1Slider;

    public Slider P2Slider;

    public float P1Health;

    public float P2Health;

    public Text hit1;

    public Text hit2;

    public float damage;

    public void Awake()
    {
        MakeSingleton();
    }

    public void SetSilderValue()
    {
        P1Slider.maxValue = P1Health;
        P1Slider.value = P1Health;
        P2Slider.maxValue = P2Health;
        P2Slider.value = P2Health;
    }

    public void TakeDamageP1()
    {
        P1Health -= damage;
        P1Slider.value = P1Health;
    }

    public void ChangeHitText1(string t)
    {
        StartCoroutine(WaitChange1(t));
    }

    private IEnumerator WaitChange1(string t)
    {
        hit1.text = t;
        yield return new WaitForSeconds(0.5F);
        hit1.text = "";
    }

    public void TakeDamageP2()
    {
        P2Health -= damage;
        P2Slider.value = P2Health;
    }

    public void ChangeHitText2(string t)
    {
        StartCoroutine(WaitChange2(t));
    }

    private IEnumerator WaitChange2(string t)
    {
        hit2.text = t;
        yield return new WaitForSeconds(0.5F);
        hit2.text = "";
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
