using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Player : MonoBehaviour
{

    public Behaviour[] DisableThings;
    public PhotonView pv;
    public bool EnEntornoDePrueba = false;



    private float life = 100;
    private Text lifeStats;
    private Text ping;
    private Hashtable personalHash = new Hashtable();


    private void Start()
    {
        if (pv.IsMine || EnEntornoDePrueba)
        {
            lifeStats = GameObject.FindGameObjectWithTag("lifeStats").GetComponent<Text>();
            ping = GameObject.FindGameObjectWithTag("ping").GetComponent<Text>();
            personalHash[PhotonNetwork.NickName] = this.life;
            PhotonNetwork.SetPlayerCustomProperties(personalHash);
            return;
        }
        else
        {
            //desactivo mis cosas (sino soy local)
            foreach (var item in DisableThings)
                item.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (!pv.IsMine && !EnEntornoDePrueba)
            return;
        ping.text = NetworkManager.instance.GetPing().ToString() + " ms";
        GetAllPlayersHeal();

        if (Input.GetKeyDown(KeyCode.O))
            TakeDamage(20);
    }

    public void TakeDamage(float amount)
    {
        personalHash[PhotonNetwork.NickName] = (float)personalHash[PhotonNetwork.NickName] - amount;
        PhotonNetwork.SetPlayerCustomProperties(personalHash);
    }

    public void GetAllPlayersHeal()
    {
        var players = NetworkManager.instance.GetPlayerList();
        lifeStats.text = NetworkManager.instance.GetNickName() + ": " + personalHash[PhotonNetwork.NickName];

        foreach (var player in players)
            lifeStats.text += "-" + player.NickName + player.CustomProperties[player.NickName];        
    }

}
