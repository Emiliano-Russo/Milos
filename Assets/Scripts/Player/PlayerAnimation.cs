using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;
    PhotonView myPhotonView;
    private PlayerMovement playerMovement;
    private float walkingVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        walkingVelocity = playerMovement.speed;
        myPhotonView = this.GetComponent<PhotonView>();
        if (!myPhotonView.IsMine)
            return;
        anim = GetComponent<Animator>();
    }


    private bool shiftPressed;
    private bool moveKeysPressed;
    void Update()
    {
        if (!myPhotonView.IsMine)
            return;
        Check_shiftPressed();
        Check_MoveKeysPressed();

        if (moveKeysPressed)
        {
            if (shiftPressed)
            {
                anim.SetBool("Running", true);
                playerMovement.speed = walkingVelocity * 2;
            }
            else
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
                playerMovement.speed = walkingVelocity;
            }
        }
        else
        {
            print("se detiene");
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
        }
        print("ShiftPressed: " + shiftPressed);
        print("MoveKeysPressed: " + moveKeysPressed);
    }

    private void Check_shiftPressed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            shiftPressed = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            shiftPressed = false;
    }

    private void Check_MoveKeysPressed()
    {
        if (Input.GetKeyDown(KeyCode.W))
            moveKeysPressed = true;
        else if (Input.GetKeyUp(KeyCode.W))
            moveKeysPressed = false;
    }
}
