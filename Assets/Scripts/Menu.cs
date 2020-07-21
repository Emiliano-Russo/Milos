using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviourPunCallbacks
{
    public InputField hostTxtInput;
    public InputField joinTxtInput;
    public InputField nickName;


    public GameObject mainMenu;
    public GameObject createRoomMenu;
    public GameObject ServerListMenu;

    public GameObject serverEntity;
    public GameObject _serverContainer;

    public Dropdown AmountPlayers;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Start() => PhotonNetwork.ConnectUsingSettings();


    public void SetNickName()
    {
        PhotonNetwork.NickName = nickName.text;
        StartCoroutine(openMainMenuFromNickName()); 
    }

    IEnumerator openMainMenuFromNickName()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Canvas/NickNameSelector").SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenMenu_RoomCreator()
    {
        mainMenu.SetActive(false);
        createRoomMenu.SetActive(true);
    }

    public void VolverAlMenu()
    {
        if (PhotonNetwork.InLobby)
            PhotonNetwork.LeaveLobby();

        createRoomMenu.SetActive(false);
        ServerListMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenMenu_ServerList()
    {
        mainMenu.SetActive(false);
        ServerListMenu.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public void StartRoom()
    {
        byte amountOfPLayers = (byte)AmountPlayers.value;
        amountOfPLayers += 1;
        string HostRoomName = hostTxtInput.text;
        NetworkManager.instance.CreateRoom(HostRoomName,amountOfPLayers);
    }

    public void Close_Game() => Application.Quit();


    public override void OnRoomListUpdate(List<RoomInfo> roomList) // clave para listar los servers
    {
        foreach (Transform child in _serverContainer.transform)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }

        foreach (var item in roomList)
        {
            GameObject instance = Instantiate(serverEntity, _serverContainer.transform);
            Transform srvName = instance.transform.Find("Panel/ServerName");
            srvName.GetComponent<Text>().text = item.Name;
            Transform totalPlayers = instance.transform.Find("Panel/TotalPlayers");
            totalPlayers.GetComponent<Text>().text = item.PlayerCount.ToString("0") + "/" + item.MaxPlayers.ToString("0");
            Transform joinBtn = instance.transform.Find("Panel/JoinBtn");
            Button btn = joinBtn.GetComponent<Button>();
            btn.onClick.AddListener(delegate { JoinRoom(item.Name); });
        }
    }

    public void JoinRoom(string srvName)
    {
        PhotonNetwork.NickName = nickName.text;
        string JoinRoomName = srvName;
        NetworkManager.instance.JoinRoom(JoinRoomName);
    }


}
