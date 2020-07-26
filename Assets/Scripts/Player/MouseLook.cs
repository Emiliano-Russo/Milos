using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [HideInInspector]
    public float mouseSensitivity;

    public Transform playerBody;

    float xRotation = 0f;

    public PhotonView pv;

    public bool onMenu;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MouseSensibility"))
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensibility");
        else
            mouseSensitivity = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 42f);

        if (onMenu)
            return;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
    }

}
