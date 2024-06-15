using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DashboardManager : MonoBehaviour
{
    APISystem api;
    [SerializeField] Text welcomeText;

    // Start is called before the first frame update
    void Start()
    {
        api = FindObjectOfType<APISystem>();
        welcomeText.text = "Welcome, " + PlayerPrefs.GetString("alias");
        StartCoroutine(IEGetPlayerProfile(PlayerPrefs.GetString("alias")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IEGetPlayerProfile(string alias)
    {
        Debug.Log("start get player");
        yield return StartCoroutine(api.GetPlayerController(alias));

        PlayerPrefs.SetString("fName", api.playerContainer.message.first_name);
        PlayerPrefs.SetString("lName", api.playerContainer.message.last_name);
        string[] tempDate = api.playerContainer.message.created.Split("T");
        PlayerPrefs.SetString("joinedDate", tempDate[0]);
        PlayerPrefs.SetInt("currentScore", int.Parse(api.playerContainer.message.score[0].value));
        PlayerPrefs.SetInt("learningModuleCompleted", int.Parse(api.playerContainer.message.score[1].value));
        PlayerPrefs.SetInt("quizCompleted", int.Parse(api.playerContainer.message.score[2].value));
        PlayerPrefs.SetInt("fullComplete", int.Parse(api.playerContainer.message.score[3].value));

        Debug.Log("fName: " + PlayerPrefs.GetString("fName"));
        Debug.Log("lName: " + PlayerPrefs.GetString("lName"));
        Debug.Log("joinedDate: " + PlayerPrefs.GetString("joinedDate"));
        Debug.Log("currentScore: " + PlayerPrefs.GetInt("currentScore"));
        Debug.Log("learningModuleCompleted: " + PlayerPrefs.GetInt("learningModuleCompleted"));
        Debug.Log("quizCompleted: " + PlayerPrefs.GetInt("quizCompleted"));
        Debug.Log("fullComplete: " + PlayerPrefs.GetInt("fullComplete"));
    }

    public void LogOut()
    {
        StartCoroutine(IELogOut());
    }

    public IEnumerator IELogOut()
    {
        yield return StartCoroutine(api.DisablePlayerController(PlayerPrefs.GetString("alias")));
        SceneManager.LoadScene("LobbyScene");
    }
}
