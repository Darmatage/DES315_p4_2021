using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_player : MonoBehaviour{

	public float projectileForce = 60f;
    public Transform firePoint;
    public GameObject projectilePrefab;

	public float fireTime;
	private float fireTimer;

	void Start(){		
		fireTime = Random.Range(0.1f,0.8f);
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
	
	
}
