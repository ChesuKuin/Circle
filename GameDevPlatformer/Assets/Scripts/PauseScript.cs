using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

	public bool isPaused;

	public GameObject pauseMenuCanvas;
	public GameObject settingsCanvas;

	public void QuitGame() {
		SceneManager.LoadScene("Main Menu");
	}
	

	void Update() {
		if (isPaused) {
			pauseMenuCanvas.SetActive(true);
		} else {
			pauseMenuCanvas.SetActive(false);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = !isPaused;
		} else if (isPaused && Input.GetKeyDown(KeyCode.R)) {
			isPaused = false;
		}
	}
}
