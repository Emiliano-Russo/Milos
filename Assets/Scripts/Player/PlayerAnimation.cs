using Photon.Pun;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;
    private PhotonView myPhotonView;
    private PlayerMovement playerMovement;
    private float walkingVelocity;
    private float standarAnimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
        playerMovement = this.GetComponent<PlayerMovement>();
        walkingVelocity = playerMovement.speed;
        myPhotonView = this.GetComponent<PhotonView>();
        if (!myPhotonView.IsMine)
            return;
        anim = GetComponent<Animator>();
        standarAnimSpeed = 1.5f;
        anim.speed = standarAnimSpeed;
    }


    private bool shiftPressed;
    void Update()
    {
        if (!myPhotonView.IsMine)
            return;
        CheckShiftPressed();

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        anim.SetFloat("PosX", x);
        anim.SetFloat("PosY", y);
        if (y == 1 && shiftPressed)
        {
            anim.speed = 1;
            anim.SetBool("IsRunning", true);
            playerMovement.speed = walkingVelocity * 2;
        }
        else
        {
            anim.speed = standarAnimSpeed;
            anim.SetBool("IsRunning", false);
            playerMovement.speed = walkingVelocity;
        }

    }

    private void CheckShiftPressed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            shiftPressed = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            shiftPressed = false;
    }

    public void ShowPistol() => anim.SetLayerWeight(1, 1);

    public void HidePistol() => anim.SetLayerWeight(1, 0);

    public void ShowFlashlight() => anim.SetLayerWeight(2, 1);

    public void HideFlashlight() => anim.SetLayerWeight(2, 0);


}
