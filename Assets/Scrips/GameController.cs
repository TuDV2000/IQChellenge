using UnityEngine;
using Firebase.Database;

public class GameController : MonoBehaviour
{
    public static GameController Ins;

    public float score = 0;
    public DatabaseReference mDatabaseRef;

    private void Awake()
    {
        MakeSingleton();
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
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
