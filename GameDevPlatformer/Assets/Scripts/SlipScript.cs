using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipScript : MonoBehaviour {

	PlayerMovement player;

	public bool slipping;

	void Start() {
		player = GameObject.FindObjectOfType<PlayerMovement>();
	}
	
	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == "Player 1") {
			slipping = true;
			player.wallSliding = false;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "Player 1") {
			slipping = false;
			player.wallSliding = true;
		}
	}
}
