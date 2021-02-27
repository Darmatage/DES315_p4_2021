using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_getHit : MonoBehaviour{

	public GameHandler gameHandlerObj;
	public int playerHealth = 5;
	
	void Start(){		
		if (GameObject.FindWithTag("GameHandler") != null){
			gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		}
	}
	
	void Update(){
		if (playerHealth <= 0){
			Destroy(gameObject);
		}
     } 	
		
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "bullet"){
			//Debug.Log("a bullet hit the player");
			int damage = other.gameObject.GetComponent<Hazard>().Damage;
			playerHealth -= damage;
			gameHandlerObj.playerTakeDamage(damage);
		}	
	}
}
