using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour{

	//Menus
	//public GameObject playerMenuUI;
	//public GameObject gameHUD;
	public GameObject pauseMenuUI;
	public static bool GameisPaused = false;
	//public GameObject fightButton;
	public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

	//Stats
	public static string playerName;
	public int playerHitsStart = 0;
	public int playerHealthStart = 20;
	public static int playerHits;
	public static int playerHealth;
	public static string winner;

	//Text Objects to display stats
	public GameObject playerHitsText;
	public GameObject playerHealthText;
	//public GameObject playerNameText;
	//public GameObject winnerText;
	
	//Timer
	//public GameObject inputFieldGameTime;
	public int gameTime = 60;
	public GameObject gameTimerText;
	private float gameTimer = 0f;

	Scene thisScene;
	

	void Awake (){
		SetLevel (volumeLevel);
		GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
		if (sliderTemp != null){
			sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
			sliderVolumeCtrl.value = volumeLevel;
		}
    }

    void Start(){
        // check for endscene
		thisScene = SceneManager.GetActiveScene();
		if (thisScene.name == "EndScene"){ }
		//initial menu displays
		//playerMenuUI.SetActive(true);
		//gameHUD.SetActive(false);
		pauseMenuUI.SetActive(false);
		//fightButton.SetActive(false);
		
		//initial player and game stats
		playerHits = playerHitsStart;
		playerHealth = playerHealthStart;
		UpdateStats();
    }

    // Update is called once per frame
    void Update(){
		if (playerHealth <= 0){
			playerHealth = 0;
			StartCoroutine(Respawn());
		}
		
		//Pause Menu 1/2
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (GameisPaused){ Resume(); }
			else{ Pause(); }
		}
    }
	
	//Timer
	void FixedUpdate(){
		gameTimer += 0.01f;
		if (gameTime <= 0){
			gameTime = 0;
			//winner = "Time's up!" + playerHealth
			//StartCoroutine(EndGame());
		}
		else if (gameTimer >= 1f){
			gameTime -= 1;
			UpdateStats();
			gameTimer = 0;
		}
	}
	
	public void UpdateStats(){
		Text pHitsTemp = playerHitsText.GetComponent<Text>();
		pHitsTemp.text = "Hits: " + playerHits;
		
		Text pHealthTemp = playerHealthText.GetComponent<Text>();
		pHealthTemp.text = "Health: " + playerHealth;
		
		//Text winTemp = winnerText.GetComponent<Text>();
		//winTemp.text = "WINNER: \n" + winner;
		
		Text timerTemp = gameTimerText.GetComponent<Text>();
		timerTemp.text = "" + gameTime;
		
	}

	public void playerTakeDamage(int damage){
		playerHealth -= damage;
	}

	IEnumerator Respawn(){
		yield return new WaitForSeconds(0.5f); 
	}

	//Pause Menu 2/2
	void Pause(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameisPaused = true;
	}
	public void Resume(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameisPaused = false;
	}
	public void Restart(){
		Time.timeScale = 1f;
		//restart the game:
		playerHits = playerHitsStart;
		playerHealth = playerHealthStart;
		//SceneManager.LoadScene("Arena1");
	}
	
	//MainMenu buttons
	public void MainMenu(){SceneManager.LoadScene("MainMenu");}
	public void Quit(){
		#if UNITY_EDITOR 
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	public void StartGame(){
		SceneManager.LoadScene("Arena1");
	}
	
	public void SetLevel (float sliderValue){
		mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
		volumeLevel = sliderValue;
    } 

}


//TODO:
//assign players to teams?
//
//how does darkrift take one player code and track multipel players?
//
//
//
//