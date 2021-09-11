using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class GameController : MonoBehaviour
<<<<<<< HEAD
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
=======
{
    public static GameController ins;
    public float score = 0;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        
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
>>>>>>> tu
    }
}
