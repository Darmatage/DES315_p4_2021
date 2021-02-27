 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour {
    public CharacterController controller;
    public Transform cam;

    public float speed = 12f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float gravity = -9.81f;
    public float jumpHeight = 5;
    private Vector3 velocity;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

	public bool isDriving = false;
	public GameObject myVehicle;

    void Update () {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 direct = new Vector3(horiz, 0f, vert).normalized;

        if (direct.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (isDriving == false){
				controller.Move(moveDir.normalized * speed * Time.deltaTime);
			} else if (isDriving == true){
				myVehicle.GetComponent<CharacterController>().Move(moveDir.normalized * speed * Time.deltaTime);
			}
		}
		
        //JUMP
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
	
	
	public void GetOnVehicle(GameObject vehicle, GameObject driverPos, GameObject exitPos){
		gameObject.transform.parent = driverPos.transform;
		
		Vector3 newPos = driverPos.transform.position;
		gameObject.GetComponent<CharacterController>().enabled = false;
		transform.position = newPos;
		
		isDriving = true;
		myVehicle = vehicle;
	}

	public void GetOffVehicle(GameObject vehicle, GameObject driverPos, GameObject exitPos){
		Vector3 newPos = exitPos.transform.position;
		transform.position = newPos;
		gameObject.GetComponent<CharacterController>().enabled = true;
		gameObject.transform.parent = null;
		isDriving = false;
	}


	
	
} 