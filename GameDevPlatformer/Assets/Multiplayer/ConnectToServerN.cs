using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ConnectToServerN : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_Text buttonText;

    public void OnClickConnect()
    {
        if (usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()

    {

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("CreatingLobby");
    }
}
