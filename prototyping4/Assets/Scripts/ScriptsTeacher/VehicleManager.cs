using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleManager : MonoBehaviour{

	//player access
	// public Transform accessPoint;
	// public float accessRange = 10f;
	// public LayerMask playerLayers;
	
	public int vehicleStrengthStart = 50;
	public int vehicleStrength;
	
	public Renderer vehicleRenderer;
	private Color vehicleColor;
	
	public bool canEnter = true;
	public bool isDriven = false;
	
	public GameObject driverPos;
	public GameObject exitPos;
	public GameObject firePoint;
	public GameObject projectile;
	public float fireRate = 1f;
	
    void Start(){
		vehicleRenderer = gameObject.GetComponentInChildren<Renderer>();
		vehicleColor = vehicleRenderer.material.color;
		vehicleStrength = vehicleStrengthStart;
    }
	
	void Update(){
		if (isDriven == true){canEnter = false;}
		else {canEnter = true;}
		
		if (vehicleStrength <= 0){
			vehicleStrength =0;
			if (isDriven == true){
				gameObject.GetComponentInChildren<Player_shoot>().isDriving = false;
				gameObject.GetComponentInChildren<Player_access>().amDriving = false;
				gameObject.GetComponentInChildren<Player_access>().vehicleStrength.SetActive(false);
				gameObject.GetComponentInChildren<Player_move>().GetOffVehicle(gameObject, driverPos, exitPos);
				vehicleStrength = vehicleStrengthStart;
				isDriven = false;
				
			}
		}
	}
	
	public void OnTriggerEnter(Collider other){
		if ((other.gameObject.tag == "bullet")&&(isDriven == true)){
			int damage = other.gameObject.GetComponent<Hazard>().Damage;
			vehicleStrength -= damage;
			StopCoroutine(impactFlash());
			StartCoroutine(impactFlash());
			UpdateVehicleStrength();
			// gameHandlerObj.playerTakeDamage(damage);
		} else {}
	}
	
	IEnumerator impactFlash(){
		Color color = Color.red;
		float emitOn = 0.2f; 
		vehicleRenderer.material.color = color;
		vehicleRenderer.material.SetColor("_EmissionColor",color*emitOn);
		
		yield return new WaitForSeconds(0.1f);
		
		float emitOff = 0f; 
		vehicleRenderer.material.color = vehicleColor;
		vehicleRenderer.material.SetColor("_EmissionColor",vehicleColor*emitOff);
	}
	
	public void UpdateVehicleStrength(){
		Text vStrengthText = gameObject.GetComponentInChildren<Player_access>().vehicleStrengthText;
		vStrengthText.text = "" + vehicleStrength;
		
	}
	

}


	// public void playerAccess(){
		// animator.SetTrigger("Access");
		// Collider[] nearPlayers = Physics.OverlapSphere(accessPoint.position, accessRange, playerLayers);
		// Collider[] nearPlayers = Physics.OverlapBox(accessPoint.position, new Vector3(accessRange *3f, accessRange *5f, accessRange *4f), Quaternion.identity, playerLayers);

		// foreach(Collider player in nearPlayers){
			// Debug.Log("Welcome, " + player.name);
			// player.GetComponent<playerDriver>().gainAccess();
		// }
	// }


	// void OnDrawGizmosSelected(){
		// if (accessPoint == null) return;
		//Gizmos.DrawWireSphere(accessPoint.position, accessRange);
		// Gizmos.DrawWireCube(accessPoint.position, new Vector3(accessRange *3f, accessRange *5f, accessRange *4f));
	// }
	