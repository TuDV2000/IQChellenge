using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RankItem : MonoBehaviour
{
    public Text rankIndex;
    public Text nameText;
    public Text scoreText;

    public void SetText(int rankIndex, string name, string score)
    {
        this.rankIndex.text = rankIndex.ToString();
        this.nameText.text = name;
        this.scoreText.text = score;
    }
}
