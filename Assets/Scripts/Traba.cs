using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traba : MonoBehaviour
{

    public float speed = 3;
    public float searchRadius = 800;
    public float gayPointsDamage = 0.2f;

    private bool with_a_player = false;

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        bool PlayersOnRoom = players.Length > 0;
        if (PlayersOnRoom)
            Persecute_Player(players);
    }

    private void Persecute_Player(GameObject[] players)
    {
        GameObject closestPlayer = GetClosestPlayerInsideCircle(players);
        FollowPlayer(closestPlayer);
        if (with_a_player)
            closestPlayer.gameObject.GetComponent<Player>().TakeGayPoints(gayPointsDamage);
    }

    private GameObject GetClosestPlayerInsideCircle(GameObject[] Allplayers)
    {
        GameObject closestPlayer = null;
        float MinDistance = searchRadius;
        foreach (var player in Allplayers)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < MinDistance)
            {
                MinDistance = distance;               
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }


    private void FollowPlayer(GameObject player)
    {
        if (player == null)
            return;
        Vector3 playerPosition = player.transform.position;
        transform.LookAt(new Vector3(playerPosition.x,transform.position.y,playerPosition.z));
        if (!with_a_player)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), Time.deltaTime * speed);
    }



    private void OnTriggerEnter(Collider other)
    {
        GameObject objectCollied = other.gameObject;
        if (objectCollied.tag == "Player")
        {       
            with_a_player = true;
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject objectCollied = other.gameObject;
        if (objectCollied.tag == "Player")
        {
            with_a_player = false;
        }
    }


}
