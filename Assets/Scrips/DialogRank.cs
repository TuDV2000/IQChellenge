using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;

public class DialogRank : MonoBehaviour
{
    public GameObject listRankContent;
    public RankItem currentRank;
    RankItem[] rankLists;

    private void Start()
    {
        rankLists = listRankContent.GetComponentsInChildren<RankItem>();

        if (DialogEndGame.Ins.listPlayerDatas.Count > 0)
        {
            DialogEndGame.Ins.listPlayerDatas.Sort();

            int max = rankLists.Length;

            for (int i = 0; i < DialogEndGame.Ins.listPlayerDatas.Count; i++)
            {
                if (i < max)
                {
                    rankLists[i].SetText(i + 1, DialogEndGame.Ins.listPlayerDatas[i].name,
                        DialogEndGame.Ins.listPlayerDatas[i].score);
                }

                if (DialogEndGame.Ins.listPlayerDatas[i].name.Equals(PlayerPrefs.GetString("name")))
                {
                    currentRank.SetText(i + 1, DialogEndGame.Ins.listPlayerDatas[i].name,
                        DialogEndGame.Ins.listPlayerDatas[i].score);
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
