using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOrbit : MonoBehaviour{
	
    public float turnSpeed = 4.0f;
    public Transform player;

	public float height = 10f;
    public float distance = -10f;
 
	private Vector3 offsetX;
	private Vector3 offsetY;
 
  void Start () {
     
     offsetX = new Vector3 (0, height, distance);
     offsetY = new Vector3 (0, 0, distance);
 }
      
     void LateUpdate(){
     offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
     offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
     
	 if (player != null){
		transform.position = player.position + offsetX + offsetY;
		transform.LookAt(player.position);
	 }
 }
}




// public float turnSpeed = 4.0f;
     // public Transform player;
     // private Vector3 offset;
 
	// public float OffsetY = 8f;
	// public float OffsetZ = 7f;
 
     // void Start () {
         // offset = new Vector3(player.position.x, player.position.y + OffsetY, player.position.z + OffsetZ);
     // }
 
     // void LateUpdate()
     // {
         // offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
         // transform.position = player.position + offset; 
         // transform.LookAt(player.position);
     // }
 // }



      // public float turnSpeed = 4.0f;
      // public Transform player;
 
     // public float height = 5f;
     // public float distance = -10f;
     
     // private Vector3 offsetX;
     // private Vector3 offsetY;
     
     // void Start () {
 
         // offsetX = new Vector3 (0, height, distance);
         // offsetY = new Vector3 (0, 0, distance);
     // }
     
     // void LateUpdate()
     // {
         // offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.up) * offsetX;
         // offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
         // transform.position = player.position + offsetX; 
         // transform.LookAt(player.position);
     // }
 // }