using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_player : MonoBehaviour{

	//movement
	public Rigidbody rb;
	public float rotationRate;
	private Vector3 eulerAngleVelocity;

	//weapon fire
	public bool canShoot = true;
	public float projectileForce = 60f;
    public Transform firePoint;
    public GameObject projectilePrefab;
	public float fireTime = 0.2f;
	private float fireTimer;

	//health stat
	public int botHealth = 5;


	void Start(){		
		fireTime = Random.Range(0.1f,0.8f);
		rotationRate = Random.Range(10,30);
		rb = gameObject.GetComponent<Rigidbody>();
		eulerAngleVelocity = new Vector3(0, rotationRate, 0);
	}

	void Update(){
		if (botHealth <= 0){
			Destroy(gameObject);
		}
     } 

    void FixedUpdate(){
        fireTimer += .01f;
		if (fireTimer >= fireTime){
			if (canShoot == true){
				Shoot();
			}
			fireTimer = 0;
		}
		//transform.Rotate (new Vector3 (0, rotationRate, 0) * Time.deltaTime);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
	}
	
	void Shoot(){
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * projectileForce, ForceMode.Impulse);
    }
	
	public void OnTriggerEnter(Collider other){
		//Debug.Log($"a bot has been hit: {other.gameObject.tag} " + "\n the object name is: " + other.gameObject.name);
		if (other.gameObject.tag == "bullet"){
			//Debug.Log("a bullet hit a bot");
			int damage = other.gameObject.GetComponent<Hazard>().Damage;
			botHealth -= damage;
		}	
	}
	
}


// rigidbody MovePosition
//rb.MovePosition(transform.position + (transform.forward * time.deltaTime * speedToMoveForward));