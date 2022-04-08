using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool GameIsOver;


	public string nextlevel = "Level02";
	public int levelToUnlock = 2;
	public void WinLevel ()
	{
		PlayerPrefs.SetInt("levelReached", levelToUnlock);
	}

}
