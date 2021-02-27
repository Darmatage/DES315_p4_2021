using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour{
    public float bulletForce = 60f;
    public Transform firePoint;
    public GameObject bulletPrefab;

	public bool isDriving = false;
	public GameObject vehicleFirePoint;
	public GameObject vehicleProjectile;
	public float vehicleFireRate;
	public float fireTimer = 0;
	bool canShoot = true;

    void Update(){
            if ((Input.GetButtonDown("Fire1")) || (Input.GetKeyDown("space"))) {
                  Shoot();
            }
    }

	void FixedUpdate(){
		fireTimer += 0.01f;
		if (fireTimer >= vehicleFireRate){
			canShoot = true;
			fireTimer = 0;
		}
		
	}


    // Create the bullet and send it flying
    void Shoot(){
		if (isDriving == false){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
		} else if ((isDriving == true)&&(canShoot == true)){
			GameObject bullet = Instantiate(vehicleProjectile, vehicleFirePoint.transform.position, vehicleFirePoint.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(vehicleFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
			canShoot = false;
		}
    }
	
	public void VehicleShoot(GameObject vFirepoint, GameObject vProjectile, float vFireRate){
		isDriving = true;
		vehicleFirePoint = vFirepoint;
		vehicleProjectile = vProjectile;
		vehicleFireRate = vFireRate;
	}
	
}