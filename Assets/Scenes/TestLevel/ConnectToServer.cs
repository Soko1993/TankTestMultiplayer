using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public Text LogText;

    private void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);

        //PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //Log("Connected to Master");
        SceneManager.LoadScene("Menu");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
