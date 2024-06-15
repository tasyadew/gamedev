using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    APISystem api;
    public Transform contentTransform;
    public GameObject playerListPrefab;
    public GameObject userRankObj;
    public GameObject leaderboardScene;
    public GameObject loadingScene;

    void Start()
    {
        api = FindObjectOfType<APISystem>();
        leaderboardScene.SetActive(false);
        loadingScene.SetActive(true);  
        StartCoroutine(IEDisplayLeaderboard());
    }

    private IEnumerator IEDisplayLeaderboard()
    {
        yield return StartCoroutine(api.GetLeaderboardController());
        var instantiatePrefab = new List<GameObject>();
        string tempRank;
        string tempAlias;
        string tempScore;

        api.leaderboardContainer.message.data.Sort((x, y) => int.Parse(y.score).CompareTo(int.Parse(x.score)));

        for (int i = 0; i < api.leaderboardContainer.message.data.Count; i++)
        {
            instantiatePrefab.Add(Instantiate(playerListPrefab, contentTransform));

            tempRank = (i + 1).ToString();
            instantiatePrefab[i].transform.GetChild(0).GetComponent<Text>().text = tempRank;

            tempAlias = api.leaderboardContainer.message.data[i].alias;
            instantiatePrefab[i].transform.GetChild(1).GetComponent<Text>().text = tempAlias;

            tempScore = api.leaderboardContainer.message.data[i].score;
            instantiatePrefab[i].transform.GetChild(2).GetComponent<Text>().text = tempScore;

            if(tempAlias == PlayerPrefs.GetString("alias"))
            {
                userRankObj.transform.GetChild(1).GetComponent<Text>().text = tempRank;
                userRankObj.transform.GetChild(2).GetComponent<Text>().text = tempAlias;
                userRankObj.transform.GetChild(3).GetComponent<Text>().text = tempScore;
                PlayerPrefs.SetInt("CurrentScore", int.Parse(tempScore));
            }
        }

        loadingScene.SetActive(false);
        leaderboardScene.SetActive(true);
    }
}