using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Player : MonoBehaviour
{

    public Behaviour[] DisableThings;


    private PhotonView myPhotonView;
    private float gayStatusValue;
    private Slider gayStatusSlider;
    private Text pingTxt;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gayStatusSlider = GameObject.FindGameObjectWithTag("gayStatus").GetComponent<Slider>();
        pingTxt = GameObject.FindGameObjectWithTag("ping").GetComponent<Text>();
        gayStatusValue = 10;
        gayStatusSlider.value = gayStatusValue;
        myPhotonView = this.GetComponent<PhotonView>();
        if (!myPhotonView.IsMine)
        { 
            //disable my things (if im not localPlayer)
            foreach (var item in DisableThings)
                item.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (!myPhotonView.IsMine)
            return;
        pingTxt.text = NetworkManager.instance.GetPing().ToString() + " ms";
    }


}
