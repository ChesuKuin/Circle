using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	public void StartGame(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
	public void LoadingScene()
	{
		SceneManager.LoadScene("Lobby");
	}
}
