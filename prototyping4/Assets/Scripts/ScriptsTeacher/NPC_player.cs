using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_player : MonoBehaviour{

	//movement
	public float rotationRate;

	//weapon fire
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
	}

	void Update(){
		void Update () {
			transform.Rotate (new Vector3 (0, rotationRate, 0) * Time.deltaTime);
        }
		if (botHealth <= 0){
			Destroy(gameObject);
		}
     } 


    void FixedUpdate(){
        fireTimer += .01f;
		if (fireTimer >= fireTime){
			Shoot();
			fireTimer = 0;
		}
    }
	
	
	void Shoot(){
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * projectileForce, ForceMode.Impulse);
    }
	
	void OnCollionEnter(Collision other){
		if (other.gameObject.tag == "hazard"){
			int damage = other.gameObject.GetComponent<Hazard>().Damage;
			botHealth -= damage;
		}
		
	}
	
	
	
}
