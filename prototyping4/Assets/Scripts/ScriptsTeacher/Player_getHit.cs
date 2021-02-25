using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_getHit : MonoBehaviour{
		
	public int playerHealth = 5;
		
	void Update(){
		if (playerHealth <= 0){
			Destroy(gameObject);
		}
     } 	
		
	public void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "hazard"){
			Debug.Log("a bullet hit the player");
			int damage = other.gameObject.GetComponent<Hazard>().Damage;
			playerHealth -= damage;
		}	
	}
}
