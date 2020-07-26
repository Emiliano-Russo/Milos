using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    //instance

    public static NetworkManager instance;
    private Photon.Realtime.Player[] playerList;

    private void Awake()
    {
        if (instance != null && instance != this)
            gameObject.SetActive(false);
        else
        {
            //set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        playerList = GetPlayerList();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
        ChangeScene("Game");
    }


    public void CreateRoom(string roomName,byte maxPlayers)
    {
        RoomOptions config = new RoomOptions();
        config.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(roomName, config,TypedLobby.Default);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    public Photon.Realtime.Player[] GetPlayerList()
    {
        return PhotonNetwork.PlayerListOthers;
    }

    internal int GetPing()
    {
        return PhotonNetwork.GetPing();
    }

    public string GetNickName()
    {
        return PhotonNetwork.NickName;
    }

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    

}
