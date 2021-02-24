using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour{
    public float bulletForce = 60f;
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update(){
            if ((Input.GetButtonDown("Fire1")) || (Input.GetKeyDown("space"))) {
                  Shoot();
            }
    }

      // Create the bullet and send it flying
    void Shoot(){
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}