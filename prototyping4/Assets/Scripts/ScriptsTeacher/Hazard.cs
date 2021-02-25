using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour{

	public int Damage = 1;
	
	public GameObject impactParticles;
	public GameObject nohitParticles;
	public float bulletLife = 0.6f;
	private float bulletTimer;
	
	private bool bulletExists = true;

	void Awake(){
		gameObject.GetComponent<Collider>().enabled = false;
		StartCoroutine(ActivateCollider()); // designed to prevent players from exploding their own bullets 
	}

	void FixedUpdate(){
		bulletTimer += 0.01f;
		if ((bulletTimer >= bulletLife)&&(bulletExists == true)){
			bulletExists = false;
			gameObject.GetComponent<Collider>().enabled = false;
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			StartCoroutine(NoHitResult());
		}
	}

    void OnTriggerEnter(Collider other){
		gameObject.GetComponent<Collider>().enabled = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false; 
		StartCoroutine(ImpactResult());
	}
    
	IEnumerator ActivateCollider(){
		yield return new WaitForSeconds(0.1f);
		gameObject.GetComponent<Collider>().enabled = true;
	}
	
	IEnumerator ImpactResult(){
		GameObject boomParticles = Instantiate(impactParticles, transform.position, Quaternion.identity); 
		yield return new WaitForSeconds(1.0f);
		Destroy(boomParticles);
		Destroy(gameObject);
	}
	
	IEnumerator NoHitResult(){
		GameObject boomParticles1 = Instantiate(nohitParticles, transform.position, Quaternion.identity); 
		yield return new WaitForSeconds(0.1f);
		GameObject boomParticles2 = Instantiate(nohitParticles, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.1f);
		GameObject boomParticles3 = Instantiate(nohitParticles, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1.0f);
		Destroy(boomParticles1);
		Destroy(boomParticles2);
		Destroy(boomParticles3);
		Destroy(gameObject);
	}
	
}
