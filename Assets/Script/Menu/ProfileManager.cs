using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    APISystem api;

    [Header("User Profile Prompt")]
    [SerializeField] TextMeshProUGUI userAlias;
    [SerializeField] TextMeshProUGUI fName;
    [SerializeField] TextMeshProUGUI lName;
    [SerializeField] TextMeshProUGUI joinedDate;

    [Header("Badges")]
    [SerializeField] GameObject learningModule;
    [SerializeField] GameObject allQuiz;
    [SerializeField] GameObject fullComplete;
    Color completedBadgeText = new Color(0, 61, 255);

    void Start()
    {
        api = FindObjectOfType<APISystem>();
        SetPlayerProfile();
    }

    void Update()
    {
        
    }

    public void SetPlayerProfile()
    {
        userAlias.text = PlayerPrefs.GetString("alias");
        fName.text = PlayerPrefs.GetString("fName");
        lName.text = PlayerPrefs.GetString("lName");
        joinedDate.text = PlayerPrefs.GetString("joinedDate"); 

        if(PlayerPrefs.GetInt("learningModuleCompleted") == 5)
        {
            learningModule.transform.GetChild(0).GetComponent<Text>().color = completedBadgeText;
            learningModule.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if(PlayerPrefs.GetInt("quizCompleted") == 5)
        {
            allQuiz.transform.GetChild(0).GetComponent<Text>().color = completedBadgeText;
            allQuiz.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if(PlayerPrefs.GetInt("fullComplete") == 1)
        {
            fullComplete.transform.GetChild(0).GetComponent<Text>().color = completedBadgeText;
            fullComplete.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
    }
}
