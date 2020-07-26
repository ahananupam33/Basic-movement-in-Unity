using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Camera_Follower : MonoBehaviour
{
    public Transform player;
    public Vector3 camDistance; 

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + camDistance;
    }
}
