using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject spawnLocation;




    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, spawnLocation.transform.position, Quaternion.identity);
      //  Instantiate(canvas);
    }

    private void Update()
    {
        
    }
}
