using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog2 : MonoBehaviour
{
    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
