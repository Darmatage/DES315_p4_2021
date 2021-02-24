using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Transform target;     // drag intended target object into Inspector slot
    public float smoother = 10f;
    public Vector3 offset;     // set the offset in the editor

    void FixedUpdate () {
       Vector3 newPos = target.position + offset;
       Vector3 smoothPos = Vector3.Lerp (transform.position, newPos, smoother * Time.deltaTime);
       transform.position = smoothPos;
	   transform.rotation = target.rotation;

       transform.LookAt (target);
    }
}



