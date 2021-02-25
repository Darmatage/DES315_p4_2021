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
	public float fireTime;
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
	
	public void OnCollisionEnter(Collision other){
		//Debug.Log("a bot has been hit");
		if (other.gameObject.tag == "hazard"){
//		if (other.gameObject.tag == "hazard"){
			Debug.Log("a bullet hit a bot");
			int damage = other.gameObject.GetComponent<Hazard>().Damage;
			botHealth -= damage;
		}	
	}
	
}


// rigidbody MovePosition
//rb.MovePosition(transform.position + (transform.forward * time.deltaTime * speedToMoveForward));