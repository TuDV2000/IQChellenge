using UnityEngine;

public class DialogRank : MonoBehaviour
{
    public GameObject listRankContent;
    public RankItem currentRank;
    RankItem[] rankLists;

    private void Start()
    {
        rankLists = listRankContent.GetComponentsInChildren<RankItem>();

        if (PlayerManager.Ins.listPlayer.Count > 0)
        {
            PlayerManager.Ins.listPlayer.Sort();

            int max = rankLists.Length;

            for (int i = 0; i < PlayerManager.Ins.listPlayer.Count; i++)
            {
                if (i < max)
                {
                    rankLists[i].SetText(i + 1, PlayerManager.Ins.listPlayer[i].name,
                        PlayerManager.Ins.listPlayer[i].score.ToString());
                }

                if (PlayerManager.Ins.listPlayer[i].name.Equals(PlayerPrefs.GetString("name")))
                {
                    currentRank.SetText(i + 1, PlayerManager.Ins.listPlayer[i].name,
                        PlayerManager.Ins.listPlayer[i].score.ToString());
                    if (i >= max)
                        break;
                }
            }
        }
    }

    public void Ok()
    {
        gameObject.SetActive(false);
    }
}
