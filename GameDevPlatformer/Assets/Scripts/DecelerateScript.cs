using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerateScript : MonoBehaviour {

	PlayerMovement player;

	void Start() {
		player = GameObject.FindObjectOfType<PlayerMovement> ();
	}
	
	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == "Player 1" && player.moveSpeed > 5) {
			player.moveSpeed -= 1;
		} else if (col.gameObject.name == "Player 1") {
			while (player.moveSpeed < 10) {
				player.moveSpeed += 1;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "Player 1") {
			player.moveSpeed = 10;
		}
	}
}
