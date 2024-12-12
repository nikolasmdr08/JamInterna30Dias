using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float smoothing;
    public float rotSmoothing;
    public Transform player;
    public Vector3 offset;

    //PhotonView view;
    void Start()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            player = players[i].transform; //Encuentra a todos los player y comienza a seguir al perteneciente a este cliente.
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.position + offset;
        //transform.position = Vector3.Lerp(transform.position, player.position + offset, smoothing);
        //transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotSmoothing);
        //transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        transform.LookAt(player.position);

    }
}