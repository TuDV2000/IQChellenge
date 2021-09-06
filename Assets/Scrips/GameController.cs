using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class GameController : MonoBehaviour
{   
    private void Start()
    {
        Invoke("Load", 1f);
    }

    void Update()
    {

    }

    void Load()
    {
        QuestionManager.ins.CreateQuestion();
    }

    public void Replay() {
        SceneManager.LoadScene("GamePlay");
    }
}
