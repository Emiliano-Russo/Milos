using Photon.Pun;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerAnimation : MonoBehaviourPunCallbacks
{

    public GameObject pistol;
    public GameObject flashlight;

    private Animator anim;
    private PhotonView myPhotonView;
    private PlayerMovement playerMovement;
    private Player player;
    private float walkingVelocity;
    private float standarAnimSpeed;

    // Start is called before the first frame update
    void Start()
    {   
        playerMovement = this.GetComponent<PlayerMovement>();
        player = this.GetComponent<Player>();
        walkingVelocity = playerMovement.speed;
        myPhotonView = this.GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        standarAnimSpeed = 1.5f;
        anim.speed = standarAnimSpeed;
        if (!myPhotonView.IsMine)
            return;
    }



    void Update()
    {
        if (!myPhotonView.IsMine)
            return;
        Manage_Movement_State();
        Manage_Flashlight_Pistol();
        Manage_Shoot_State();
        time += Time.deltaTime;
    }

    float timeGapShoot = 0.5f;
    float time = 1f;
    private void Manage_Shoot_State()
    {
        float shoot_layer_weight = anim.GetLayerWeight(1);
        if (Input.GetButtonDown("Fire1") && shoot_layer_weight == 1 && time > timeGapShoot)
        {
            time = 0;
            player.GetPhotonView().RPC("ShootMilk", RpcTarget.All); //player.ShootMilk();
        }
    }

    private bool shiftPressed;
    private void Manage_Movement_State()
    {
        CheckShiftPressed();
        float input_x_horizontal = Input.GetAxis("Horizontal");
        float input_y_vertical = Input.GetAxis("Vertical");
        anim.SetFloat("PosX", input_x_horizontal);
        anim.SetFloat("PosY", input_y_vertical);

        float actualStamina = player.runStatusSlider.value;//Manage_Stamina();
        if (input_y_vertical == 1 && shiftPressed && actualStamina > 0)
        {
            actualStamina -= 0.2f;
            if (actualStamina <= 0)
                shiftPressed = false;
            anim.speed = 1;
            anim.SetBool("IsRunning", true);
            playerMovement.speed = walkingVelocity * 2;
        }
        else
        {
            if (actualStamina < 100)
                actualStamina += 0.1f;
            anim.speed = standarAnimSpeed;
            anim.SetBool("IsRunning", false);
            playerMovement.speed = walkingVelocity;
        }
        player.runStatusSlider.value = actualStamina;
    }


    private void CheckShiftPressed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            shiftPressed = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            shiftPressed = false;
    }

    private void Manage_Flashlight_Pistol()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.GetPhotonView().RPC("ShowFlashlight", RpcTarget.All);
            //ShowFlashlight();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.GetPhotonView().RPC("ShowPistol", RpcTarget.All);
            //ShowPistol();            
        }   
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.GetPhotonView().RPC("HideEverthing", RpcTarget.All);
            //HideEverthing();
        }
    }


   

    [PunRPC]
    private void ShowPistol()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetLayerWeight(2, 0);
        pistol.SetActive(true);
        flashlight.SetActive(false);
    }

    [PunRPC]
    private void ShowFlashlight()
    {
        anim.SetLayerWeight(2, 1);
        anim.SetLayerWeight(1, 0);
        pistol.SetActive(false);
        flashlight.SetActive(true);
    }

    [PunRPC]
    private void HideEverthing()
    {
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
        pistol.SetActive(false);
        flashlight.SetActive(false);
    }



}
