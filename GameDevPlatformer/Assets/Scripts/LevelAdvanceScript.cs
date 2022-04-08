using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvanceScript : MonoBehaviour {

	public bool isWon;

	public GameObject nextLevelMenuCanvas;
	public GameObject player;
	public GameManager gameManager;
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Player 1") {
			isWon = true;
		}
	}

	public void Resume() {
		isWon = false;
	}

	public void QuitGame() {
		SceneManager.LoadScene("Main Menu");
	}

	public void NextLevel(string nextLevelName) {
		SceneManager.LoadScene(nextLevelName);
	}

	void Update() {
		if (isWon) {
			nextLevelMenuCanvas.SetActive(true);

			gameManager.WinLevel();
			this.enabled = false;
		} else {
			nextLevelMenuCanvas.SetActive(false);
		}

		if (isWon && Input.GetKeyDown(KeyCode.R)) {
			isWon = false;
		}

		if (isWon && Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("Main Menu");
		}
	}

	
	
}
