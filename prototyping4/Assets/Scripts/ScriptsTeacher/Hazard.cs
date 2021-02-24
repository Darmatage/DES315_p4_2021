using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour{

	public int Damage = 1;
	
	public GameObject impactParticles;
	public float bulletLife = 2f;
	private float bulletTimer;

	void FixedUpdate(){
		bulletTimer += 0.01f;
		if (bulletTimer >= bulletLife){
			StartCoroutine(ImpactResult());
		}
	}

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
		StartCoroutine(ImpactResult());
	}
    
	IEnumerator ImpactResult(){
		GameObject boomParticles = Instantiate(impactParticles, transform.position, Quaternion.identity); 
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
	
}
