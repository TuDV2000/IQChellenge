using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class GameController : MonoBehaviour
{
    public static GameController ins;
    public float score = 0;

    public DatabaseReference mDatabaseRef;

    private void Awake()
    {
        MakeSingleton();
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
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
