using Photon.Pun;
using System;
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
    public Slider runStatusSlider;
    public Text pingTxt;   
    public Image vodkaImg;
    public Image faisanImg;
    public AudioSource drinkSound;
    public AudioSource shootMilkSound;
    public ParticleSystem milkParticles;
    public GameObject milkSpawnPosition;
    public GameObject milk;

    private PhotonView myPhotonView;
    private float gayStatusValue;
    private bool vinoFaisanTomado = false;
    private bool vodkaAbsolutTomado = false;
 


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

    public PhotonView GetPhotonView() => myPhotonView;

    public void Update()
    {      
        if (!myPhotonView.IsMine)// if im not local player        
            name3D.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);        
        else // if im local player
        {
            gayStatusSlider.value = gayStatusValue;
            pingTxt.text = NetworkManager.instance.GetPing().ToString() + " ms";

           // Vector3 forward = milkSpawnPosition.transform.TransformDirection(Vector3.forward) * 10;
            //Debug.DrawRay(milkSpawnPosition.transform.position, forward, Color.green);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                Drink_Vodka();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Drink_VinoFaisan();            
        }
    }

    [PunRPC]
    public void ShootMilk()
    {     
        float distanceToLocalPlayer = Vector3.Distance(this.transform.position, Camera.main.transform.position);

        if (distanceToLocalPlayer < 10)
        {
            float volume = (100 - distanceToLocalPlayer * 8)/100;
            shootMilkSound.volume = volume;
            shootMilkSound.Play();
        }

        milkParticles.Play();

        Vector3 shootDir = (milkSpawnPosition.transform.forward).normalized;      
        GameObject milkBullet = Instantiate(milk, milkSpawnPosition.transform.position, Quaternion.identity);
        milkBullet.GetComponent<Milk>().SetUp_ShootDir(shootDir);

        
    }

    public void TakeGayPoints(float amount) => gayStatusValue += amount;

    public void PlayMilkSound() => shootMilkSound.Play();

    private void Drink_Vodka()
    {
        if (vodkaAbsolutTomado) return;
        vodkaAbsolutTomado = true;
        drinkSound.Play();
        vodkaImg.gameObject.SetActive(false);
        gayStatusValue = 0;
    }

    private void Drink_VinoFaisan()
    {
        if (vinoFaisanTomado) return;
        vinoFaisanTomado = true;
        drinkSound.Play();
        faisanImg.gameObject.SetActive(false);
        gayStatusValue = 0;
    }

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
