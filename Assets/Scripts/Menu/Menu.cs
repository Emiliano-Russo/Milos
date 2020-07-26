using Photon.Pun;
using Photon.Realtime;
using Steamworks;
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
    public GameObject nickNameSelector;
    public GameObject mainMenu;
    public GameObject createRoomMenu;
    public GameObject ServerListMenu;
    public GameObject serverEntity;
    public GameObject _serverContainer;
    public Dropdown AmountPlayers;
    public GameObject loadingScreen;
    public Dropdown resolutionDropDown;
    public GameObject optionsMenu;
    public Toggle fullscreen;

    private Resolution[] resolutions;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Start()
    {
        resolutions = Screen.resolutions;
        PhotonNetwork.ConnectUsingSettings();
        SetNickName();      
    }


    public void SetNickName()
    {
        if (!SteamManager.Initialized) { return; }
        string name = SteamFriends.GetPersonaName();
        Debug.Log(name);
        PhotonNetwork.NickName = name;
    }


    public void OpenOptions_Menu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    public void ApplyGraphicsChanges()
    {
        Resolution resolution = resolutions[resolutionDropDown.value];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen.isOn);
        VolverAlMenu();
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
        optionsMenu.SetActive(false);
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
        createRoomMenu.SetActive(false);
        loadingScreen.SetActive(true);
        byte amountOfPLayers = (byte)AmountPlayers.value;
        amountOfPLayers += 1;
        string HostRoomName = hostTxtInput.text;
        NetworkManager.instance.CreateRoom(HostRoomName,amountOfPLayers);
    }

    public void Close_Game()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        SteamAPI.Shutdown();
    }

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
            if (item.PlayerCount == item.MaxPlayers)
                btn.gameObject.SetActive(false);
        }
    }

    public void JoinRoom(string srvName)
    {
        ServerListMenu.SetActive(false);
        loadingScreen.SetActive(true);
        string JoinRoomName = srvName;
        NetworkManager.instance.JoinRoom(JoinRoomName);
    }


}
