using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_access : MonoBehaviour{
	
	public bool canEnter = false;
	public GameObject pressE;
	public GameObject vehicleStrength;
	public Text vehicleStrengthText;
	public Color vehicleColor;
	
	public GameObject myVehicle;
	public GameObject myV_driverPos;
	public GameObject myV_exitPos;
	public GameObject myV_firePoint;
	public GameObject myV_projectile;
	public float myV_fireRate;
	
	public bool amDriving = false;
	public bool canExit = false;
	
    // Start is called before the first frame update
    void Start(){
        pressE.SetActive(false);
		vehicleStrength.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
		if ((canEnter == true)&&(Input.GetKeyDown(KeyCode.E))){
			pressE.SetActive(false);
			canEnter = false;
			canExit = false;
			Debug.Log("Entering Vehicle");
			amDriving = true;
			myVehicle.GetComponent<VehicleManager>().isDriven = true;
			gameObject.GetComponent<Player_move>().GetOnVehicle(myVehicle, myV_driverPos, myV_exitPos);
			gameObject.GetComponent<Player_shoot>().VehicleShoot(myV_firePoint, myV_projectile, myV_fireRate);
			vehicleStrength.SetActive(true);
			myVehicle.GetComponent<VehicleManager>().UpdateVehicleStrength();
			AccessColorOff();
			StartCoroutine(exitDelay());
		}
		else if ((canExit==true)&&(Input.GetKeyDown(KeyCode.E))){
			Debug.Log("Exiting Vehicle");
			vehicleStrength.SetActive(false);
			amDriving = false;
			myVehicle.GetComponent<VehicleManager>().isDriven = false;
			gameObject.GetComponent<Player_shoot>().isDriving = false;
			gameObject.GetComponent<Player_move>().GetOffVehicle(myVehicle, myV_driverPos, myV_exitPos);
		}
    }
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Vehicle"){
			bool isThisDriven = other.gameObject.GetComponent<VehicleManager>().isDriven;
			if (isThisDriven == false){
				canEnter=true;
				pressE.SetActive(true);
				
				myVehicle = other.gameObject;
				myV_driverPos = other.gameObject.GetComponent<VehicleManager>().driverPos;
				myV_exitPos = other.gameObject.GetComponent<VehicleManager>().exitPos;
				myV_firePoint = other.gameObject.GetComponent<VehicleManager>().firePoint;
				myV_projectile = other.gameObject.GetComponent<VehicleManager>().projectile;
				myV_fireRate = other.gameObject.GetComponent<VehicleManager>().fireRate;
				
				AccessColorOn();  // must be placed AFTER myVehicle is define
			}
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Vehicle"){
			canEnter=false;
			pressE.SetActive(false);
			AccessColorOff();
		}
	}
	
	public void EjectPlayer(){
		
		
	}
	
	
	void AccessColorOn(){
		vehicleColor = myVehicle.GetComponent<VehicleManager>().vehicleRenderer.material.color;
		Color color = Color.green;
		float emit = 0.2f; 
		myVehicle.GetComponent<VehicleManager>().vehicleRenderer.material.color = color;
		myVehicle.GetComponent<VehicleManager>().vehicleRenderer.material.SetColor("_EmissionColor",color*emit);
	}
	
	void AccessColorOff(){
		float emit = 0f; 
		myVehicle.GetComponent<VehicleManager>().vehicleRenderer.material.color = vehicleColor;
		myVehicle.GetComponent<VehicleManager>().vehicleRenderer.material.SetColor("_EmissionColor",vehicleColor*emit);
	}

	IEnumerator exitDelay(){
		yield return new WaitForSeconds(1f);
		canExit = true;
	}
}
