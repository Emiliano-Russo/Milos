using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Player : MonoBehaviour
{

    public Behaviour[] DisableThings;
    public GameObject name3D;

    public Slider gayStatusSlider;
    public Text pingTxt;

    private PhotonView myPhotonView;
    private float gayStatusValue;
 


    private void Start()
    {
        myPhotonView = this.GetComponent<PhotonView>();
        if (!myPhotonView.IsMine) // im not local player
        {
            name3D.GetComponent<TextMesh>().text = GetComponent<PhotonView>().Owner.NickName;
            //disable clone player things
            foreach (var item in DisableThings)
                item.gameObject.SetActive(false);
        }
        else //im local player
        {
            Config_Cursor();
            Initialize_Values();
        }
    }

    public void Update()
    {      
        if (!myPhotonView.IsMine)// if im not local player        
            name3D.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);        
        else // if im local player
        {
            gayStatusSlider.value = gayStatusValue;
            pingTxt.text = NetworkManager.instance.GetPing().ToString() + " ms";
        }
    }

    public void TakeGayPoints(float amount) => gayStatusValue += amount;

    private void Initialize_Values()
    {     
        gayStatusValue = 10;
        gayStatusSlider.value = gayStatusValue;
        name3D.SetActive(false);
    }

    private void Config_Cursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
