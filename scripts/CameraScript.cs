using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public string gamestate;
    public Transform Target;
    // Update is called once per frame
    void Update()
    {
        gamestate=GameObject.FindGameObjectWithTag("playermanager").GetComponent<playermanager>().gamestate;
        if(gamestate!="GAMEOVER")
        cameradynamics();
    }

    void cameradynamics()
    {
        Target=GameObject.FindGameObjectWithTag("playermanager").GetComponent<playermanager>().cameraplayer.transform;
        Vector3 CamPosition = Target.position;
        CamPosition.z = Target.position.z - 8f;
        // Set the camera distance from gameobject and also interpolation speed
        Vector3 cameraMoveDir = (CamPosition - transform.position).normalized;
        float offsetdistance = Vector3.Distance(CamPosition, transform.position);
        float CamSpeed = 2f;

        // Check for overshooting
        if(offsetdistance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * offsetdistance * CamSpeed * Time.deltaTime;
            float offsetDistanceAfterMoving = Vector3.Distance(newCameraPosition, CamPosition);
            if(offsetDistanceAfterMoving > offsetdistance)
            {
                // Camera has overshot the target
                newCameraPosition = CamPosition;
            }
            transform.position = newCameraPosition;
        }
    }
}
