using UnityEngine;
using Firebase.Database;

public class UIHomeManager : MonoBehaviour
{
    public static UIHomeManager Ins;

    public DialogInputName dialogInputName;
    public HomeManager homeManager;
    public DatabaseReference mDatabaseRef;

    private void Awake()
    {
        MakeSingleton();
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("name") == "")
            dialogInputName.Show();
        else
            homeManager.Show();
    }

    public void ClearPlayerPref()
    {
        PlayerPrefs.DeleteAll();
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
