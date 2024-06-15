using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLogin : MonoBehaviour
{
    [Header("===== Login =====")]
    public GameObject loginPanel;
    public Button loginButton;
    public Button loginBackButton;
    public Button navToRegisterButton;
    public InputField userLoginAlias;

    [Header("===== Register =====")]
    public GameObject registerPanel;
    public Button registerButton;
    public Button registerBackButton;
    public InputField userRegisterAlias;
    public InputField userRegisterFName;
    public InputField userRegisterLName;
    

    [Header("===== Warning Text =====")]
    [Header("Login")]
    public GameObject userLoggedIn;
    public GameObject userNotExistWarning;
    public GameObject emptyLoginWarning;

    [Header("Register")]
    public GameObject emptyRegisterWarning;
    public GameObject userExistedWarning;
    public GameObject userAccCreated;

    GameSceneManager gameSceneManager;
    APISystem api;

    void Start(){
        api = FindObjectOfType<APISystem>();
        gameSceneManager = GetComponent<GameSceneManager>();
        
        userNotExistWarning.SetActive(false);
        emptyLoginWarning.SetActive(false);
        userLoggedIn.SetActive(false);
        emptyRegisterWarning.SetActive(false);
        userAccCreated.SetActive(false);
        userExistedWarning.SetActive(false);

        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public void LoginUser()
    {
        StartCoroutine(IELoginUser());
    }

    public void RegisterUser()
    {
        StartCoroutine(IERegisterUser());
    }

    public IEnumerator IELoginUser()
    {
        userNotExistWarning.SetActive(false);
        emptyLoginWarning.SetActive(false);
        userLoggedIn.SetActive(false);

        if (string.IsNullOrEmpty(userLoginAlias.text))
        {
            emptyLoginWarning.SetActive(true);
        }

        else 
        {
            yield return StartCoroutine(api.EnablePlayerController(userLoginAlias.text));
            if (api.playerLoggedIn == true)
            {
                loginButton.interactable = false;
                loginBackButton.interactable = false;
                navToRegisterButton.interactable = false;
                userLoggedIn.SetActive(true);
                yield return new WaitForSeconds(2f);
                PlayerPrefs.SetString("alias", userLoginAlias.text);
                gameSceneManager.ChangeScene("DashboardScene");
            }
            
            else
            {
                userNotExistWarning.SetActive(true);
            }
        }  
    }

    public IEnumerator IERegisterUser()
    {
        emptyRegisterWarning.SetActive(false);
        userAccCreated.SetActive(false);
        userExistedWarning.SetActive(false);

        if (string.IsNullOrEmpty(userRegisterAlias.text) || string.IsNullOrEmpty(userRegisterFName.text) || string.IsNullOrEmpty(userRegisterLName.text))
        {
            emptyRegisterWarning.SetActive(true);
        }

        else
        {   
            yield return StartCoroutine(api.CreatePlayerController(userRegisterAlias.text, userRegisterAlias.text + "_id", userRegisterFName.text, userRegisterLName.text));
            if (api.playerAccCreated == true)
            {
                registerButton.interactable = false;
                registerBackButton.interactable = false;
                userAccCreated.SetActive(true);
                yield return new WaitForSeconds(2f); 
                BackFromRegister();
            }
            
            else if (api.playerExisted == true)
            {
                userExistedWarning.SetActive(true);
            }

            else 
            {
                Debug.Log("Error creating account.");
            }
        }
    }

    public void EmptyTextInput()
    {
        userLoginAlias.text = "";
        userRegisterAlias.text = "";
        userRegisterFName.text = "";
        userRegisterLName.text = "";
    }

    public void NavigateToRegister()
    {
        loginPanel.SetActive(false);
        EmptyTextInput();
        userNotExistWarning.SetActive(false);
        emptyLoginWarning.SetActive(false);

        registerPanel.SetActive(true);
    }

    public void BackFromRegister()
    {
        registerPanel.SetActive(false);
        registerButton.interactable = true;
        registerBackButton.interactable = true;
        EmptyTextInput();
        emptyRegisterWarning.SetActive(false);
        userExistedWarning.SetActive(false);
        userAccCreated.SetActive(false);

        loginPanel.SetActive(true);
    }
}
