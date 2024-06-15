using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class APISystem : MonoBehaviour
{
    const string baseURI = "https://tenenet.net/api";
    const string token = "6579db37db85048e18bc07a222438d70";
    const string leaderboard_id = "leaderboard_player_score";
    public bool playerExisted;
    public bool playerAccCreated;
    public bool playerLoggedIn;
    public PlayerContainer playerContainer; //user details
    public LeaderboardContainer leaderboardContainer; //leaderboard details
    public MessageContainer messageContainer; //message returned   

    public IEnumerator CreatePlayerController(string alias, string id, string fname, string lname)
    {
        UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURI + "/createPlayer" + "?token=" + token + "&alias=" + alias + "&id=" + id + "&fname=" + fname + "&lname=" + lname, "");

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            StartCoroutine(CreatePlayerController(alias, id, fname, lname));
           
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            messageContainer = JsonUtility.FromJson<MessageContainer>(www.downloadHandler.text);
            if(messageContainer.message == "player_exists")
            {
                playerExisted = true;
                playerAccCreated = false;
            }
            else if (messageContainer.status == "0")
            {
                playerAccCreated = false;          
            }
            else
            {
                playerExisted = false;
                playerAccCreated = true;
            }
        }
    }

    public IEnumerator EnablePlayerController(string alias)
    {
        Debug.Log("Enabling " + alias + "...");
        UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURI + "/enablePlayer" + "?token=" + token + "&alias=" + alias, "");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            StartCoroutine(EnablePlayerController(alias));
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            messageContainer = JsonUtility.FromJson<MessageContainer>(www.downloadHandler.text);
            if(messageContainer.message == "player_no_exists")
            {
                playerLoggedIn = false;
            }
            else 
            {
                playerLoggedIn = true;          
            }
        }
    }

    public IEnumerator DisablePlayerController(string alias)
    {
        Debug.Log("Disabling " + alias + "...");
        UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURI + "/disablePlayer" + "?token=" + token + "&alias=" + alias, "");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            StartCoroutine(DisablePlayerController(alias));
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    public IEnumerator GetPlayerController(string alias)
    {
        Debug.Log("Getting " + alias + "'s data... ");
        UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURI + "/getPlayer" + "?token=" + token + "&alias=" + alias, "");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            StartCoroutine(GetPlayerController(alias));
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            playerContainer = JsonUtility.FromJson<PlayerContainer>(www.downloadHandler.text);

            // if(playerContainer.status == "0")
            // {
            //     playerExisted = false;
            // }
            // else 
            // {
            //     playerExisted = true;    
            // }
        }
    }

    public IEnumerator InsertPlayerActivityController(string alias, string metric_ID, string addOrRemove, string value)
    {
        Debug.Log("Inserting " + alias + "'s activity data...");
        UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURI + "/insertPlayerActivity" + "?token=" + token + "&alias=" + alias + "&id=" + metric_ID + "&operator=" + addOrRemove + "&value=" + value, "");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            StartCoroutine(InsertPlayerActivityController(alias, metric_ID, addOrRemove, value));
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    public IEnumerator GetLeaderboardController()
    {
        Debug.Log("Getting player leaderboard...");
        UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURI + "/getLeaderboard" + "?token=" + token + "&id=" + leaderboard_id, "");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result== UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            StartCoroutine(GetLeaderboardController());
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            leaderboardContainer = JsonUtility.FromJson<LeaderboardContainer>(www.downloadHandler.text);
        }
    }
}
