using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material material;

    private PhotonView photonView;

    private void Start() => photonView = PhotonView.Get(this);
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            photonView.RPC("CambiarColorAVerde", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            photonView.RPC("CambiarColorARojo", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void CambiarColorAVerde()
    {
        material.color = Color.green;
    }

    [PunRPC]
    public void CambiarColorARojo()
    {
        material.color = Color.red;
    }


}
